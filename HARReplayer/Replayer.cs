using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace HARReplayer
{
    public class Replayer
    {
        private readonly IConfiguration _config;
        private readonly string[] _allMethods = { "PUT", "HEAD", "DELETE", "GET", "POST", "OPTIONS", "PATCH" };
        private readonly string[] _methodsToReplay = null;
        private readonly TextWriter _output = Console.Out;

        public Replayer(IConfiguration config)
        {
            _config = config;

            if (_config.IncludedHttpMethods.ToUpper() == "ALL")
            {
                _methodsToReplay = _allMethods;
            }
            else
            {
                _methodsToReplay = _config.IncludedHttpMethods.Split('|');
            }
            if (_config.SuppressOutput)
            {
                Console.SetOut(new StreamWriter(Stream.Null));
            }
        }

        public void Play()
        {
            StreamReader sr = new StreamReader(_config.HarFile);
            var rootObj = JsonConvert.DeserializeObject<RootObject>(sr.ReadToEnd());

            Replay(rootObj.log);
        }

        void Replay(Log session)
        {
            Console.WriteLine("Replaying {0} sessions", session.entries.Count);
            Stopwatch sw = new Stopwatch();

            Stats stats = new Stats();
            HttpClientHandler handler = new HttpClientHandler();
            handler.MaxRequestContentBufferSize = Int16.MaxValue * 2 * 10; //10 times the default.
            

            using (HttpClient client = new HttpClient(handler))
            {
                sw.Start();
                foreach (var entry in session.entries)
                {

                    if (_methodsToReplay.FirstOrDefault(x => x.ToUpper() == entry.request.method.ToUpper()) == null)
                    {
                        Console.WriteLine("HttpMethod {0} will not be included. Skipping {1}", entry.request.method, entry.request.url);
                        continue;
                    }

                    Console.WriteLine("Testing: {0}", entry.request.url);
                    Uri u = new Uri(entry.request.url);

                    HttpRequestMessage msg = new HttpRequestMessage(new HttpMethod(entry.request.method), u);

                    foreach (var h in entry.request.headers)
                    {
                        if (!h.name.StartsWith("Content"))
                        {
                            msg.Headers.Add(h.name, h.value);
                        }
                    }

                    if (entry.request.postData != null)
                    {
                        if (entry.request.postData.@params != null)
                        {
                            var formData = new List<KeyValuePair<string, string>>();
                            foreach (var p in entry.request.postData.@params)
                            {
                                formData.Add(new KeyValuePair<string, string>(p.name, p.value));
                            }
                            msg.Content = new FormUrlEncodedContent(formData); 
                        }
                        else if (!String.IsNullOrWhiteSpace(entry.request.postData.text))
                        {
                            msg.Content = new StringContent(entry.request.postData.text, Encoding.UTF8, entry.request.postData.mimeType);
                        }
                    }

                    Task<HttpResponseMessage> t = client.SendAsync(msg);
                    stats.RequestsExecuted += 1;

                    HttpResponseMessage response = null;
                    try
                    {
                        response = t.Result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Internal error: {0}", e.Message);
                        Console.WriteLine("StackTrace: {0}", e.ToString());
                        stats.Errors.Add(String.Format("INTERNAL ERROR: {0} {1}", entry.request.method, entry.request.url));
                        continue;
                    }
                    if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotModified)
                    {
                        stats.Errors.Add(String.Format("({0}) {1} {2}", response.StatusCode, entry.request.method, entry.request.url));
                    }

                    Console.WriteLine("Status: " + response.StatusCode);
                    foreach (var respH in response.Headers)
                    {
                        Console.WriteLine("{0}: {1}", respH.Key, respH.Value);
                    }

                }
                sw.Stop();
                stats.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                
            }
            stats.RequestsPerSecond = stats.RequestsExecuted / ((decimal)stats.ElapsedMilliseconds / 1000M);
            if (!_config.SuppressStats)
            {
                Console.SetOut(_output);
                if (_config.StatsAsJson)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(stats));
                }
                else
                {
                    Console.WriteLine("=======================================================");
                    Console.WriteLine("RequestsExecuted: {0}", stats.RequestsExecuted);
                    Console.WriteLine("ElapsedMilliseconds: {0}", stats.ElapsedMilliseconds);
                    Console.WriteLine("NumberOfErrors: {0}", stats.Errors.Count);
                    Console.WriteLine("RequestsPerSecond: {0}", stats.RequestsPerSecond);

                    if (stats.Errors.Count > 0)
                    {
                        Console.WriteLine("Requests with errors...");
                        foreach (string errors in stats.Errors)
                        {
                            Console.WriteLine(errors);
                        }
                    }
                }

            }
        }
    }
}

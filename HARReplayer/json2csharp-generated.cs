using System.Collections.Generic;

/// Generated using Fiddler (4.4.3.8 beta) HttpArchive v1.2 export format and http://json2csharp.com
namespace HARReplayer
{
    public class Timings
    {
        public int blocked { get; set; }
        public int ssl { get; set; }
        public int receive { get; set; }
        public int wait { get; set; }
        public int dns { get; set; }
        public int send { get; set; }
        public int connect { get; set; }
    }

    public class Header
    {
        public string value { get; set; }
        public string name { get; set; }
    }


    public class Param
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class PostData
    {
        public string text { get; set; }
        public string mimeType { get; set; }
        public List<Param> @params { get; set; }
    }


    public class Request
    {
        public int headersSize { get; set; }
        public string url { get; set; }
        public List<Header> headers { get; set; }
        public int bodySize { get; set; }
        public string httpVersion { get; set; }
        public List<object> cookies { get; set; }
        public string method { get; set; }
        public List<object> queryString { get; set; }
        public PostData postData { get; set; }
    }

    public class Content
    {
        public string comment { get; set; }
        public int size { get; set; }
        public int compression { get; set; }
        public string mimeType { get; set; }
        public string text { get; set; }
        public string encoding { get; set; }
    }

    public class Header2
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class Response
    {
        public int bodySize { get; set; }
        public int headersSize { get; set; }
        public string redirectURL { get; set; }
        public Content content { get; set; }
        public string statusText { get; set; }
        public List<Header2> headers { get; set; }
        public int status { get; set; }
        public string httpVersion { get; set; }
        public List<object> cookies { get; set; }
    }

    public class Cache
    {
    }

    public class Entry
    {
        public int time { get; set; }
        public Timings timings { get; set; }
        public string connection { get; set; }
        public Request request { get; set; }
        public Response response { get; set; }
        public string startedDateTime { get; set; }
        public Cache cache { get; set; }
    }

    public class Creator
    {
        public string name { get; set; }
        public string comment { get; set; }
        public string version { get; set; }
    }

    public class Log
    {
        public List<object> pages { get; set; }
        public string comment { get; set; }
        public List<Entry> entries { get; set; }
        public Creator creator { get; set; }
        public string version { get; set; }
    }

    public class RootObject
    {
        public Log log { get; set; }
    }
}

using Yaclp.Attributes;

namespace HARReplayer
{
    public class CommandLineParameters : IConfiguration
    {
        [ParameterDescription("The path to a har file.")]
        [ParameterExample(@"c:\scrap\fiddlerlog.har")]
        public string HarFile { get; set; }


        [ParameterDescription("Should the replayer send the embedded cookies?")]
        [ParameterExample("false")]
        [ParameterDefault("true")]
        [ParameterIsOptional]
        public bool SendCookies { get; set; }

        [ParameterDescription("Which method(s) should the replayer send from the session log?")]
        [ParameterExample("HEAD|PATCH|PUT")]
        [ParameterDefault("ALL")]
        [ParameterIsOptional]
        public string IncludedHttpMethods { get; set; }

        [ParameterDescription("Suppress output?")]
        [ParameterExample("true")]
        [ParameterDefault("false")]
        [ParameterIsOptional]
        public bool SuppressOutput { get; set; }

        [ParameterDescription("Suppress statistics summary?")]
        [ParameterExample("true")]
        [ParameterDefault("false")]
        [ParameterIsOptional]
        public bool SuppressStats { get; set; }

        [ParameterDescription("Statistics Format?")]
        [ParameterExample("human|json|csv")]
        [ParameterDefault("human")]
        [ParameterIsOptional]
        public string StatsFormat { get; set; }

    }
}

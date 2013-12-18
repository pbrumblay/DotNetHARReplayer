
namespace HARReplayer
{
    public interface IConfiguration
    {
        string HarFile { get; set; }

        bool SendCookies { get; set; }

        string IncludedHttpMethods { get; set; }

        bool SuppressOutput { get; set; }

        bool SuppressStats { get; set; }

        string StatsFormat { get; set; }
    }
}

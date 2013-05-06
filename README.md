DotNetHARReplayer
=================

C# application which replays HttpArchive v1.2 session log. This application requires .NET 4.0. From the usage output:

<pre>
Usage:
        HARReplayer.exe <HarFile> [/SendCookies:<SendCookies>] [/IncludedHttpMethods:<IncludedHttpMethods>] 
        [/SuppressOutput:<SuppressOutput>] [/SuppressStats:<SuppressStats>] [/StatsAsJson:<StatsAsJson>]

Details:
        HarFile                 The path to a har file. e.g. 'c:\scrap\fiddlerlog.har'
        SendCookies             Should the replayer send the embedded cookies? e.g. 'false'
        IncludedHttpMethods     Which method(s) should the replayer send from the session log? e.g. 'HEAD|PATCH|PUT'
        SuppressOutput          Suppress output? e.g. 'true'
        SuppressStats           Suppress statistics summary? e.g. 'true'
        StatsAsJson             Format statistics as JSON? e.g. 'true'
</pre>

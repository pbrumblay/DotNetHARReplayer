using Yaclp;

namespace HARReplayer
{
    class Player
    {
        static void Main(string[] args)
        {
            var config = DefaultParser.ParseOrExitWithUsageMessage<CommandLineParameters>(args);

            new Replayer(config).Play();
        }

    }
}

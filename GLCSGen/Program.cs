using System.Xml.Linq;
using CommandLine;
using GLCSGen.Spec;

namespace GLCSGen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            if (!Parser.Default.ParseArguments(args, options))
            {
                return;
            }

            var spec = new GlSpec(XDocument.Load(options.GlSpecFile));
        }
    }
}

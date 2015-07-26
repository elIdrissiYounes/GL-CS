using System;
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

            foreach (var feature in spec.Features)
            {
                Console.WriteLine("Feature: {0} {1} {2}", feature.Api, feature.Name, feature.Version);
                Console.WriteLine("  {0} enumerations", feature.Enumerations.Count);
                Console.WriteLine("  {0} commands", feature.Commands.Count);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

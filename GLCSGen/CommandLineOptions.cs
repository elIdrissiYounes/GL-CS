using CommandLine;

namespace GLCSGen
{
    internal class CommandLineOptions
    {
        [Option('s', "spec", Required = false, HelpText = "Path to the gl.xml spec to parse.")]
        public string GlSpecFile { get; set; } = "gl.xml";

        [Option('o', "outdir", Required = false, HelpText = "Path to directory where generated files are written.")]
        public string OutputPath { get; set; } = "output";
    }
}

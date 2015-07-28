using CommandLine;

namespace GLCSGen
{
    internal class CommandLineOptions
    {
        [Option('s', "spec", Required = false, HelpText = "Path to the gl.xml spec to parse.")]
        public string GlSpecFile { get; set; } = "gl.xml";

        [Option('o', "outdir", Required = false, HelpText = "Path to directory where generated files are written.")]
        public string OutputPath { get; set; } = "output";

        // TODO: Add option to only generate certain versions/profiles
        // TODO: Add option to add more C#-like methods (rewrite out parameters as return values, etc)
        // TODO: Add option to only include specific functions (and only include enums that are in groups used by those function parameters).
    }
}

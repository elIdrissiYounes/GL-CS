using System.IO;
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

            var outputPath = options.OutputPath;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            foreach (var feature in spec.Features)
            {
                WriteFeatureToFile(feature, outputPath);
            }
        }

        private static void WriteFeatureToFile(IGlApi api, string outputPath)
        {
            var version = api.Version.ToString().Replace(".", "");
            var filePath = Path.Combine(outputPath, $"{api.ApiFamily}{version}.cs");
            using (var streamWriter = new StreamWriter(filePath))
            {
                var code = new CodeWriter(streamWriter);

                code.WriteLine("using System;");
                code.WriteLine();
                code.WriteLine("namespace OpenGL");
                code.WriteOpenBraceAndIndent();

                code.WriteLine($"public static class {api.ApiFamily}{version}");
                code.WriteOpenBraceAndIndent();

                foreach (var enumeration in api.Enumerations)
                {
                    WriteEnumeration(code, enumeration);
                }

                code.DedentAndWriteCloseBrace();

                code.DedentAndWriteCloseBrace();
            }
        }

        private static void WriteEnumeration(CodeWriter code, IGlEnumeration enumeration)
        {
            var type = string.Empty;
            var value = string.Empty;
            if (enumeration.Int32Value.HasValue)
            {
                type = "int";
                value = enumeration.Int32Value.Value.ToString();
            }
            else if (enumeration.UInt32Value.HasValue)
            {
                type = "uint";
                value = "0x" + enumeration.UInt32Value.Value.ToString("x8");
            }
            else if (enumeration.UInt64Value.HasValue)
            {
                type = "ulong";
                value = "0x" + enumeration.UInt64Value.Value.ToString("x16");
            }

            code.WriteLine($"public const {type} {enumeration.Name} = {value};");
        }
    }
}

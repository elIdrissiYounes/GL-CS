using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Console.WriteLine("Reading spec from {0}", options.GlSpecFile);
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
            var name = string.Format("{0}{1}", api.ApiFamily, api.Version.ToString().Replace(".", ""));
            if (api.ProfileType != GlProfileType.None)
            {
                name += api.ProfileType.ToString();
            }
            var filePath = Path.Combine(outputPath, $"{name}.cs");

            Console.WriteLine("Writing {0} {1} ({2}) to {3}", api.ApiFamily, api.Version, api.ProfileType, filePath);
            using (var streamWriter = new StreamWriter(filePath))
            {
                var code = new CodeWriter(streamWriter);

                code.WriteLine("using System;");
                code.WriteLine();
                code.WriteLine("namespace OpenGL");
                code.WriteOpenBraceAndIndent();

                code.WriteLine($"public static class {name}");
                code.WriteOpenBraceAndIndent();

                foreach (var enumeration in api.Enumerations.OrderBy(e => e.Name))
                {
                    WriteEnumeration(code, enumeration);
                }

                foreach (var cmd in api.Commands.OrderBy(c => c.Name))
                {
                    code.WriteLine();
                    WriteCommand(code, cmd);
                }

                code.DedentAndWriteCloseBrace();

                code.DedentAndWriteCloseBrace();
            }
        }

        private static void WriteCommand(CodeWriter code, IGlCommand cmd)
        {
            var returnType = GetCSharpType(cmd.ReturnType);
            var args = GetCSharpArgs(cmd.Parameters);

            code.WriteLine("private delegate {0} {1}Func({2});", returnType, cmd.Name, args);
            code.WriteLine("private static {0}Func {0}Ptr;", cmd.Name);

            var argsInCall = string.Join(", ", cmd.Parameters.Select(p => "@" + p.Name));
            code.WriteLine(
                "public static {0} {1}({2}) => " + (returnType != "void" ? "return {1}Ptr({3});" : "{1}Ptr({3});"),
                returnType, 
                cmd.Name, 
                args,
                argsInCall);
        }

        private static string GetCSharpArgs(IEnumerable<IGlParameter> parameters)
        {
            return string.Join(", ", parameters.Select(p => p.Type.ToCSharpType() + " @" + p.Name).ToArray());
        }

        private static string GetCSharpType(IGlType returnType)
        {
            return returnType.ToCSharpType();
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

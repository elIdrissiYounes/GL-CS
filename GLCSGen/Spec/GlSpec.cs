using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlSpec : IGlSpec
    {
        public GlSpec(XDocument doc)
        {
            Enumerations = ParseEnums(doc);
            Commands = ParseCommands(doc);
        }

        public IReadOnlyList<IGlEnumeration> Enumerations { get; }
        public IReadOnlyList<IGlCommand> Commands { get; }

        private static IReadOnlyList<IGlCommand> ParseCommands(XDocument doc)
        {
            var commands = new List<IGlCommand>();

            foreach (var commandsNode in doc.Root.Elements("commands"))
            {
                commands.AddRange(commandsNode.Elements("command").Select(GlCommand.Parse));
            }

            return commands;
        }

        private static IReadOnlyList<IGlEnumeration> ParseEnums(XDocument doc)
        {
            var enumerations = new List<IGlEnumeration>();

            foreach (var enumsNode in doc.Root.Elements("enums"))
            {
                enumerations.AddRange(enumsNode.Elements("enum").Select(GlEnumeration.Parse));
            }

            return enumerations;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlSpec : IGlSpec
    {
        public GlSpec(XDocument doc)
        {
            var allEnums = ParseEnums(doc);
            var allCommands = ParseCommands(doc);
            Features = ParseFeatures(doc, allEnums, allCommands);
        }

        public IReadOnlyList<IGlApi> Features { get; }

        private static IReadOnlyList<IGlApi> ParseFeatures(XDocument doc, IReadOnlyList<IGlEnumeration> allEnums, IReadOnlyList<IGlCommand> allCommands)
        {
            var features = new List<IGlApi>();
            
            foreach (var featureNode in doc.Root.Elements("feature"))
            {
            }

            return features;
        }

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

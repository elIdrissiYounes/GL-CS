using System;
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

        public IReadOnlyList<IGlFeature> Features { get; }

        private static IReadOnlyList<IGlFeature> ParseFeatures(XDocument doc, IEnumerable<IGlEnumeration> allEnums, IEnumerable<IGlCommand> allCommands)
        {
            return doc.Root?.Elements("feature").Select(featureNode => GlFeature.Parse(featureNode, allEnums, allCommands)).ToList();
        }

        private static IEnumerable<IGlCommand> ParseCommands(XDocument doc)
        {
            var commands = new List<IGlCommand>();

            foreach (var commandsNode in doc.Root.Elements("commands"))
            {
                commands.AddRange(commandsNode.Elements("command").Select(GlCommand.Parse));
            }

            return commands;
        }

        private static IEnumerable<IGlEnumeration> ParseEnums(XDocument doc)
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

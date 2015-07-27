using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlFeature : IGlFeature
    {
        public GlFeature(
            GlApi api,
            string name,
            Version version,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands)
            : this(api, name, version, enumerations, commands, null)
        {}

        public GlFeature(
            GlApi api,
            string name,
            Version version,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands,
            IGlFeature parentFeature)
            : this(api, name, version, enumerations, commands, parentFeature, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>())
        {}

        public GlFeature(
            GlApi api,
            string name,
            Version version,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands,
            IGlFeature parentFeature,
            IEnumerable<IGlEnumeration> enumerationsToRemove,
            IEnumerable<IGlCommand> commandsToRemove)
        {
            Api = api;
            Name = name;
            Version = version;

            if (parentFeature != null)
            {
                if (version.CompareTo(parentFeature.Version) <= 0)
                {
                    throw new GlInvalidParentFeatureException();
                }

                enumerations = parentFeature.Enumerations.Union(enumerations).Except(enumerationsToRemove);
                commands = parentFeature.Commands.Union(commands).Except(commandsToRemove);
            }

            Enumerations = new List<IGlEnumeration>(enumerations);
            Commands = new List<IGlCommand>(commands);
        }

        public GlApi Api { get; }
        public string Name { get; }
        public Version Version { get; }
        public IReadOnlyList<IGlEnumeration> Enumerations { get; }
        public IReadOnlyList<IGlCommand> Commands { get; }

        public static IGlFeature Parse(
            XElement element,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands,
            IGlFeature parentFeature = null)
        {
            var api = (GlApi)Enum.Parse(typeof(GlApi), element.Attribute("api").Value, true);
            var name = element.Attribute("name").Value;
            var version = Version.Parse(element.Attribute("number").Value);

            var enumerationsToAdd = new List<IGlEnumeration>();
            var commandsToAdd = new List<IGlCommand>();

            var enumerationsToRemove = new List<IGlEnumeration>();
            var commandsToRemove = new List<IGlCommand>();

            foreach (var requireElement in element.Elements("require"))
            {
                enumerationsToAdd.AddRange(requireElement.Elements("enum")
                                                         .Select(e => e.Attribute("name").Value)
                                                         .Select(n => enumerations.First(e => e.Name == n)));

                commandsToAdd.AddRange(requireElement.Elements("command")
                                                     .Select(e => e.Attribute("name").Value)
                                                     .Select(n => commands.First(c => c.Name == n)));
            }

            foreach (var removeElement in element.Elements("remove"))
            {
                enumerationsToRemove.AddRange(removeElement.Elements("enum")
                                                           .Select(e => e.Attribute("name").Value)
                                                           .Select(n => enumerations.First(e => e.Name == n)));

                commandsToRemove.AddRange(removeElement.Elements("command")
                                                       .Select(e => e.Attribute("name").Value)
                                                       .Select(n => commands.First(c => c.Name == n)));
            }

            return new GlFeature(api, name, version, enumerationsToAdd, commandsToAdd, parentFeature, enumerationsToRemove, commandsToRemove);
        }
    }
}

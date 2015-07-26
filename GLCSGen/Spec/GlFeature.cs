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
        {
            Api = api;
            Name = name;
            Version = version;
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
            IEnumerable<IGlCommand> commands)
        {
            var api = (GlApi)Enum.Parse(typeof(GlApi), element.Attribute("api").Value, true);
            var name = element.Attribute("name").Value;
            var version = Version.Parse(element.Attribute("number").Value);

            IEnumerable<IGlEnumeration> featureEnumerations;
            IEnumerable<IGlCommand> featureCommands;

            var requireElement = element.Element("require");
            if (requireElement != null)
            {
                featureEnumerations = requireElement.Elements("enum")
                                                    .Select(e => e.Attribute("name").Value)
                                                    .Select(n => enumerations.First(e => e.Name == n));

                featureCommands = requireElement.Elements("command")
                                                .Select(e => e.Attribute("name").Value)
                                                .Select(n => commands.First(c => c.Name == n));
            }
            else
            {
                featureEnumerations = Enumerable.Empty<IGlEnumeration>();
                featureCommands = Enumerable.Empty<IGlCommand>();
            }

            return new GlFeature(api, name, version, featureEnumerations, featureCommands);
        }
    }
}

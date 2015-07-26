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

            var featureEnumerations = Enumerable.Empty<IGlEnumeration>();
            var featureCommands = Enumerable.Empty<IGlCommand>();

            return new GlFeature(api, name, version, featureEnumerations, featureCommands);
        }
    }
}

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

            var enumerationsByGroup = new Dictionary<string, IEnumerable<IGlEnumeration>>();

            foreach (var groupElem in doc.Root.Element("groups").Elements("group"))
            {
                var enumNames = groupElem.Elements("enum").Select(e => e.Attribute("name").Value).ToList();

                enumerationsByGroup.Add(groupElem.Attribute("name").Value,
                                        allEnums.Where(e => enumNames.Contains(e.Name)));
            }

            Features = ParseFeatures(doc, enumerationsByGroup, allEnums, allCommands);
        }

        public IReadOnlyList<IGlApi> Features { get; }

        private static IReadOnlyList<IGlApi> ParseFeatures(
            XDocument doc,
            IReadOnlyDictionary<string, IEnumerable<IGlEnumeration>> enumsByGroup,
            IReadOnlyList<IGlEnumeration> allEnums,
            IReadOnlyList<IGlCommand> allCommands)
        {
            var features = new List<IGlApi>();

            foreach (var featureElem in doc.Root.Elements("feature"))
            {
                var family = GlApi.GetApiFamily(featureElem);
                var version = GlApi.GetVersion(featureElem);

                if (family == GlApiFamily.Gl && version.Equals(new Version(1, 0)))
                {
                    features.Add(GlApi.ParseOpenGl1(featureElem,
                                                    enumsByGroup,
                                                    allCommands));
                }
                else if (family == GlApiFamily.Gl && version.CompareTo(new Version(3, 0)) >= 0)
                {
                    features.Add(GlApi.Parse(featureElem,
                                             GetParentApi(features, family, version),
                                             GlProfileType.Compatibility,
                                             allEnums,
                                             allCommands));

                    features.Add(GlApi.Parse(featureElem,
                                             GetParentApi(features, family, version),
                                             GlProfileType.Core,
                                             allEnums,
                                             allCommands));
                }
                else
                {
                    features.Add(GlApi.Parse(featureElem,
                                             GetParentApi(features, family, version),
                                             GlProfileType.None,
                                             allEnums,
                                             allCommands));
                }
            }

            return features;
        }

        private static IGlApi GetParentApi(IEnumerable<IGlApi> features, GlApiFamily family, Version version)
        {
            return features.OrderBy(f => f.Version).FirstOrDefault(f => f.ApiFamily == family);

        }

        private static IReadOnlyList<IGlCommand> ParseCommands(XDocument doc)
        {
            var commands = new List<IGlCommand>();

            foreach (var commandsElem in doc.Root.Elements("commands"))
            {
                commands.AddRange(commandsElem.Elements("command").Select(GlCommand.Parse));
            }

            return commands;
        }

        private static IReadOnlyList<IGlEnumeration> ParseEnums(XDocument doc)
        {
            var enumerations = new List<IGlEnumeration>();

            foreach (var enumsElem in doc.Root.Elements("enums"))
            {
                enumerations.AddRange(enumsElem.Elements("enum").Select(GlEnumeration.Parse));
            }

            return enumerations;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlApi : IGlApi
    {
        public GlApi(
            GlApiFamily apiFamily,
            GlProfileType profileType,
            Version version,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands)
            : this(apiFamily, profileType, version, enumerations, commands, null)
        {}

        public GlApi(
            GlApiFamily apiFamily,
            GlProfileType profileType,
            Version version,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands,
            IGlApi parentApi)
            : this(apiFamily, profileType, version, enumerations, commands, parentApi, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>())
        {}

        public GlApi(
            GlApiFamily apiFamily,
            GlProfileType profileType,
            Version version,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands,
            IGlApi parentApi,
            IEnumerable<IGlEnumeration> enumerationsToRemove,
            IEnumerable<IGlCommand> commandsToRemove)
        {
            ApiFamily = apiFamily;
            ProfileType = profileType;
            Version = version;

            if (parentApi != null)
            {
                if (version.CompareTo(parentApi.Version) <= 0)
                {
                    throw new GlInvalidParentFeatureException();
                }

                enumerations = parentApi.Enumerations.Union(enumerations).Except(enumerationsToRemove);
                commands = parentApi.Commands.Union(commands).Except(commandsToRemove);
            }

            Enumerations = new List<IGlEnumeration>(enumerations);
            Commands = new List<IGlCommand>(commands);
        }

        public GlApiFamily ApiFamily { get; }
        public GlProfileType ProfileType { get; }
        public string Name { get; }
        public Version Version { get; }
        public IReadOnlyList<IGlEnumeration> Enumerations { get; }
        public IReadOnlyList<IGlCommand> Commands { get; }

        public static IGlApi Parse(
            XElement element,
            IEnumerable<IGlEnumeration> enumerations,
            IEnumerable<IGlCommand> commands,
            IGlApi parentApi = null)
        {
            var api = (GlApiFamily)Enum.Parse(typeof(GlApiFamily), element.Attribute("api").Value, true);
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

            return new GlApi(api, GlProfileType.Compatibility, version, enumerationsToAdd, commandsToAdd, parentApi, enumerationsToRemove, commandsToRemove);
        }
    }
}

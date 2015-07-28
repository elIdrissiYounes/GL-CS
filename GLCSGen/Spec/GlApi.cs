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
        {
            ApiFamily = apiFamily;
            ProfileType = profileType;
            Version = version;
            Enumerations = new List<IGlEnumeration>(enumerations);
            Commands = new List<IGlCommand>(commands);
        }

        public GlApiFamily ApiFamily { get; }
        public GlProfileType ProfileType { get; }
        public Version Version { get; }
        public IReadOnlyList<IGlEnumeration> Enumerations { get; }
        public IReadOnlyList<IGlCommand> Commands { get; }

        public static IGlApi ParseOpenGl1(
            XElement element,
            IReadOnlyDictionary<string, IEnumerable<IGlEnumeration>> allEnumerationsByGroup,
            IEnumerable<IGlCommand> allCommands)
        {
            if (element.Attribute("api").Value != "gl" ||
                element.Attribute("number").Value != "1.0")
            {
                throw new GlFeatureNotOpenGl1Exception();
            }

            var commandsToAdd = new List<IGlCommand>();
            foreach (var requireElement in element.Elements("require"))
            {
                commandsToAdd.AddRange(requireElement.Elements("command")
                                                     .Select(e => e.Attribute("name").Value)
                                                     .Select(n => allCommands.First(c => c.Name == n)));
            }

            var enumerationsToAdd = new List<IGlEnumeration>();
            foreach (var parameter in from command in commandsToAdd
                                      from parameter in command.Parameters
                                      where parameter.Type.Base == GlTypeBase.Enum && !string.IsNullOrEmpty(parameter.Group)
                                      select parameter)
            {
                enumerationsToAdd.AddRange(allEnumerationsByGroup[parameter.Group]);
            }

            return new GlApi(GlApiFamily.Gl, GlProfileType.None, new Version(1, 0), enumerationsToAdd, commandsToAdd);
        }

        public static IGlApi Parse(
            XElement element,
            IGlApi parentApi,
            GlProfileType profileType,
            IEnumerable<IGlEnumeration> allEnumerations,
            IEnumerable<IGlCommand> allCommands)
        {
            var apiFamily = GetApiFamily(element);
            var version = GetVersion(element);

            if (parentApi != null)
            {
                if (parentApi.Version.CompareTo(version) >= 0 || parentApi.ApiFamily != apiFamily)
                {
                    throw new GlInvalidParentApiException();
                }
            }

            var enumerations = new List<IGlEnumeration>();
            var commands = new List<IGlCommand>();

            if (parentApi != null)
            {
                enumerations.AddRange(parentApi.Enumerations);
                commands.AddRange(parentApi.Commands);
            }

            foreach (var requireElement in element.Elements("require"))
            {
                enumerations.AddRange(requireElement.Elements("enum")
                                                    .Select(e => e.Attribute("name").Value)
                                                    .Select(n => allEnumerations.First(e => e.Name == n)));

                commands.AddRange(requireElement.Elements("command")
                                                .Select(e => e.Attribute("name").Value)
                                                .Select(n => allCommands.First(c => c.Name == n)));
            }

            foreach (var removeElement in element.Elements("remove"))
            {
                foreach (var removeEnum in removeElement.Elements("enum")
                                                        .Select(e => e.Attribute("name").Value)
                                                        .Select(n => enumerations.FirstOrDefault(e => e.Name == n))
                                                        .Where(removeEnum => removeEnum != null))
                {
                    enumerations.Remove(removeEnum);
                }

                foreach (var removeCommand in removeElement.Elements("command")
                                                           .Select(e => e.Attribute("name").Value)
                                                           .Select(n => commands.FirstOrDefault(c => c.Name == n))
                                                           .Where(removeCommand => removeCommand != null))
                {
                    commands.Remove(removeCommand);
                }
            }

            return new GlApi(apiFamily, profileType, version, enumerations, commands);
        }

        public static Version GetVersion(XElement element)
        {
            return Version.Parse(element.Attribute("number").Value);
        }

        public static GlApiFamily GetApiFamily(XElement element)
        {
            return element.Attribute("api").Value == "gl" ? GlApiFamily.Gl : GlApiFamily.GlEs;
        }
    }
}

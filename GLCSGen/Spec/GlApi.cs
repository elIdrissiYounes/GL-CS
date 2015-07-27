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
                throw new GlFeatureNotOpenGl1Exception("ParseOpenGl1 can only parse the 1.0 feature of OpenGL");
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
                                      where parameter.Type.BaseType == GlBaseType.Enum && !string.IsNullOrEmpty(parameter.Group)
                                      select parameter)
            {
                enumerationsToAdd.AddRange(allEnumerationsByGroup[parameter.Group]);
            }

            return new GlApi(GlApiFamily.Gl, GlProfileType.None, new Version(1, 0), enumerationsToAdd, commandsToAdd);
        }

        public static IGlApi Parse(
            XElement element,
            IEnumerable<IGlEnumeration> allEnumerations,
            IEnumerable<IGlCommand> allCommands,
            IGlApi parentApi,
            GlProfileType profile)
        {
            return null;
        }

        private static IGlApi Parse(
            XElement element,
            IEnumerable<IGlEnumeration> allEnumerations,
            IEnumerable<IGlCommand> allCommands,
            IGlApi parentApi = null)
        {
            var apiFamily = element.Attribute("api").Value == "gl" ? GlApiFamily.Gl : GlApiFamily.GlEs;

            var version = Version.Parse(element.Attribute("number").Value);

            var enumerationsToAdd = new List<IGlEnumeration>();
            var commandsToAdd = new List<IGlCommand>();

            var enumerationsToRemove = new List<IGlEnumeration>();
            var commandsToRemove = new List<IGlCommand>();

            foreach (var requireElement in element.Elements("require"))
            {
                enumerationsToAdd.AddRange(requireElement.Elements("enum")
                                                         .Select(e => e.Attribute("name").Value)
                                                         .Select(n => allEnumerations.First(e => e.Name == n)));

                commandsToAdd.AddRange(requireElement.Elements("command")
                                                     .Select(e => e.Attribute("name").Value)
                                                     .Select(n => allCommands.First(c => c.Name == n)));
            }

            foreach (var removeElement in element.Elements("remove"))
            {
                enumerationsToRemove.AddRange(removeElement.Elements("enum")
                                                           .Select(e => e.Attribute("name").Value)
                                                           .Select(n => allEnumerations.First(e => e.Name == n)));

                commandsToRemove.AddRange(removeElement.Elements("command")
                                                       .Select(e => e.Attribute("name").Value)
                                                       .Select(n => allCommands.First(c => c.Name == n)));
            }

            return new GlApi(apiFamily, GlProfileType.Compatibility, version, enumerationsToAdd, commandsToAdd);
        }
    }
}

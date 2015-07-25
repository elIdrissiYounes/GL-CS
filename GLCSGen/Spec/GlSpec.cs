using System;
using System.Collections.Generic;
using System.Globalization;
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
                commands.AddRange(commandsNode.Elements("command").Select(ParseCommand));
            }

            return commands;
        }

        private static IGlCommand ParseCommand(XElement commandNode)
        {
            var name = commandNode.Element("proto")?.Element("name")?.Value;
            var returnType = (GlType)Enum.Parse(typeof (GlType), commandNode.Element("proto")?.Value.Replace(name, "").Trim(), true);
            var parameters = commandNode.Elements("param").Select(ParseCommandParameter).ToList();
            var glCommand = new GlCommand(name, returnType, parameters);
            return glCommand;
        }

        private static IGlParameter ParseCommandParameter(XElement paramNode)
        {
            var group = paramNode.Attribute("group")?.Value;
            var type = paramNode.Element("ptype")?.Value;
            if (type != null && type.StartsWith("GL"))
            {
                type = type.Substring(2);
            }
            var paramName = paramNode.Element("name")?.Value.Trim();

            var glParameter = new GlParameter((GlType)Enum.Parse(typeof (GlType), type, true), @group, paramName);
            return glParameter;
        }

        private static IReadOnlyList<IGlEnumeration> ParseEnums(XDocument doc)
        {
            var enumerations = new List<IGlEnumeration>();

            foreach (var enumsNode in doc.Root.Elements("enums"))
            {
                enumerations.AddRange(enumsNode.Elements("enum").Select(ParseEnum));
            }

            return enumerations;
        }

        private static IGlEnumeration ParseEnum(XElement enumNode)
        {
            var name = enumNode.Attribute("name")?.Value;
            var value = enumNode.Attribute("value")?.Value;

            var numericValue = value.StartsWith("0x")
                                   ? uint.Parse(value.Substring(2), NumberStyles.HexNumber)
                                   : uint.Parse(value);
            return new GlEnumeration(name, numericValue);
        }
    }
}

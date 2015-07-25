using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlSpec : IGlSpec
    {
        private readonly List<IGlCommand> _commands = new List<IGlCommand>();
        private readonly List<IGlEnumeration> _enumerations = new List<IGlEnumeration>();

        public GlSpec(XDocument doc)
        {
            ParseEnums(doc);
            ParseCommands(doc);
        }

        public IReadOnlyList<IGlEnumeration> Enumerations => _enumerations;
        public IReadOnlyList<IGlCommand> Commands => _commands;

        private void ParseCommands(XDocument doc)
        {
            foreach (var commandsNode in doc.Root.Elements("commands"))
            {
                foreach (var commandNode in commandsNode.Elements("command"))
                {
                    _commands.Add(ParseCommand(commandNode));
                }
            }
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

        private void ParseEnums(XDocument doc)
        {
            foreach (var enumsNode in doc.Root.Elements("enums"))
            {
                foreach (var enumNode in enumsNode.Elements("enum"))
                {
                    _enumerations.Add(ParseEnum(enumNode));
                }
            }
        }

        private IGlEnumeration ParseEnum(XElement enumNode)
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

using System;
using System.Globalization;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlEnumeration : IGlEnumeration
    {
        public GlEnumeration(string name, int value)
        {
            Name = name;
            Int32Value = value;
        }

        public GlEnumeration(string name, uint value)
        {
            Name = name;
            UInt32Value = value;
        }

        public GlEnumeration(string name, ulong value)
        {
            Name = name;
            UInt64Value = value;
        }

        public string Name { get; }
        public int? Int32Value { get; }
        public uint? UInt32Value { get; }
        public ulong? UInt64Value { get; }

        public static IGlEnumeration Parse(XElement enumNode)
        {
            var name = enumNode.Attribute("name").Value;
            var value = enumNode.Attribute("value").Value;

            var parseStyle = NumberStyles.Any;
            if (value.StartsWith("0x"))
            {
                value = value.Substring(2);
                parseStyle = NumberStyles.HexNumber;
            }

            uint uintValue;
            if (uint.TryParse(value, parseStyle, CultureInfo.InvariantCulture, out uintValue))
            {
                return new GlEnumeration(name, uintValue);
            }

            ulong ulongValue;
            if (ulong.TryParse(value, parseStyle, CultureInfo.InvariantCulture, out ulongValue))
            {
                return new GlEnumeration(name, ulongValue);
            }

            int intValue;
            if (int.TryParse(value, parseStyle, CultureInfo.InvariantCulture, out intValue))
            {
                return new GlEnumeration(name, intValue);
            }

            throw new InvalidOperationException("Unhandled enum value type!");
        }
    }
}

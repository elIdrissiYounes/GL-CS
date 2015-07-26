using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GLCSGen.Spec
{
    public class GlTypeDescription : IGlTypeDescription
    {
        private static readonly IReadOnlyDictionary<GlTypeModifier, string[]> ModifierSequences = new Dictionary<GlTypeModifier, string[]>
        {
            {GlTypeModifier.None, new string[] {}},
            {GlTypeModifier.Const, new[] {"const"}},
            {GlTypeModifier.Pointer, new[] {"*"}},
            {GlTypeModifier.PointerToConst, new[] {"const", "*"}},
            {GlTypeModifier.ConstPointer, new[] {"*", "const"}},
            {GlTypeModifier.PointerToPointer, new[] {"*", "*"}},
            {GlTypeModifier.PointerToPointerToConst, new[] {"const", "*", "*"}},
            {GlTypeModifier.PointerToConstPointer, new[] {"*", "const", "*"}},
            {GlTypeModifier.ConstPointerToPointer, new[] {"*", "*", "const"}},
            {GlTypeModifier.ConstPointerToConstPointer, new[] {"*", "const", "*", "const"}},
            {GlTypeModifier.PointerToConstPointerToConst, new[] {"const", "*", "const", "*"}}
        };

        public GlTypeDescription(GlBaseType baseType, GlTypeModifier modifier)
        {
            BaseType = baseType;
            Modifier = modifier;
        }

        public GlBaseType BaseType { get; }
        public GlTypeModifier Modifier { get; }

        public static IGlTypeDescription Parse(string value)
        {
            var parts = Regex.Split(value, "([ \\*])")
                             .Select(m => m.Trim())
                             .Where(m => !string.IsNullOrEmpty(m) && !m.Equals("struct"))
                             .ToList();

            var baseType = GetBaseType(parts);
            var modifier = GetModifier(parts);

            return new GlTypeDescription(baseType, modifier);
        }

        private static GlTypeModifier GetModifier(IEnumerable<string> parts)
        {
            return Enum.GetValues(typeof(GlTypeModifier))
                       .Cast<GlTypeModifier>()
                       .FirstOrDefault(modifier => parts.SequenceEqual(ModifierSequences[modifier]));
        }

        private static GlBaseType GetBaseType(IList<string> parts)
        {
            string baseType;
            if (parts[0] == "const")
            {
                baseType = parts[1];
                parts.RemoveAt(1);
            }
            else
            {
                baseType = parts[0];
                parts.RemoveAt(0);
            }

            baseType = baseType.Replace("_", "");

            if (baseType.StartsWith("GL"))
            {
                baseType = baseType.Substring(2);
            }

            if (baseType.EndsWith("NV"))
            {
                baseType = baseType.Substring(0, baseType.Length - 2);
            }
            else if (baseType.EndsWith("ARB") ||
                     baseType.EndsWith("KHR") ||
                     baseType.EndsWith("EXT") ||
                     baseType.EndsWith("AMD"))
            {
                baseType = baseType.Substring(0, baseType.Length - 3);
            }

            return (GlBaseType)Enum.Parse(typeof(GlBaseType), baseType, true);
        }
    }
}

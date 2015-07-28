using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GLCSGen.Spec
{
    public class GlType : IGlType
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

        public GlType(GlTypeBase typeBase, GlTypeModifier modifier)
        {
            Base = typeBase;
            Modifier = modifier;
        }

        public GlTypeBase Base { get; }
        public GlTypeModifier Modifier { get; }

        public static IGlType Parse(string value)
        {
            var parts = Regex.Split(value, "([ \\*])")
                             .Select(m => m.Trim())
                             .Where(m => !string.IsNullOrEmpty(m) && !m.Equals("struct"))
                             .ToList();

            var baseType = GetBaseType(parts);
            var modifier = GetModifier(parts);

            return new GlType(baseType, modifier);
        }

        private static GlTypeModifier GetModifier(IEnumerable<string> parts)
        {
            return Enum.GetValues(typeof(GlTypeModifier))
                       .Cast<GlTypeModifier>()
                       .FirstOrDefault(modifier => parts.SequenceEqual(ModifierSequences[modifier]));
        }

        private static GlTypeBase GetBaseType(IList<string> parts)
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

            return (GlTypeBase)Enum.Parse(typeof(GlTypeBase), baseType, true);
        }

        public string ToCSharpType()
        {
            switch (Base)
            {
                case GlTypeBase.Bitfield:
                    return "uint";
                case GlTypeBase.Boolean:
                    return "bool";
                case GlTypeBase.Byte:
                    return "byte";
                case GlTypeBase.Char:
                {
                    if (Modifier == GlTypeModifier.PointerToConst)
                    {
                        return "string";
                    }

                    return "char";
                }
                case GlTypeBase.ClampD:
                    return "int";
                case GlTypeBase.ClampF:
                    return "float";
                case GlTypeBase.ClampX:
                    return "int";
                case GlTypeBase.ClContext:
                    return "IntPtr";
                case GlTypeBase.ClEvent:
                    return "IntPtr";
                case GlTypeBase.DebugProc:
                    return "DebugProc";
                case GlTypeBase.Double:
                    return "double";
                case GlTypeBase.EglImageOes:
                    return "IntPtr";
                case GlTypeBase.Enum:
                    return "uint";
                case GlTypeBase.Fixed:
                    return "int";
                case GlTypeBase.Float:
                    return "float";
                case GlTypeBase.Half:
                    return "float";
                case GlTypeBase.Handle:
                    return "IntPtr";
                case GlTypeBase.Int:
                    return "int";
                case GlTypeBase.Int64:
                    return "int64";
                case GlTypeBase.IntPtr:
                    return "int64";
                case GlTypeBase.Short:
                    return "short";
                case GlTypeBase.SizeI:
                    return "int";
                case GlTypeBase.SizeIPtr:
                    return "int64";
                case GlTypeBase.Sync:
                    return "IntPtr";
                case GlTypeBase.UByte:
                    return "ubyte";
                case GlTypeBase.UInt:
                    return "uint";
                case GlTypeBase.UInt64:
                    return "uint64";
                case GlTypeBase.UShort:
                    return "ushort";
                case GlTypeBase.VdpauSurface:
                    return "IntPtr";
                case GlTypeBase.Void:
                    return "void";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

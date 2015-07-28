using System;
using GLCSGen.Spec;

namespace GLCSGen
{
    public static class GlTypeBaseExtensions
    {
        public static string GetCSharpType(this GlTypeBase type)
        {
            switch (type)
            {
                case GlTypeBase.Bitfield:
                    return "uint";
                case GlTypeBase.Boolean:
                    return "bool";
                case GlTypeBase.Byte:
                    return "byte";
                case GlTypeBase.Char:
                    return "char";
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
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}

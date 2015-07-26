using System;

namespace GLCSGen.Spec
{
    internal static class GlTypeHelpers
    {
        public static GlType Parse(string type)
        {
            if (type.StartsWith("GL"))
            {
                type = type.Substring(2);
            }

            type = type.Replace(" ", "").Replace("*", "Ptr").Replace("_", "");
            return (GlType)Enum.Parse(typeof (GlType), type, true);
        }
    }
}

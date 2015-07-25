namespace GLCSGen.Spec
{
    public class GlParameter : IGlParameter
    {
        public GlType Type { get; }
        public string Group { get; }
        public string Name { get; }

        public GlParameter(GlType type, string group, string name)
        {
            Type = type;
            Group = group;
            Name = name;
        }
    }
}
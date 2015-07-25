namespace GLCSGen.Spec
{
    public class GlEnumeration : IGlEnumeration
    {
        public GlEnumeration(string name, uint value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public uint Value { get; }
    }
}
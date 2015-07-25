namespace GLCSGen.Spec
{
    public interface IGlParameter
    {
        GlType Type { get; }
        string Group { get; }
        string Name { get; }
    }
}
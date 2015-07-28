namespace GLCSGen.Spec
{
    public interface IGlParameter
    {
        IGlType Type { get; }
        string Group { get; }
        string Name { get; }
    }
}

namespace GLCSGen.Spec
{
    public interface IGlParameter
    {
        IGlTypeDescription Type { get; }
        string Group { get; }
        string Name { get; }
    }
}
namespace GLCSGen.Spec
{
    public interface IGlTypeDescription
    {
        GlBaseType BaseType { get; }
        GlTypeModifier Modifier { get; }
    }
}
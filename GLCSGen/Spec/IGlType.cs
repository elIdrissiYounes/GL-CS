namespace GLCSGen.Spec
{
    public interface IGlType
    {
        GlTypeBase Base { get; }
        GlTypeModifier Modifier { get; }
        string ToCSharpType();
    }
}

namespace GLCSGen.Spec
{
    public interface IGlEnumeration
    {
        string Name { get; }
        int? Int32Value { get; }
        uint? UInt32Value { get; }
        ulong? UInt64Value { get; }
    }
}
using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlCommand
    {
        string Name { get; }
        IGlTypeDescription ReturnType { get; set; }
        IReadOnlyList<IGlParameter> Parameters { get; set; }
    }
}
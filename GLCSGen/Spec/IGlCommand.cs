using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlCommand
    {
        string Name { get; }
        IGlType ReturnType { get; set; }
        IReadOnlyList<IGlParameter> Parameters { get; set; }
    }
}

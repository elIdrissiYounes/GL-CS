using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlCommand
    {
        string Name { get; }
        GlType ReturnType { get; set; }
        IReadOnlyList<IGlParameter> Parameters { get; set; }
    }
}
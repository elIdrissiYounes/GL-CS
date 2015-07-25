using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlSpec
    {
        IReadOnlyList<IGlEnumeration> Enumerations { get; }
        IReadOnlyList<IGlCommand> Commands { get; }
    }
}

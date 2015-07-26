using System;
using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlFeature
    {
        GlApi Api { get; }
        string Name { get; }
        Version Version { get; }
        IReadOnlyList<IGlEnumeration> Enumerations { get; }
        IReadOnlyList<IGlCommand> Commands { get; }
    }
}

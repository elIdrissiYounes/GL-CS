using System;
using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlApi
    {
        GlApiFamily ApiFamily { get; }
        GlProfileType ProfileType { get; }
        Version Version { get; }
        IReadOnlyList<IGlEnumeration> Enumerations { get; }
        IReadOnlyList<IGlCommand> Commands { get; }
    }
}

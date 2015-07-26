using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public interface IGlSpec
    {
        IReadOnlyList<IGlFeature> Features { get; }
    }
}

using System.Collections.Generic;

namespace GLCSGen.Spec
{
    public class GlCommand : IGlCommand
    {
        public GlCommand(string name, GlType returnType, IEnumerable<IGlParameter> parameters)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = new List<IGlParameter>(parameters);
        }

        public string Name { get; }
        public GlType ReturnType { get; set; }
        public IReadOnlyList<IGlParameter> Parameters { get; set; }
    }
}
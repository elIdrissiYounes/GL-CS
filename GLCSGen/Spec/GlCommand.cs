using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlCommand : IGlCommand
    {
        public GlCommand(string name, IGlTypeDescription returnType, IEnumerable<IGlParameter> parameters)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = new List<IGlParameter>(parameters);
        }

        public string Name { get; }
        public IGlTypeDescription ReturnType { get; set; }
        public IReadOnlyList<IGlParameter> Parameters { get; set; }

        public static IGlCommand Parse(XElement commandNode)
        {
            var name = commandNode.Element("proto").Element("name").Value;
            var returnType = commandNode.Element("proto").Nodes()
                                        .Where(node => node.NodeType == XmlNodeType.Text || (node as XElement)?.Name != "name")
                                        .Select(node => node is XText ? ((XText)node).Value : (node as XElement)?.Value)
                                        .Select(t => t.StartsWith("GL") ? t.Substring(2) : t)
                                        .Aggregate("", (a, b) => a + b);
            var parameters = commandNode.Elements("param").Select(GlParameter.Parse).ToList();
            var glCommand = new GlCommand(name, GlTypeDescription.Parse(returnType), parameters);
            return glCommand;
        }
    }
}

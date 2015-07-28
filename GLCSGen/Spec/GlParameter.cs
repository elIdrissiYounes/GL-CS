using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace GLCSGen.Spec
{
    public class GlParameter : IGlParameter
    {
        public GlParameter(IGlType type, string group, string name)
        {
            Type = type;
            Group = group;
            Name = name;
        }

        public IGlType Type { get; }
        public string Group { get; }
        public string Name { get; }

        public static IGlParameter Parse(XElement paramNode)
        {
            var group = paramNode.Attribute("group")?.Value;
            var paramName = paramNode.Element("name").Value.Trim();
            var type = paramNode.Nodes()
                                .Where(node => node.NodeType == XmlNodeType.Text || (node as XElement)?.Name != "name")
                                .Select(node => node is XText ? ((XText)node).Value : (node as XElement)?.Value)
                                .Select(t => t.StartsWith("GL") ? t.Substring(2) : t)
                                .Aggregate("", (a, b) => a + b);

            return new GlParameter(GlType.Parse(type), group, paramName);
        }
    }
}

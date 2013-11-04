using System.Collections.Generic;
using System.Xml.Serialization;

namespace GLCSGen
{
    public class EnumDictionary : Dictionary<string, string>, IXmlSerializable
    {
        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            // we don't care about parsing our format back
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (var entry in this)
            {
                writer.WriteStartElement("Enum");
                writer.WriteAttributeString("Name", entry.Key);
                writer.WriteAttributeString("Value", entry.Value);
                writer.WriteEndElement();
            }
        }
    }
}

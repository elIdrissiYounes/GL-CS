using System.Xml.Linq;
using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class GlEnumerationTests
    {
        [Test]
        public void ParsesEnumNamesAndValues()
        {
            var value = GlEnumeration.Parse(XElement.Parse(@"<enum value=""0x00000001"" name=""GL_CURRENT_BIT""/>"));
            Assert.That(value.Name, Is.EqualTo("GL_CURRENT_BIT"));
            Assert.That(value.UInt32Value, Is.EqualTo(0x00000001));
        }

        [Test]
        public void ParsesEnumsWithLongValues()
        {
            var value = GlEnumeration.Parse(XElement.Parse(@"<enum value=""0xFFFFFFFFFFFFFFFF"" name=""GL_CURRENT_BIT""/>"));
            Assert.That(value.Name, Is.EqualTo("GL_CURRENT_BIT"));
            Assert.That(value.UInt64Value, Is.EqualTo(0xFFFFFFFFFFFFFFFF));
        }

        [Test]
        public void ParsesEnumsWithNegativeValues()
        {
            var value = GlEnumeration.Parse(XElement.Parse(@"<enum value=""-2"" name=""GL_CURRENT_BIT""/>"));
            Assert.That(value.Name, Is.EqualTo("GL_CURRENT_BIT"));
            Assert.That(value.Int32Value, Is.EqualTo(-2));
        }
    }
}

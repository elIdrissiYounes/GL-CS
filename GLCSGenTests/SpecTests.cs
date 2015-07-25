using System.Xml.Linq;
using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class SpecTests
    {
        [Test]
        public void Commands_ReturnsParsedCommandInfo()
        {
            var doc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<registry>
    <commands namespace=""GL"">
        <command>
            <proto>void <name>glAccum</name></proto>
            <param group=""AccumOp""><ptype>GLenum</ptype> <name>op</name></param>
            <param group=""CoordF""><ptype>GLfloat</ptype> <name> value </name></param>
            <glx type=""render"" opcode=""137"" />
        </command>
    </commands>
</registry>");

            var glSpec = new GlSpec(doc);

            Assert.That(glSpec.Commands, Has.Count.EqualTo(1));

            var cmd = glSpec.Commands[0];
            Assert.That(cmd.Name, Is.EqualTo("glAccum"));
            Assert.That(cmd.ReturnType, Is.EqualTo(GlType.Void));
            Assert.That(cmd.Parameters, Has.Count.EqualTo(2));
            Assert.That(cmd.Parameters[0].Type, Is.EqualTo(GlType.Enum));
            Assert.That(cmd.Parameters[0].Group, Is.EqualTo("AccumOp"));
            Assert.That(cmd.Parameters[0].Name, Is.EqualTo("op"));
            Assert.That(cmd.Parameters[1].Type, Is.EqualTo(GlType.Float));
            Assert.That(cmd.Parameters[1].Group, Is.EqualTo("CoordF"));
            Assert.That(cmd.Parameters[1].Name, Is.EqualTo("value"));
        }

        [Test]
        public void Enumerations_ReturnsCorrectEnumValues()
        {
            var doc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<registry>
    <enums namespace=""GL"" group=""AttribMask"" type=""bitmask"">
        <enum value=""0x00000001"" name=""GL_CURRENT_BIT""/>
        <enum value=""0x00000002"" name=""GL_POINT_BIT""/>
    </enums>
</registry>");

            var glSpec = new GlSpec(doc);

            Assert.That(glSpec.Enumerations, Has.Count.EqualTo(2));
            Assert.That(glSpec.Enumerations[0].Name, Is.EqualTo("GL_CURRENT_BIT"));
            Assert.That(glSpec.Enumerations[0].Value, Is.EqualTo(0x00000001));
            Assert.That(glSpec.Enumerations[1].Name, Is.EqualTo("GL_POINT_BIT"));
            Assert.That(glSpec.Enumerations[1].Value, Is.EqualTo(0x00000002));
        }
    }
}

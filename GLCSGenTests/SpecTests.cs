using System.Xml.Linq;
using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class SpecTests
    {
        [Test]
        public void ParsesCommandInformation()
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
            Assert.That(cmd.ReturnType.BaseType, Is.EqualTo(GlBaseType.Void));
            Assert.That(cmd.ReturnType.Modifier, Is.EqualTo(GlTypeModifier.None));
            Assert.That(cmd.Parameters, Has.Count.EqualTo(2));
            Assert.That(cmd.Parameters[0].Type.BaseType, Is.EqualTo(GlBaseType.Enum));
            Assert.That(cmd.Parameters[0].Type.Modifier, Is.EqualTo(GlTypeModifier.None));
            Assert.That(cmd.Parameters[0].Group, Is.EqualTo("AccumOp"));
            Assert.That(cmd.Parameters[0].Name, Is.EqualTo("op"));
            Assert.That(cmd.Parameters[1].Type.BaseType, Is.EqualTo(GlBaseType.Float));
            Assert.That(cmd.Parameters[1].Type.Modifier, Is.EqualTo(GlTypeModifier.None));
            Assert.That(cmd.Parameters[1].Group, Is.EqualTo("CoordF"));
            Assert.That(cmd.Parameters[1].Name, Is.EqualTo("value"));
        }

        [Test]
        public void ParsesEnumNamesAndValues()
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
            Assert.That(glSpec.Enumerations[0].UInt32Value, Is.EqualTo(0x00000001));
            Assert.That(glSpec.Enumerations[1].Name, Is.EqualTo("GL_POINT_BIT"));
            Assert.That(glSpec.Enumerations[1].UInt32Value, Is.EqualTo(0x00000002));
        }

        [Test]
        public void ParsesEnumsWithNegativeValues()
        {
            var doc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<registry>
    <enums namespace=""GL"" group=""AttribMask"" type=""bitmask"">
        <enum value=""-2"" name=""GL_CURRENT_BIT""/>
    </enums>
</registry>");

            var glSpec = new GlSpec(doc);

            Assert.That(glSpec.Enumerations, Has.Count.EqualTo(1));
            Assert.That(glSpec.Enumerations[0].Name, Is.EqualTo("GL_CURRENT_BIT"));
            Assert.That(glSpec.Enumerations[0].Int32Value, Is.EqualTo(-2));
        }

        [Test]
        public void ParsesEnumsWithLongValues()
        {
            var doc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<registry>
    <enums namespace=""GL"" group=""AttribMask"" type=""bitmask"">
        <enum value=""0xFFFFFFFFFFFFFFFF"" name=""GL_CURRENT_BIT""/>
    </enums>
</registry>");

            var glSpec = new GlSpec(doc);

            Assert.That(glSpec.Enumerations, Has.Count.EqualTo(1));
            Assert.That(glSpec.Enumerations[0].Name, Is.EqualTo("GL_CURRENT_BIT"));
            Assert.That(glSpec.Enumerations[0].UInt64Value, Is.EqualTo(0xFFFFFFFFFFFFFFFF));
        }
    }
}

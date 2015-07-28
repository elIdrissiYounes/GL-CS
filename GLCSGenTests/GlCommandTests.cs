using System.Xml.Linq;
using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class GlCommandTests
    {
        [Test]
        public void ParsesCommandInformation()
        {
            var commandNode = XElement.Parse(@"
                <command>
                    <proto>void <name>glAccum</name></proto>
                    <param group=""AccumOp""><ptype>GLenum</ptype> <name>op</name></param>
                    <param group=""CoordF""><ptype>GLfloat</ptype> <name> value </name></param>
                    <glx type=""render"" opcode=""137"" />
                </command>");

            var cmd = GlCommand.Parse(commandNode);
            Assert.That(cmd.Name, Is.EqualTo("glAccum"));
            Assert.That(cmd.ReturnType.Base, Is.EqualTo(GlTypeBase.Void));
            Assert.That(cmd.ReturnType.Modifier, Is.EqualTo(GlTypeModifier.None));
            Assert.That(cmd.Parameters, Has.Count.EqualTo(2));
            Assert.That(cmd.Parameters[0].Type.Base, Is.EqualTo(GlTypeBase.Enum));
            Assert.That(cmd.Parameters[0].Type.Modifier, Is.EqualTo(GlTypeModifier.None));
            Assert.That(cmd.Parameters[0].Group, Is.EqualTo("AccumOp"));
            Assert.That(cmd.Parameters[0].Name, Is.EqualTo("op"));
            Assert.That(cmd.Parameters[1].Type.Base, Is.EqualTo(GlTypeBase.Float));
            Assert.That(cmd.Parameters[1].Type.Modifier, Is.EqualTo(GlTypeModifier.None));
            Assert.That(cmd.Parameters[1].Group, Is.EqualTo("CoordF"));
            Assert.That(cmd.Parameters[1].Name, Is.EqualTo("value"));
        }
    }
}

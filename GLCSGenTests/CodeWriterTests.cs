using System;
using System.IO;
using System.Text;
using GLCSGen;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class CodeWriterTests
    {
        [SetUp]
        public void SetUp()
        {
            _stringBuilder = new StringBuilder();
            _stringWriter = new StringWriter(_stringBuilder);
            _codeWriter = new CodeWriter(_stringWriter);
        }

        private StringBuilder _stringBuilder;
        private StringWriter _stringWriter;
        private CodeWriter _codeWriter;

        [Test]
        public void DedentAndWriteCloseBrace_ProperlyDecrementsTabCountAndInsertsBrace()
        {
            _codeWriter.TabCount = 2;

            _codeWriter.DedentAndWriteCloseBrace();
            Assert.That(_codeWriter.TabCount, Is.EqualTo(1));
            Assert.That(_stringBuilder.ToString(), Is.EqualTo("    }" + Environment.NewLine));

            _codeWriter.DedentAndWriteCloseBrace();
            Assert.That(_codeWriter.TabCount, Is.EqualTo(0));
            Assert.That(_stringBuilder.ToString(), Is.EqualTo("    }" + Environment.NewLine + "}" + Environment.NewLine));
        }

        [Test]
        public void TabCount_CannotBeNegative()
        {
            _codeWriter.TabCount = -1;
            Assert.That(_codeWriter.TabCount, Is.EqualTo(0));
        }

        [Test]
        public void TabCount_DefaultsToZero()
        {
            Assert.That(_codeWriter.TabCount, Is.EqualTo(0));
        }

        [Test]
        public void WriteLine_AddsTabSpacingBeforeContentWhenTabCountIsGreaterThanZero()
        {
            _codeWriter.WriteLine("Test1");
            _codeWriter.TabCount = 1;
            _codeWriter.WriteLine("Test2");
            Assert.That(_stringBuilder.ToString(), Is.EqualTo(string.Format("Test1{0}    Test2{0}", Environment.NewLine)));
        }

        [Test]
        public void WriteLine_WritesAProperNewLine()
        {
            _codeWriter.WriteLine();
            Assert.That(_stringBuilder.ToString(), Is.EqualTo(Environment.NewLine));
        }

        [Test]
        public void WriteOpenBraceAndIndent_ProperlyInsertsBraceAndIncrementsTabCount()
        {
            _codeWriter.WriteOpenBraceAndIndent();
            Assert.That(_codeWriter.TabCount, Is.EqualTo(1));
            Assert.That(_stringBuilder.ToString(), Is.EqualTo("{" + Environment.NewLine));

            _codeWriter.WriteOpenBraceAndIndent();
            Assert.That(_codeWriter.TabCount, Is.EqualTo(2));
            Assert.That(_stringBuilder.ToString(), Is.EqualTo("{" + Environment.NewLine + "    {" + Environment.NewLine));
        }
    }
}

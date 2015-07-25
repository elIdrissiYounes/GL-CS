using System;
using System.IO;

namespace GLCSGen
{
    public sealed class CodeWriter
    {
        private readonly TextWriter _writer;
        private int _tabCount;
        private string _tabString = string.Empty;

        public int TabCount
        {
            get { return _tabCount; }
            set
            {
                _tabCount = Math.Max(value, 0);
                _tabString = new string(' ', _tabCount * 4);
            }
        }

        public CodeWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public void WriteLine()
        {
            _writer.WriteLine();
        }

        public void WriteLine(string line)
        {
            _writer.Write(_tabString);
            _writer.WriteLine(line);
        }

        public void WriteLine(string format, params object[] args)
        {
            _writer.Write(_tabString);
            _writer.WriteLine(format, args);
        }

        public void WriteOpenBraceAndIndent()
        {
            WriteLine("{");
            TabCount++;
        }

        public void DedentAndWriteCloseBrace()
        {
            TabCount--;
            WriteLine("}");
        }
    }
}

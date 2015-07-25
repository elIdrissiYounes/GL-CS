using System;
using System.IO;

namespace GLCSGen
{
    internal class CodeWriter : IDisposable
    {
        private readonly StreamWriter _writer;
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

        public CodeWriter(string filename)
        {
            _writer = new StreamWriter(filename);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CodeWriter()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            _writer.Dispose();
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

        public void WriteOpenBrace()
        {
            WriteLine("{");
            TabCount++;
        }

        public void WriteCloseBrace()
        {
            TabCount--;
            WriteLine("}");
        }
    }
}

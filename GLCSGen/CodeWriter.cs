using System;
using System.IO;

namespace GLCSGen
{
    class CodeWriter : IDisposable
    {
        private readonly StreamWriter writer;

        private int tabCount = 0;
        private string tabString = "";

        public int TabCount
        {
            get { return tabCount; }
            set
            {
                tabCount = Math.Max(value, 0);
                tabString = new string(' ', tabCount * 4);
            }
        }

        public CodeWriter(string filename)
        {
            writer = new StreamWriter(filename);
        }

        ~CodeWriter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            writer.Dispose();
        }

        public void WriteLine()
        {
            writer.WriteLine();
        }

        public void WriteLine(string line)
        {
            writer.Write(tabString);
            writer.WriteLine(line);
        }

        public void WriteLine(string format, params object[] args)
        {
            writer.Write(tabString);
            writer.WriteLine(format, args);
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

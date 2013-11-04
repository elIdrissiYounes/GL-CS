/*
 * The MIT License (MIT)
 * Copyright (c) 2013, Nick Gravelyn.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

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

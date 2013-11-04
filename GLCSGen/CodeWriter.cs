/*
 * Copyright (c) 2013, Nick Gravelyn.
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 *    1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 * 
 *    2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 * 
 *    3. This notice may not be removed or altered from any source
 *    distribution.
 * 
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

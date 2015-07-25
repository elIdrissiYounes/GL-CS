using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static OpenGL.GL21;
using static SDL2.SDL;

namespace GLCSTest
{
    internal class Program
    {
        private static void Main()
        {
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 2);
            SDL_GL_SetAttribute(SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 1);

            var window = SDL_CreateWindow("GL-CS Test",
                                          SDL_WINDOWPOS_CENTERED,
                                          SDL_WINDOWPOS_CENTERED,
                                          960,
                                          540,
                                          SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL_WindowFlags.SDL_WINDOW_SHOWN);

            var context = SDL_GL_CreateContext(window);
            SDL_GL_MakeCurrent(window, context);

            // Set the GetProcAddress delegate for OpenGL to be able to get function pointers
            GetProcAddress = SDL_GL_GetProcAddress;

            // Load an individual function so we can get the version string
            LoadFunction("glGetString");
            Console.WriteLine("OpenGL Version: {0}", Marshal.PtrToStringAnsi(glGetString(GL_VERSION)));

            // Now load the rest of the functions in one go
            LoadAllFunctions();

            var watch = new Stopwatch();
            watch.Start();

            var running = true;
            while (running)
            {
                SDL_Event evt;
                while (SDL_PollEvent(out evt) != 0)
                {
                    if (evt.type == SDL_EventType.SDL_QUIT)
                    {
                        running = false;
                    }
                }

                watch.Restart();

                glClearColor(0f, 0f, 0f, 1f);
                glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

                glBegin(GL_TRIANGLES);

                glColor3f(1, 0, 0);
                glVertex3f(-.5f, -.5f, 0);

                glColor3f(0, 1, 0);
                glVertex3f(0, .5f, 0);

                glColor3f(0, 0, 1);
                glVertex3f(.5f, -.5f, 0);

                glEnd();

                glFlush();
                SDL_GL_SwapWindow(window);
            }

            SDL_DestroyWindow(window);
        }
    }
}

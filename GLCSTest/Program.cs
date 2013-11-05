using SDL2;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GL = OpenGL.GL21;

namespace GLCSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 2);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 1);

            IntPtr window = SDL.SDL_CreateWindow(
                "GL-CS Test",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                960,
                540,
                SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            IntPtr context = SDL.SDL_GL_CreateContext(window);
            SDL.SDL_GL_MakeCurrent(window, context);

            // Set the GetProcAddress delegate for OpenGL to be able to get function pointers
            GL.GetProcAddress = SDL.SDL_GL_GetProcAddress;

            // Load an individual function so we can get the version string
            GL.LoadFunction("glGetString");
            Console.WriteLine("OpenGL Version: {0}", Marshal.PtrToStringAnsi(GL.glGetString(GL.GL_VERSION)));

            // Now load the rest of the functions in one go
            GL.LoadAllFunctions();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            double totalTime = 0;
            bool running = true;
            while (running)
            {
                SDL.SDL_Event evt;
                while (SDL.SDL_PollEvent(out evt) != 0)
                {
                    if (evt.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        running = false;
                    }
                }

                totalTime += watch.Elapsed.TotalSeconds;
                watch.Restart();

                GL.glClearColor(0f, 0f, 0f, 1f);
                GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);


                GL.glBegin(GL.GL_TRIANGLES);

                GL.glColor3f(1, 0, 0); GL.glVertex3f(-.5f, -.5f, 0);
                GL.glColor3f(0, 1, 0); GL.glVertex3f(0, .5f, 0);
                GL.glColor3f(0, 0, 1); GL.glVertex3f(.5f, -.5f, 0);

                GL.glEnd();


                GL.glFlush();
                SDL.SDL_GL_SwapWindow(window);
            }

            SDL.SDL_DestroyWindow(window);
        }
    }
}

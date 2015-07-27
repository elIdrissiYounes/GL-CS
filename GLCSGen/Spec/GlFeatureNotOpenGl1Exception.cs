using System;

namespace GLCSGen.Spec
{
    public class GlFeatureNotOpenGl1Exception : Exception
    {
        public GlFeatureNotOpenGl1Exception(string message)
            : base(message)
        {
        }
    }
}
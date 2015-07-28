using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class GlTypeTests
    {
        [TestCase("void", GlTypeModifier.None)]
        [TestCase("void const", GlTypeModifier.Const)]
        [TestCase("const void", GlTypeModifier.Const)]
        [TestCase("void*", GlTypeModifier.Pointer)]
        [TestCase("void *", GlTypeModifier.Pointer)]
        [TestCase("void*const", GlTypeModifier.ConstPointer)]
        [TestCase("void *const", GlTypeModifier.ConstPointer)]
        [TestCase("void* const", GlTypeModifier.ConstPointer)]
        [TestCase("void * const", GlTypeModifier.ConstPointer)]
        [TestCase("void const*", GlTypeModifier.PointerToConst)]
        [TestCase("void const *", GlTypeModifier.PointerToConst)]
        [TestCase("const void*", GlTypeModifier.PointerToConst)]
        [TestCase("const void *", GlTypeModifier.PointerToConst)]
        [TestCase("void**", GlTypeModifier.PointerToPointer)]
        [TestCase("void **", GlTypeModifier.PointerToPointer)]
        [TestCase("void* *", GlTypeModifier.PointerToPointer)]
        [TestCase("void * *", GlTypeModifier.PointerToPointer)]
        [TestCase("void const**", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("void const **", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("void const* *", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("void const * *", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("const void**", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("const void **", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("const void* *", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("const void * *", GlTypeModifier.PointerToPointerToConst)]
        [TestCase("void*const*", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void *const*", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void* const*", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void*const *", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void * const*", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void *const *", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void * const *", GlTypeModifier.PointerToConstPointer)]
        [TestCase("void**const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void **const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void* *const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void** const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void * *const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void ** const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void * * const", GlTypeModifier.ConstPointerToPointer)]
        [TestCase("void*const*const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void *const*const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void* const*const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void*const *const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void*const* const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void * const*const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void *const *const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void *const* const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void * const *const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void * const* const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void * const * const", GlTypeModifier.ConstPointerToConstPointer)]
        [TestCase("void const*const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("void const *const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("void const* const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("void const*const *", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("void const * const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("void const *const *", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("void const * const *", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void*const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void *const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void* const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void*const *", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void * const*", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void *const *", GlTypeModifier.PointerToConstPointerToConst)]
        [TestCase("const void * const *", GlTypeModifier.PointerToConstPointerToConst)]
        public void CanParseModifierCorrectly(
            string value,
            GlTypeModifier glTypeModifier)
        {
            var type = GlType.Parse(value);
            Assert.That(type.Modifier, Is.EqualTo(glTypeModifier));
        }

        [TestCase("bitfield", GlTypeBase.Bitfield)]
        [TestCase("GLbitfield", GlTypeBase.Bitfield)]
        [TestCase("boolean", GlTypeBase.Boolean)]
        [TestCase("GLboolean", GlTypeBase.Boolean)]
        [TestCase("byte", GlTypeBase.Byte)]
        [TestCase("GLbyte", GlTypeBase.Byte)]
        [TestCase("char", GlTypeBase.Char)]
        [TestCase("GLchar", GlTypeBase.Char)]
        [TestCase("clampd", GlTypeBase.ClampD)]
        [TestCase("GLclampd", GlTypeBase.ClampD)]
        [TestCase("clampf", GlTypeBase.ClampF)]
        [TestCase("GLclampf", GlTypeBase.ClampF)]
        [TestCase("clampx", GlTypeBase.ClampX)]
        [TestCase("GLclampx", GlTypeBase.ClampX)]
        [TestCase("struct cl_context", GlTypeBase.ClContext)]
        [TestCase("struct cl_event", GlTypeBase.ClEvent)]
        [TestCase("debugproc", GlTypeBase.DebugProc)]
        [TestCase("DEBUGPROC", GlTypeBase.DebugProc)]
        [TestCase("DEBUGPROCKHR", GlTypeBase.DebugProc)]
        [TestCase("DEBUGPROCARB", GlTypeBase.DebugProc)]
        [TestCase("DEBUGPROCAMD", GlTypeBase.DebugProc)]
        [TestCase("double", GlTypeBase.Double)]
        [TestCase("GLdouble", GlTypeBase.Double)]
        [TestCase("eglimageoes", GlTypeBase.EglImageOes)]
        [TestCase("GLeglimageoes", GlTypeBase.EglImageOes)]
        [TestCase("enum", GlTypeBase.Enum)]
        [TestCase("GLenum", GlTypeBase.Enum)]
        [TestCase("fixed", GlTypeBase.Fixed)]
        [TestCase("GLfixed", GlTypeBase.Fixed)]
        [TestCase("float", GlTypeBase.Float)]
        [TestCase("GLfloat", GlTypeBase.Float)]
        [TestCase("half", GlTypeBase.Half)]
        [TestCase("halfNV", GlTypeBase.Half)]
        [TestCase("GLhalf", GlTypeBase.Half)]
        [TestCase("GLhalfNV", GlTypeBase.Half)]
        [TestCase("handle", GlTypeBase.Handle)]
        [TestCase("HandleARB", GlTypeBase.Handle)]
        [TestCase("int", GlTypeBase.Int)]
        [TestCase("GLint", GlTypeBase.Int)]
        [TestCase("int64", GlTypeBase.Int64)]
        [TestCase("GLint64", GlTypeBase.Int64)]
        [TestCase("short", GlTypeBase.Short)]
        [TestCase("GLshort", GlTypeBase.Short)]
        [TestCase("sizei", GlTypeBase.SizeI)]
        [TestCase("GLsizei", GlTypeBase.SizeI)]
        [TestCase("sizeiptr", GlTypeBase.SizeIPtr)]
        [TestCase("GLsizeiptr", GlTypeBase.SizeIPtr)]
        [TestCase("sync", GlTypeBase.Sync)]
        [TestCase("ubyte", GlTypeBase.UByte)]
        [TestCase("GLubyte", GlTypeBase.UByte)]
        [TestCase("uint", GlTypeBase.UInt)]
        [TestCase("GLuint", GlTypeBase.UInt)]
        [TestCase("uint64", GlTypeBase.UInt64)]
        [TestCase("uint64EXT", GlTypeBase.UInt64)]
        [TestCase("GLuint64", GlTypeBase.UInt64)]
        [TestCase("ushort", GlTypeBase.UShort)]
        [TestCase("GLushort", GlTypeBase.UShort)]
        [TestCase("vdpausurface", GlTypeBase.VdpauSurface)]
        [TestCase("vdpausurfaceNV", GlTypeBase.VdpauSurface)]
        [TestCase("void", GlTypeBase.Void)]
        [TestCase("GLvoid", GlTypeBase.Void)]
        public void CanParseBaseTypeCorrectly(
            string value,
            GlTypeBase glTypeBase)
        {
            var type = GlType.Parse(value);
            Assert.That(type.Base, Is.EqualTo(glTypeBase));
        }

        [TestCase(GlTypeBase.Void, GlTypeModifier.None, "void")]
        [TestCase(GlTypeBase.Int, GlTypeModifier.None, "int")]
        [TestCase(GlTypeBase.Char, GlTypeModifier.PointerToConst, "string")]
        public void CanGenerateCSharpTypeCorrectly(
            GlTypeBase typeBase,
            GlTypeModifier modifier,
            string expectedValue)
        {
            var type = new GlType(typeBase, modifier);
            Assert.That(type.ToCSharpType(), Is.EqualTo(expectedValue));
        }
    }
}

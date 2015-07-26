using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class GlTypeDescriptionTests
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
            var type = GlTypeDescription.Parse(value);
            Assert.That(type.Modifier, Is.EqualTo(glTypeModifier));
        }

        [TestCase("bitfield", GlBaseType.Bitfield)]
        [TestCase("GLbitfield", GlBaseType.Bitfield)]
        [TestCase("boolean", GlBaseType.Boolean)]
        [TestCase("GLboolean", GlBaseType.Boolean)]
        [TestCase("byte", GlBaseType.Byte)]
        [TestCase("GLbyte", GlBaseType.Byte)]
        [TestCase("char", GlBaseType.Char)]
        [TestCase("GLchar", GlBaseType.Char)]
        [TestCase("clampd", GlBaseType.ClampD)]
        [TestCase("GLclampd", GlBaseType.ClampD)]
        [TestCase("clampf", GlBaseType.ClampF)]
        [TestCase("GLclampf", GlBaseType.ClampF)]
        [TestCase("clampx", GlBaseType.ClampX)]
        [TestCase("GLclampx", GlBaseType.ClampX)]
        [TestCase("struct cl_context", GlBaseType.ClContext)]
        [TestCase("struct cl_event", GlBaseType.ClEvent)]
        [TestCase("debugproc", GlBaseType.DebugProc)]
        [TestCase("DEBUGPROC", GlBaseType.DebugProc)]
        [TestCase("DEBUGPROCKHR", GlBaseType.DebugProc)]
        [TestCase("DEBUGPROCARB", GlBaseType.DebugProc)]
        [TestCase("DEBUGPROCAMD", GlBaseType.DebugProc)]
        [TestCase("double", GlBaseType.Double)]
        [TestCase("GLdouble", GlBaseType.Double)]
        [TestCase("eglimageoes", GlBaseType.EglImageOes)]
        [TestCase("GLeglimageoes", GlBaseType.EglImageOes)]
        [TestCase("enum", GlBaseType.Enum)]
        [TestCase("GLenum", GlBaseType.Enum)]
        [TestCase("fixed", GlBaseType.Fixed)]
        [TestCase("GLfixed", GlBaseType.Fixed)]
        [TestCase("float", GlBaseType.Float)]
        [TestCase("GLfloat", GlBaseType.Float)]
        [TestCase("half", GlBaseType.Half)]
        [TestCase("halfNV", GlBaseType.Half)]
        [TestCase("GLhalf", GlBaseType.Half)]
        [TestCase("GLhalfNV", GlBaseType.Half)]
        [TestCase("handle", GlBaseType.Handle)]
        [TestCase("HandleARB", GlBaseType.Handle)]
        [TestCase("int", GlBaseType.Int)]
        [TestCase("GLint", GlBaseType.Int)]
        [TestCase("int64", GlBaseType.Int64)]
        [TestCase("GLint64", GlBaseType.Int64)]
        [TestCase("short", GlBaseType.Short)]
        [TestCase("GLshort", GlBaseType.Short)]
        [TestCase("sizei", GlBaseType.SizeI)]
        [TestCase("GLsizei", GlBaseType.SizeI)]
        [TestCase("sizeiptr", GlBaseType.SizeIPtr)]
        [TestCase("GLsizeiptr", GlBaseType.SizeIPtr)]
        [TestCase("sync", GlBaseType.Sync)]
        [TestCase("ubyte", GlBaseType.UByte)]
        [TestCase("GLubyte", GlBaseType.UByte)]
        [TestCase("uint", GlBaseType.UInt)]
        [TestCase("GLuint", GlBaseType.UInt)]
        [TestCase("uint64", GlBaseType.UInt64)]
        [TestCase("uint64EXT", GlBaseType.UInt64)]
        [TestCase("GLuint64", GlBaseType.UInt64)]
        [TestCase("ushort", GlBaseType.UShort)]
        [TestCase("GLushort", GlBaseType.UShort)]
        [TestCase("vdpausurface", GlBaseType.VdpauSurface)]
        [TestCase("vdpausurfaceNV", GlBaseType.VdpauSurface)]
        [TestCase("void", GlBaseType.Void)]
        [TestCase("GLvoid", GlBaseType.Void)]
        public void CanParseBaseTypeCorrectly(
            string value,
            GlBaseType glBaseType)
        {
            var type = GlTypeDescription.Parse(value);
            Assert.That(type.BaseType, Is.EqualTo(glBaseType));
        }
    }
}

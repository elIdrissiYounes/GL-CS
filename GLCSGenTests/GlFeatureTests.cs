using System;
using System.Linq;
using System.Xml.Linq;
using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class GlFeatureTests
    {
        [Test]
        public void ParsesGlApi()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Api, Is.EqualTo(GlApi.Gl));
        }

        [Test]
        public void ParsesGlEs1Api()
        {
            var element = XElement.Parse(@"<feature api=""gles1"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Api, Is.EqualTo(GlApi.GlEs1));
        }

        [Test]
        public void ParsesGlEs2Api()
        {
            var element = XElement.Parse(@"<feature api=""gles2"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Api, Is.EqualTo(GlApi.GlEs2));
        }

        [Test]
        public void ReadsName()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Name, Is.EqualTo("GL_VERSION_1_1"));
        }

        [Test]
        public void ParsesVersion()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Version, Is.EqualTo(new Version(1, 1)));
        }
    }
}

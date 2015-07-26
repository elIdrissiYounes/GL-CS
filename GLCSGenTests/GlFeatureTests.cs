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
        public void CanRemoveCommandsFromInheritedFeature()
        {
            Assert.Fail("Not implemented yet");

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <command name=""glDrawArrays""/>
                    </require>
                    <remove>
                        <command name=""glBlendColor""/>
                    </remove>
                </feature>");
        }

        [Test]
        public void CanRemoveEnumsFromInheritedFeature()
        {
            Assert.Fail("Not implemented yet");

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <enum name=""GL_CURRENT_BIT""/>
                    </require>
                    <remove>
                        <enum name=""GL_POINT_BIT""/>
                    </remove>
                </feature>");
        }

        [Test]
        public void SupportsMultipleRequireElements()
        {
            Assert.Fail("Not implemented yet");

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <command name=""glDrawArrays""/>
                    </require>
                    <require>
                        <command name=""glBlendColor""/>
                    </require>
                </feature>");
        }

        [Test]
        public void SupportsMultipleRemoveElements()
        {
            Assert.Fail("Not implemented yet");
        }

        [Test]
        public void IncludesCommands()
        {
            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <command name=""glDrawArrays""/>
                    </require>
                </feature>");

            var commands = new[] {new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())};

            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands);
            Assert.That(feature.Commands, Has.Count.EqualTo(1));
            Assert.That(feature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
        }

        [Test]
        public void IncludesCommandsFromInheritedFeature()
        {
            Assert.Fail("Not implemented yet");
        }

        [Test]
        public void IncludesEnumerations()
        {
            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <enum name=""GL_DEPTH_BUFFER_BIT""/>
                    </require>
                </feature>");

            var enumerations = new[] {new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u)};

            var feature = GlFeature.Parse(element, enumerations, Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Enumerations, Has.Count.EqualTo(1));
            Assert.That(feature.Enumerations[0].Name, Is.EqualTo("GL_DEPTH_BUFFER_BIT"));
            Assert.That(feature.Enumerations[0].UInt32Value, Is.EqualTo(0));
        }

        [Test]
        public void IncludesEnumerationsFromInheritedFeature()
        {
            Assert.Fail("Not implemented yet");
        }

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
        public void ParsesVersion()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Version, Is.EqualTo(new Version(1, 1)));
        }

        [Test]
        public void ReadsName()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlFeature.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Name, Is.EqualTo("GL_VERSION_1_1"));
        }
    }
}

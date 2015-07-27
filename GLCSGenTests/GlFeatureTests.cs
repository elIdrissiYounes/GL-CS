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
        public void Constructor_CanRemoveCommandsFromInheritedFeature()
        {
            var glCommand = new GlCommand("glDrawArrays",
                                          new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None),
                                          Enumerable.Empty<IGlParameter>());

            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(1, 0),
                                          Enumerable.Empty<IGlEnumeration>(),
                                          new[] {glCommand});

            var childFeature = new GlApi(GlApiFamily.Gl,
                                         GlProfileType.Compatibility,
                                         new Version(2, 0),
                                         Enumerable.Empty<IGlEnumeration>(),
                                         Enumerable.Empty<IGlCommand>(),
                                         parentFeature,
                                         Enumerable.Empty<IGlEnumeration>(),
                                         new[] {glCommand});

            Assert.That(childFeature.Commands, Is.Empty);
        }

        [Test]
        public void Constructor_CanRemoveEnumsFromInheritedFeature()
        {
            var glEnumeration = new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u);

            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(1, 0),
                                          new[] {glEnumeration},
                                          Enumerable.Empty<IGlCommand>());

            var childFeature = new GlApi(GlApiFamily.Gl,
                                         GlProfileType.Compatibility,
                                         new Version(2, 0),
                                         Enumerable.Empty<IGlEnumeration>(),
                                         Enumerable.Empty<IGlCommand>(),
                                         parentFeature,
                                         new[] {glEnumeration},
                                         Enumerable.Empty<IGlCommand>());

            Assert.That(childFeature.Enumerations, Is.Empty);
        }

        [Test]
        public void Constructor_IncludesCommandsFromInheritedFeature()
        {
            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(1, 0),
                                          Enumerable.Empty<IGlEnumeration>(),
                                          new[]
                                          {
                                              new GlCommand("glDrawArrays",
                                                            new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None),
                                                            Enumerable.Empty<IGlParameter>())
                                          });

            var childFeature = new GlApi(GlApiFamily.Gl,
                                         GlProfileType.Compatibility,
                                         new Version(2, 0),
                                         Enumerable.Empty<IGlEnumeration>(),
                                         Enumerable.Empty<IGlCommand>(),
                                         parentFeature);
            Assert.That(childFeature.Commands, Has.Count.EqualTo(1));
            Assert.That(childFeature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
        }

        [Test]
        public void Constructor_IncludesEnumerationsFromInheritedFeature()
        {
            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(1, 0),
                                          new[] {new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u)},
                                          Enumerable.Empty<IGlCommand>());

            var childFeature = new GlApi(GlApiFamily.Gl,
                                         GlProfileType.Compatibility,
                                         new Version(2, 0),
                                         Enumerable.Empty<IGlEnumeration>(),
                                         Enumerable.Empty<IGlCommand>(),
                                         parentFeature);

            Assert.That(childFeature.Enumerations, Has.Count.EqualTo(1));
            Assert.That(childFeature.Enumerations[0].Name, Is.EqualTo("GL_DEPTH_BUFFER_BIT"));
            Assert.That(childFeature.Enumerations[0].UInt32Value, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_ThrowsIfChildHasLowerVersionThanParent()
        {
            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(2, 0),
                                          Enumerable.Empty<IGlEnumeration>(),
                                          Enumerable.Empty<IGlCommand>());

            Assert.Throws<GlInvalidParentFeatureException>(() => new GlApi(GlApiFamily.Gl,
                                                                           GlProfileType.Compatibility,
                                                                           new Version(1, 0),
                                                                           Enumerable.Empty<IGlEnumeration>(),
                                                                           Enumerable.Empty<IGlCommand>(),
                                                                           parentFeature));
        }

        [Test]
        public void Constructor_ThrowsIfChildHasSameVersionThanParent()
        {
            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(2, 0),
                                          Enumerable.Empty<IGlEnumeration>(),
                                          Enumerable.Empty<IGlCommand>());

            Assert.Throws<GlInvalidParentFeatureException>(() => new GlApi(GlApiFamily.Gl,
                                                                           GlProfileType.Compatibility,
                                                                           new Version(2, 0),
                                                                           Enumerable.Empty<IGlEnumeration>(),
                                                                           Enumerable.Empty<IGlCommand>(),
                                                                           parentFeature));
        }

        [Test]
        public void Parse_IncludesCommands()
        {
            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <command name=""glDrawArrays""/>
                    </require>
                </feature>");

            var commands = new[] {new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())};

            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands);
            Assert.That(feature.Commands, Has.Count.EqualTo(1));
            Assert.That(feature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
        }

        [Test]
        public void Parse_IncludesEnumerations()
        {
            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <enum name=""GL_DEPTH_BUFFER_BIT""/>
                    </require>
                </feature>");

            var enumerations = new[] {new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u)};

            var feature = GlApi.Parse(element, enumerations, Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Enumerations, Has.Count.EqualTo(1));
            Assert.That(feature.Enumerations[0].Name, Is.EqualTo("GL_DEPTH_BUFFER_BIT"));
            Assert.That(feature.Enumerations[0].UInt32Value, Is.EqualTo(0));
        }

        [Test]
        public void Parse_ParsesGlApi()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.ApiFamily, Is.EqualTo(GlApiFamily.Gl));
        }

        [Test]
        public void Parse_ParsesVersion()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.Version, Is.EqualTo(new Version(1, 1)));
        }

        [Test]
        public void Parse_SupportsMultipleRemoveElements()
        {
            var commands = new[]
            {
                new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>()),
                new GlCommand("glBlendColor", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())
            };

            var parentFeature = new GlApi(GlApiFamily.Gl,
                                          GlProfileType.Compatibility,
                                          new Version(1, 0),
                                          Enumerable.Empty<IGlEnumeration>(),
                                          commands);

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <remove>
                        <command name=""glDrawArrays""/>
                    </remove>
                    <remove>
                        <command name=""glBlendColor""/>
                    </remove>
                </feature>");

            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands, parentFeature);
            Assert.That(feature.Commands, Is.Empty);
        }

        [Test]
        public void Parse_SupportsMultipleRequireElements()
        {
            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <command name=""glDrawArrays""/>
                    </require>
                    <require>
                        <command name=""glBlendColor""/>
                    </require>
                </feature>");

            var commands = new[]
            {
                new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>()),
                new GlCommand("glBlendColor", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())
            };

            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands);
            Assert.That(feature.Commands, Has.Count.EqualTo(2));
            Assert.That(feature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
            Assert.That(feature.Commands[1].Name, Is.EqualTo("glBlendColor"));
        }

        [Test]
        public void ParsesGlEs1Api()
        {
            var element = XElement.Parse(@"<feature api=""gles1"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.ApiFamily, Is.EqualTo(GlApiFamily.GlEs1));
        }

        [Test]
        public void ParsesGlEs2Api()
        {
            var element = XElement.Parse(@"<feature api=""gles2"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            Assert.That(feature.ApiFamily, Is.EqualTo(GlApiFamily.GlEs2));
        }
    }
}

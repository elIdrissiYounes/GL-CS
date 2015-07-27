using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GLCSGen.Spec;
using NUnit.Framework;

namespace GLCSGenTests
{
    [TestFixture]
    public class GlApiTests
    {
        [Test]
        public void Parse_CanRemoveCommandsFromInheritedFeature()
        {
            Assert.Fail();
            //var glCommand = new GlCommand("glDrawArrays",
            //                              new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None),
            //                              Enumerable.Empty<IGlParameter>());

            //var parentFeature = new GlApi(GlApiFamily.Gl,
            //                              GlProfileType.Compatibility,
            //                              new Version(1, 0),
            //                              Enumerable.Empty<IGlEnumeration>(),
            //                              new[] {glCommand});

            //var childFeature = new GlApi(GlApiFamily.Gl,
            //                             GlProfileType.Compatibility,
            //                             new Version(2, 0),
            //                             Enumerable.Empty<IGlEnumeration>(),
            //                             Enumerable.Empty<IGlCommand>(),
            //                             parentFeature,
            //                             Enumerable.Empty<IGlEnumeration>(),
            //                             new[] {glCommand});

            //Assert.That(childFeature.Commands, Is.Empty);
        }

        [Test]
        public void Parse_CanRemoveEnumsFromInheritedFeature()
        {
            Assert.Fail();
            //var glEnumeration = new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u);

            //var parentFeature = new GlApi(GlApiFamily.Gl,
            //                              GlProfileType.Compatibility,
            //                              new Version(1, 0),
            //                              new[] {glEnumeration},
            //                              Enumerable.Empty<IGlCommand>());

            //var childFeature = new GlApi(GlApiFamily.Gl,
            //                             GlProfileType.Compatibility,
            //                             new Version(2, 0),
            //                             Enumerable.Empty<IGlEnumeration>(),
            //                             Enumerable.Empty<IGlCommand>(),
            //                             parentFeature,
            //                             new[] {glEnumeration},
            //                             Enumerable.Empty<IGlCommand>());

            //Assert.That(childFeature.Enumerations, Is.Empty);
        }

        [Test]
        public void Parse_IncludesCommands()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"
            //    <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
            //        <require>
            //            <command name=""glDrawArrays""/>
            //        </require>
            //    </feature>");

            //var commands = new[] {new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())};

            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands);
            //Assert.That(feature.Commands, Has.Count.EqualTo(1));
            //Assert.That(feature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
        }

        [Test]
        public void Parse_IncludesCommandsFromInheritedFeature()
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

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
                    <require>
                        <command name=""glBlendColor""/>
                    </require>
                </feature>");

            var commands = new[] {new GlCommand("glBlendColor", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())};

            var childFeature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands, parentFeature, GlProfileType.Compatibility);
            Assert.That(childFeature.Commands, Has.Count.EqualTo(2));
            Assert.That(childFeature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
            Assert.That(childFeature.Commands[1].Name, Is.EqualTo("glBlendColor"));
        }

        [Test]
        public void Parse_IncludesEnumerations()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"
            //    <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
            //        <require>
            //            <enum name=""GL_DEPTH_BUFFER_BIT""/>
            //        </require>
            //    </feature>");

            //var enumerations = new[] {new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u)};

            //var feature = GlApi.Parse(element, enumerations, Enumerable.Empty<IGlCommand>(), null, GlProfileType.Compatibility);
            //Assert.That(feature.Enumerations, Has.Count.EqualTo(1));
            //Assert.That(feature.Enumerations[0].Name, Is.EqualTo("GL_DEPTH_BUFFER_BIT"));
            //Assert.That(feature.Enumerations[0].UInt32Value, Is.EqualTo(0));
        }

        [Test]
        public void Parse_IncludesEnumerationsFromInheritedFeature()
        {
            Assert.Fail();
            //var parentFeature = new GlApi(GlApiFamily.Gl,
            //                              GlProfileType.Compatibility,
            //                              new Version(1, 0),
            //                              new[] {new GlEnumeration("GL_DEPTH_BUFFER_BIT", 0u)},
            //                              Enumerable.Empty<IGlCommand>());

            //var childFeature = new GlApi(GlApiFamily.Gl,
            //                             GlProfileType.Compatibility,
            //                             new Version(2, 0),
            //                             Enumerable.Empty<IGlEnumeration>(),
            //                             Enumerable.Empty<IGlCommand>(),
            //                             parentFeature);

            //Assert.That(childFeature.Enumerations, Has.Count.EqualTo(1));
            //Assert.That(childFeature.Enumerations[0].Name, Is.EqualTo("GL_DEPTH_BUFFER_BIT"));
            //Assert.That(childFeature.Enumerations[0].UInt32Value, Is.EqualTo(0));
        }

        [Test]
        public void Parse_ParsesGlApi()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            //Assert.That(feature.ApiFamily, Is.EqualTo(GlApiFamily.Gl));
        }

        [Test]
        public void Parse_ParsesGlEs1Api()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"<feature api=""gles1"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            //Assert.That(feature.ApiFamily, Is.EqualTo(GlApiFamily.GlEs));
        }

        [Test]
        public void Parse_ParsesGlEs2Api()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"<feature api=""gles2"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            //Assert.That(feature.ApiFamily, Is.EqualTo(GlApiFamily.GlEs));
        }

        [Test]
        public void Parse_ParsesVersion()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1""></feature>");
            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), Enumerable.Empty<IGlCommand>());
            //Assert.That(feature.Version, Is.EqualTo(new Version(1, 1)));
        }

        [Test]
        public void Parse_SupportsMultipleRemoveElements()
        {
            Assert.Fail();
            //var commands = new[]
            //{
            //    new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>()),
            //    new GlCommand("glBlendColor", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())
            //};

            //var parentFeature = new GlApi(GlApiFamily.Gl,
            //                              GlProfileType.Compatibility,
            //                              new Version(1, 0),
            //                              Enumerable.Empty<IGlEnumeration>(),
            //                              commands);

            //var element = XElement.Parse(@"
            //    <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
            //        <remove>
            //            <command name=""glDrawArrays""/>
            //        </remove>
            //        <remove>
            //            <command name=""glBlendColor""/>
            //        </remove>
            //    </feature>");

            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands, parentFeature);
            //Assert.That(feature.Commands, Is.Empty);
        }

        [Test]
        public void Parse_SupportsMultipleRequireElements()
        {
            Assert.Fail();
            //var element = XElement.Parse(@"
            //    <feature api=""gl"" name=""GL_VERSION_1_1"" number=""1.1"">
            //        <require>
            //            <command name=""glDrawArrays""/>
            //        </require>
            //        <require>
            //            <command name=""glBlendColor""/>
            //        </require>
            //    </feature>");

            //var commands = new[]
            //{
            //    new GlCommand("glDrawArrays", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>()),
            //    new GlCommand("glBlendColor", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())
            //};

            //var feature = GlApi.Parse(element, Enumerable.Empty<IGlEnumeration>(), commands);
            //Assert.That(feature.Commands, Has.Count.EqualTo(2));
            //Assert.That(feature.Commands[0].Name, Is.EqualTo("glDrawArrays"));
            //Assert.That(feature.Commands[1].Name, Is.EqualTo("glBlendColor"));
        }

        [Test]
        public void Parse_ThrowsIfChildHasLowerVersionThanParent()
        {
            Assert.Fail();
            //var parentFeature = new GlApi(GlApiFamily.Gl,
            //                              GlProfileType.Compatibility,
            //                              new Version(2, 0),
            //                              Enumerable.Empty<IGlEnumeration>(),
            //                              Enumerable.Empty<IGlCommand>());

            //Assert.Throws<GlInvalidParentFeatureException>(() => new GlApi(GlApiFamily.Gl,
            //                                                               GlProfileType.Compatibility,
            //                                                               new Version(1, 0),
            //                                                               Enumerable.Empty<IGlEnumeration>(),
            //                                                               Enumerable.Empty<IGlCommand>(),
            //                                                               parentFeature));
        }

        [Test]
        public void Parse_ThrowsIfChildHasSameVersionThanParent()
        {
            Assert.Fail();
            //var parentFeature = new GlApi(GlApiFamily.Gl,
            //                              GlProfileType.Compatibility,
            //                              new Version(2, 0),
            //                              Enumerable.Empty<IGlEnumeration>(),
            //                              Enumerable.Empty<IGlCommand>());

            //Assert.Throws<GlInvalidParentFeatureException>(() => new GlApi(GlApiFamily.Gl,
            //                                                               GlProfileType.Compatibility,
            //                                                               new Version(2, 0),
            //                                                               Enumerable.Empty<IGlEnumeration>(),
            //                                                               Enumerable.Empty<IGlCommand>(),
            //                                                               parentFeature));
        }

        [Test]
        public void ParseOpenGL1_IncludesCommands()
        {
            var allCommands = new[]
            {
                new GlCommand("glAccum", new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None), Enumerable.Empty<IGlParameter>())
            };

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_0"" number=""1.0"">
                    <require>
                        <command name=""glAccum""/>
                    </require>
                </feature>");

            var api = GlApi.ParseOpenGl1(element, new Dictionary<string, IEnumerable<IGlEnumeration>>(), allCommands);
            Assert.That(api.Commands, Has.Count.EqualTo(1));
            Assert.That(api.Commands[0], Is.SameAs(allCommands[0]));
        }

        [Test]
        public void ParseOpenGL1_IncludesEnumsUsingCommandParameterGroups()
        {
            var enumerationsByGroup = new Dictionary<string, IEnumerable<IGlEnumeration>>
            {
                ["AccumOp"] = new[] {new GlEnumeration("Test1", 0u), new GlEnumeration("Test2", 1u)},
                ["OtherEnum"] = new[] {new GlEnumeration("Test3", 2u), new GlEnumeration("Test4", 3u)}
            };

            var allCommands = new[]
            {
                new GlCommand("glAccum",
                              new GlTypeDescription(GlBaseType.Void, GlTypeModifier.None),
                              new[] {new GlParameter(new GlTypeDescription(GlBaseType.Enum, GlTypeModifier.None), "AccumOp", "op")})
            };

            var element = XElement.Parse(@"
                <feature api=""gl"" name=""GL_VERSION_1_0"" number=""1.0"">
                    <require>
                        <command name=""glAccum""/>
                    </require>
                </feature>");

            var api = GlApi.ParseOpenGl1(element, enumerationsByGroup, allCommands);
            Assert.That(api.Enumerations, Has.Count.EqualTo(2));
            Assert.That(api.Enumerations, Is.EquivalentTo(enumerationsByGroup["AccumOp"]));
        }

        [Test]
        public void ParseOpenGl1_ReturnsApiWithCorrectProperties()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_0"" number=""1.0""></feature>");
            var api = GlApi.ParseOpenGl1(element, new Dictionary<string, IEnumerable<IGlEnumeration>>(), Enumerable.Empty<IGlCommand>());
            Assert.That(api.ApiFamily, Is.EqualTo(GlApiFamily.Gl));
            Assert.That(api.Version, Is.EqualTo(new Version(1, 0)));
            Assert.That(api.ProfileType, Is.EqualTo(GlProfileType.None));
        }

        [Test]
        public void ParseOpenGl1_ThrowsIfApiIsNotGl()
        {
            var element = XElement.Parse(@"<feature api=""gles1"" name=""GL_VERSION_1_0"" number=""1.0""></feature>");
            Assert.Throws<GlFeatureNotOpenGl1Exception>(() => GlApi.ParseOpenGl1(element,
                                                                                 new Dictionary<string, IEnumerable<IGlEnumeration>>(),
                                                                                 Enumerable.Empty<IGlCommand>()));
        }

        [Test]
        public void ParseOpenGl1_ThrowsIfVersionIsNot1()
        {
            var element = XElement.Parse(@"<feature api=""gl"" name=""GL_VERSION_1_0"" number=""1.1""></feature>");
            Assert.Throws<GlFeatureNotOpenGl1Exception>(() => GlApi.ParseOpenGl1(element,
                                                                                 new Dictionary<string, IEnumerable<IGlEnumeration>>(),
                                                                                 Enumerable.Empty<IGlCommand>()));
        }
    }
}

using System.IO;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using SerializationInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;

#endif

namespace SerializationInspections.Plugin.Tests.Integrative
{
    public class HighlightingTests : CSharpHighlightingTestBase
    {
        protected override string RelativeTestDataPath => "Highlighting";

#if RESHARPER8
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
#else
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
#endif
        {
            return highlighting is SerializationHighlightingBase;
        }

        [TestNetFramework4]
        public class Default : HighlightingTests
        {
            [Test]
            public void TestException()
            {
                DoNamedTest2();
            }

            [Test]
            public void TestExceptionHierarchy()
            {
                DoNamedTest2();
            }

            [Test]
            public void TestSerializableInterface()
            {
                DoNamedTest2();
            }

            [Test]
            public void TestSerializableInterfaceHierarchy()
            {
                DoNamedTest2();
            }

            [Test]
            public void TestSerializableInterfaceOnStruct()
            {
                DoNamedTest2();
            }

            [Test]
            public void TestOtherUnaffectedTypes()
            {
                DoNamedTest2();
            }
        }

        [ExcludeMsCorLib]
        public class NoMsCorLib : HighlightingTests
        {
            protected override string RelativeTestDataPath => Path.Combine(base.RelativeTestDataPath, "NoMsCorLib");

            [Test]
            public void TestNoMsCorlib()
            {
                DoNamedTest2();
            }

            [Test]
            public void TestNoMsCorlibButOwnSerializableAttribute()
            {
                DoNamedTest2();
            }
        }
    }
}
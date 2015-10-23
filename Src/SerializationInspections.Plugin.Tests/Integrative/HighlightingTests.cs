using System.IO;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using SerializationInspections.Plugin.Highlighting;

namespace SerializationInspections.Plugin.Tests.Integrative
{
    public class HighlightingTests : CSharpHighlightingTestBase
    {
        protected override string RelativeTestDataPath => "Highlighting";

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
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
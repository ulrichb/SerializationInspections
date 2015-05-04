using System;
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

namespace SerializationInspections.Plugin.Tests.IntegrationTests
{
    public class HighlightingTests : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath
        {
            get { return "Highlighting"; }
        }

#if RESHARPER8
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
#else
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
#endif
        {
            return highlighting is SerializationHighlightingBase;
        }

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
}
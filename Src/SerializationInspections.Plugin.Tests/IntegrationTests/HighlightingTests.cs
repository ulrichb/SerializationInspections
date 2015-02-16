using System;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using NUnit.Framework;
using SerializationInspections.Plugin.Highlighting;

namespace SerializationInspections.Plugin.Tests.IntegrationTests
{
    public class HighlightingTests : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath
        {
            get { return "Highlighting"; }
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
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
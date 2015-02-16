using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Intentions.Test;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;
using SerializationInspections.Plugin.Highlighting;

namespace SerializationInspections.Plugin.Tests.IntegrationTests
{
    [TestFixture]
    public class SerializationHighlightingQuickFixAvailabilityTests : QuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath
        {
            get { return @"QuickFixes"; }
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile psiSourceFile)
        {
            return highlighting is SerializationHighlightingBase;
        }

        [Test]
        public void TestAvailability()
        {
            DoNamedTest2();
        }
    }
}
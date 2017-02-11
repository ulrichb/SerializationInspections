using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using SerializationInspections.Plugin.Highlighting;

namespace SerializationInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    [TestNetFramework4]
    public class SerializationHighlightingQuickFixAvailabilityTests : QuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath => @"QuickFixes";

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

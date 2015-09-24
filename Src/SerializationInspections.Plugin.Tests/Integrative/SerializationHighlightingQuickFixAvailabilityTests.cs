using JetBrains.ReSharper.Psi;
using NUnit.Framework;
using SerializationInspections.Plugin.Highlighting;

#if RESHARPER8
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Intentions.Test;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;

#endif

namespace SerializationInspections.Plugin.Tests.Integrative
{
    [TestFixture]
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
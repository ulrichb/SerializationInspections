using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using SerializationInspections.Plugin.Quickfixes;

#if RESHARPER92
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Psi.CSharp.CodeStyle.FormatSettings;

#endif

namespace SerializationInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    [TestNetFramework4]
    public class MissingSerializationAttributeQuickFixTest : CSharpQuickFixTestBase<MissingSerializationAttributeQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\MissingSerializationAttribute";

        [Test]
        public void TestExceptionClass()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestExceptionWithExistingAttributes()
        {
            DoNamedTest2();
        }

        protected override void DoTestSolution(params string[] fileSet)
        {
            ExecuteWithinSettingsTransaction(s =>
            {
                // The default of the FORCE_ATTRIBUTE_STYLE setting was changed to "SEPARATE" in R# 10:
#if RESHARPER92

                RunGuarded(() => s.SetValue((CSharpFormatSettingsKey x) => x.FORCE_ATTRIBUTE_STYLE, ForceAttributeStyle.SEPARATE));
#endif

                base.DoTestSolution(fileSet);
            });
        }
    }
}
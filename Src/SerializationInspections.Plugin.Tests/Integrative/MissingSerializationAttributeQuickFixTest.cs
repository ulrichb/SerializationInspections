using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;
using SerializationInspections.Plugin.Quickfixes;

namespace SerializationInspections.Plugin.Tests.Integrative
{
    [TestFixture]
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
    }
}
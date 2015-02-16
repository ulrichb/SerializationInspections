using JetBrains.ReSharper.IntentionsTests;
using NUnit.Framework;
using SerializationInspections.Plugin.Quickfixes;

namespace SerializationInspections.Plugin.Tests.IntegrationTests
{
    [TestFixture]
    public class MissingSerializationAttributeQuickFixTest : QuickFixTestBase<MissingSerializationAttributeQuickFix>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"QuickFixes\MissingSerializationAttribute"; }
        }

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
using NUnit.Framework;
using SerializationInspections.Plugin.Quickfixes;
#if RESHARPER8
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes.Tests;

#else
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;

#endif

namespace SerializationInspections.Plugin.Tests.IntegrationTests
{
    [TestFixture]
    public class MissingSerializationAttributeQuickFixTest : CSharpQuickFixTestBase<MissingSerializationAttributeQuickFix>
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
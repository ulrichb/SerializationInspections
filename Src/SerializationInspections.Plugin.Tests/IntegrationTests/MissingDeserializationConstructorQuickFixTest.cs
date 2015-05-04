using NUnit.Framework;
using SerializationInspections.Plugin.Quickfixes;

#if RESHARPER8
using JetBrains.ReSharper.IntentionsTests;

#else
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
#endif

namespace SerializationInspections.Plugin.Tests.IntegrationTests
{
    [TestFixture]
    public class MissingDeserializationConstructorQuickFixTest : QuickFixTestBase<MissingDeserializationConstructorQuickFix>
    {
        protected override string RelativeTestDataPath
        {
            get { return @"QuickFixes\MissingDeserializationConstructorQuickFix"; }
        }

        [Test]
        public void TestExceptionClass()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestExceptionWithExistingMembers()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestCustomSerializable()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestCustomSerializableStruct()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestCustomSerializableWithPrivateDeserializationConstructorInBase()
        {
            DoNamedTest2();
        }
    }
}
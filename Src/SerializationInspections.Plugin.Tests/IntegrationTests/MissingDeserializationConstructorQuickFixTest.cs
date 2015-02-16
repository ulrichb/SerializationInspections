using JetBrains.ReSharper.IntentionsTests;
using NUnit.Framework;
using SerializationInspections.Plugin.Quickfixes;

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
using System.Runtime.Serialization;
using NUnit.Framework;

namespace SerializationInspections.Sample.Highlighting.Tests
{
    [TestFixture]
    public class ExceptionTests : SerializationTestBase
    {
        [Test]
        public void ExceptionWithDeserializationConstructor()
        {
            var deserialized = SerializeAndDeserialize(new ExceptionWithDeserializationConstructor("message"));

            Assert.That(deserialized.Message, Is.EqualTo("message"));
        }

        [Test]
        public void ExceptionWithDeserializationConstructorButNoSerializableAttribute()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new ExceptionWithDeserializationConstructorButNoSerializableAttribute()));
        }

        [Test]
        public void ExceptionWithSerializableAttributeButNoDeserializationConstructor()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new ExceptionWithSerializableAttributeButNoDeserializationConstructor()));
        }

        [Test]
        public void ExceptionWithoutSerializableAttributeAndDeserializationConstructor()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new ExceptionWithoutSerializableAttributeAndDeserializationConstructor()));
        }
    }
}

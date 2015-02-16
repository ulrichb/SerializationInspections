using System.Runtime.Serialization;
using NUnit.Framework;

namespace SerializationInspections.Sample.Highlighting.Tests
{
    [TestFixture]
    public class SerializableInterfaceTests : SerializationTestBase
    {
        [Test]
        public void CustomSerializableWithDeserializationConstructor()
        {
            var instance = new CustomSerializableWithDeserializationConstructor { Serialized = "a", NonSerialized = "b" };
            var deserialized = SerializeAndDeserialize(instance);

            Assert.That(deserialized.Serialized, Is.EqualTo("a"));
            Assert.That(deserialized.NonSerialized, Is.EqualTo(null));
        }

        [Test]
        public void CustomSerializableWithoutDeserializationConstructor()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new CustomSerializableWithoutDeserializationConstructor()));
        }

        [Test]
        public void CustomSerializableWithDeserializationConstructorButNoSerializableAttribute()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new CustomSerializableWithDeserializationConstructorButNoSerializableAttribute()));
        }

        [Test]
        public void CustomSerializableWithoutSerializableAttributeAndDeserializationConstructor()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new CustomSerializableWithoutSerializableAttributeAndDeserializationConstructor()));
        }
    }
}
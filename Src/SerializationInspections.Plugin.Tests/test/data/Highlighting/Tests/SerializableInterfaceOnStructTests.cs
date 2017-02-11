using System.Runtime.Serialization;
using NUnit.Framework;

namespace SerializationInspections.Sample.Highlighting.Tests
{
    [TestFixture]
    public class SerializableInterfaceOnStructTests : SerializationTestBase
    {
        [Test]
        public void CustomSerializableWithDeserializationConstructor()
        {
            var instance = new CustomSerializableStructWitDeserializationConstructor();
            SerializeAndDeserialize(instance);
        }

        [Test]
        public void CustomSerializableWithoutDeserializationConstructor()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new CustomSerializableStructWithoutDeserializationConstructor()));
        }

        [Test]
        public void CustomSerializableStructWithoutSerializableAttributeAndDeserializationConstructor()
        {
            Assert.Throws<SerializationException>(() =>
                SerializeAndDeserialize(new CustomSerializableStructWithoutSerializableAttributeAndDeserializationConstructor()));
        }
    }
}

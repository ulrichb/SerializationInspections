using System;
using System.Runtime.Serialization;

namespace SerializationInspections.Sample.Highlighting
{
    [Serializable]
    public struct CustomSerializableStructWitDeserializationConstructor : ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        private CustomSerializableStructWitDeserializationConstructor(SerializationInfo info, StreamingContext context)
        {
        }
    }

    [Serializable]
    public struct CustomSerializableStructWithoutDeserializationConstructor : ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }

    public struct CustomSerializableStructWithoutSerializableAttributeAndDeserializationConstructor : ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

// ReSharper disable UnusedMember.Local

namespace SerializationInspections.Sample.Highlighting
{
    [Serializable]
    public class ExceptionWithDeserializationConstructor : Exception
    {
        public ExceptionWithDeserializationConstructor(string message)
            : base(message)
        {
        }

        private ExceptionWithDeserializationConstructor(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public class ExceptionWithDeserializationConstructorButNoSerializableAttribute : Exception
    {
        public ExceptionWithDeserializationConstructorButNoSerializableAttribute()
        {
        }

        private ExceptionWithDeserializationConstructorButNoSerializableAttribute(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ExceptionWithSerializableAttributeButNoDeserializationConstructor : Exception
    {
    }

    public class ExceptionWithoutSerializableAttributeAndDeserializationConstructor : Exception
    {
    }
}

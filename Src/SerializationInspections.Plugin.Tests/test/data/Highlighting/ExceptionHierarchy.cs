using System;
using System.Runtime.Serialization;

namespace SerializationInspections.Sample.Highlighting
{
    [Serializable]
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase()
        {
        }

        protected ExceptionBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class DerivedFromExceptionBaseWithSerializationAttributeButNoDeserializationConstructor : ExceptionBase
    {
    }

    public class DerivedFromExceptionBaseWithoutSerializationAttribute : ExceptionBase
    {
    }
}

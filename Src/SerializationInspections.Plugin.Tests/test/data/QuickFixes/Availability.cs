using System;

namespace SerializationInspections.Sample.QuickFixes
{
    public class ExceptionClassWithoutSerializableAttribute : Exception
    {
    }

    [Serializable]
    public class ExceptionClassWithSerializableAttributeButNoDeserializationConstructor : Exception
    {
    }
}

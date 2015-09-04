using System;
using System.Runtime.Serialization;

namespace SerializationInspections.Sample.QuickFixes.MissingDeserializationConstructorQuickFix
{
  public class CustomSerializableWithPrivateDeserializationConstructorBase
  {
    protected CustomSerializableWithPrivateDeserializationConstructorBase()
    {
    }

    private CustomSerializableWithPrivateDeserializationConstructorBase(SerializationInfo info, StreamingContext context)
    {
    }
  }

  [Serializable]
  public class CustomSerializableWithPrivateDeserializationConstructorInBase{caret} : CustomSerializableWithPrivateDeserializationConstructorBase, ISerializable
  {
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
using System;
using System.Runtime.Serialization;

namespace SerializationInspections.Sample.QuickFixes.MissingDeserializationConstructorQuickFix
{
  [Serializable]
  public struct CustomSerializableStruct{caret} : ISerializable
  {
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
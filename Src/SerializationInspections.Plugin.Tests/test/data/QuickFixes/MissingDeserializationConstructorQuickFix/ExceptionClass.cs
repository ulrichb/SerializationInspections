using System;

namespace SerializationInspections.Sample.QuickFixes.MissingDeserializationConstructorQuickFix
{
  [Serializable]
  public class ExceptionClass{caret} : Exception
  {
  }
}
using System;

namespace SerializationInspections.Sample.QuickFixes.MissingDeserializationConstructorQuickFix
{
  [Serializable]
  public class ExceptionWithExistingMembers{caret} : Exception
  {
    public int ExistingField;

    public ExceptionWithExistingMembers()
    {
    }

    public int ExistingProperty { get; set; }

    private void ExistingMethod()
    {
    }
  }
}
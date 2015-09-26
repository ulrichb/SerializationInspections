using System;
using System.Diagnostics;

namespace SerializationInspections.Sample.QuickFixes.MissingSerializationAttribute
{
  [DebuggerDisplay("x")]
  [CLSCompliant(true)]
  public class ExceptionWithExistingAttributes{caret} : Exception
  {
  }
}
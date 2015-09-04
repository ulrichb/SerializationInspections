using System.Runtime.Serialization;

namespace System
{
    public class SerializableAttribute // Tests the triggering of the highlighting when the SerializableAttribute is resolvable
    {
    }
}

namespace System.Runtime.Serialization
{
    public interface ISerializable
    {
    }
}

namespace SerializationInspections.Sample.Highlighting
{
    public class CustomSerializable : ISerializable
    {
    }
}
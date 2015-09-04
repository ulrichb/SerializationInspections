using System;
using System.Runtime.Serialization;

namespace SerializationInspections.Sample.Highlighting
{
    public class SimpleClass
    {
    }

    public struct SimpleStruct
    {
    }

    public interface ISimpleInterface
    {
    }

    public interface IInterfaceDerivedFromISerializable : ISerializable
    {
    }

    public delegate void SimpleDelegate();

    [Serializable]
    public delegate void SerializableDelegate();

    public enum SimpleEnum
    {
    }

    [Serializable]
    public enum SerializableEnum
    {
    }
}
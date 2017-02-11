using System;
using System.Runtime.Serialization;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable RedundantOverridenMember

namespace SerializationInspections.Sample.Highlighting
{
    [Serializable]
    public abstract class CustomSerializableBase : ISerializable
    {
        protected CustomSerializableBase()
        {
        }

        protected CustomSerializableBase(SerializationInfo info, StreamingContext context)
        {
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }

    [Serializable]
    public class DerivedCustomSerializableWithoutDeserializationConstructor : CustomSerializableBase
    {
    }

    public class DerivedCustomSerializableWithoutSerializableAttribute : CustomSerializableBase
    {
    }

    public class DerivedCustomSerializablePlusImplementedISerializableWithoutSerializableAttribute
        : CustomSerializableBase, ISerializable
    {
    }

    public class DerivedCustomSerializableWithOverriddenGetObjectDataButWithoutSerializableAttribute : CustomSerializableBase
    {
        // IDEA: Here it would also be safe to emit a warning.

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

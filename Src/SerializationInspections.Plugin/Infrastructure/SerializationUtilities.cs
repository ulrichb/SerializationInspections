using System.Linq;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;

namespace SerializationInspections.Plugin.Infrastructure
{
    /// <summary>
    /// Utility class for serialization types.
    /// </summary>
    public static class SerializationUtilities
    {
        public static readonly ClrTypeName SerializationInfoTypeName = new ClrTypeName("System.Runtime.Serialization.SerializationInfo");
        public static readonly ClrTypeName StreamingContextTypeName = new ClrTypeName("System.Runtime.Serialization.StreamingContext");

        public static bool HasDeserializationConstructor(ITypeElement typeElement)
        {
            return typeElement.Constructors.Any(constructor =>
            {
                if (constructor.Parameters.Count != 2)
                    return false;

                return constructor.Parameters[0].Type.IsSubtypeOf(SerializationInfoTypeName.CreateTypeInContextOf(constructor)) &&
                       constructor.Parameters[1].Type.IsSubtypeOf(StreamingContextTypeName.CreateTypeInContextOf(constructor));
            });
        }
    }
}

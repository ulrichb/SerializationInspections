using JetBrains.Metadata.Reader.API;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace SerializationInspections.Plugin.Infrastructure
{
    /// <summary>
    ///     Extension methods for <see cref="IClrTypeName" />.
    /// </summary>
    public static class ClrTypeNameExtensions
    {
        public static IDeclaredType CreateTypeInContextOf(this IClrTypeName clrTypeName, IDeclaration declaration)
        {
            return TypeFactory.CreateTypeByCLRName(clrTypeName, declaration.GetPsiModule());
        }

        public static IDeclaredType CreateTypeInContextOf(this IClrTypeName clrTypeName, IClrDeclaredElement contextElement)
        {
            return TypeFactory.CreateTypeByCLRName(clrTypeName, contextElement.Module);
        }
    }
}

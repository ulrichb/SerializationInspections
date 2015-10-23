using JetBrains.Annotations;
using JetBrains.Metadata.Reader.API;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace SerializationInspections.Plugin.Infrastructure
{
    /// <summary>
    /// Extension methods for <see cref="IClrTypeName"/>.
    /// </summary>
    public static class ClrTypeNameExtensions
    {
        [NotNull]
        public static IDeclaredType CreateTypeInContextOf([NotNull] this IClrTypeName clrTypeName, [NotNull] IDeclaration declaration)
        {
            return TypeFactory.CreateTypeByCLRName(clrTypeName, declaration.GetPsiModule(), declaration.GetResolveContext());
        }

        [NotNull]
        public static IDeclaredType CreateTypeInContextOf([NotNull] this IClrTypeName clrTypeName, [NotNull] IClrDeclaredElement contextElement)
        {
            return TypeFactory.CreateTypeByCLRName(clrTypeName, contextElement.Module, contextElement.ResolveContext);
        }
    }
}
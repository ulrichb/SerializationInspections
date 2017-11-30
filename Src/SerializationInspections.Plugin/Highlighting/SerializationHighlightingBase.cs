using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperExtensionsShared.Highlighting;

namespace SerializationInspections.Plugin.Highlighting
{
    /// <summary>
    /// A base class for the serialization highlighting types.
    /// </summary>
    public abstract class SerializationHighlightingBase : SimpleTreeNodeHighlightingBase<IClassLikeDeclaration>
    {
        protected SerializationHighlightingBase(IClassLikeDeclaration classLikeDeclaration, string toolTipText)
            : base(classLikeDeclaration, toolTipText)
        {
        }

        public override DocumentRange CalculateRange() => TreeNode.GetNameDocumentRange();
    }
}

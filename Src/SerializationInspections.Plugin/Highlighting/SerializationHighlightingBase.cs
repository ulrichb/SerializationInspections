using JetBrains.Annotations;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.Tree;
#if RESHARPER8
using IHighlighting = JetBrains.ReSharper.Daemon.Impl.IHighlightingWithRange;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

namespace SerializationInspections.Plugin.Highlighting
{
    /// <summary>
    /// A base class for a serialization highlighting.
    /// </summary>
    public abstract class SerializationHighlightingBase : IHighlighting
    {
        private readonly string _toolTipText;

        protected SerializationHighlightingBase([NotNull] ITypeDeclaration typeDeclaration, [NotNull] string toolTipText)
        {
            _toolTipText = toolTipText;
            TypeDeclaration = typeDeclaration;
        }

        [NotNull]
        public ITypeDeclaration TypeDeclaration { get; private set; }

        public string ToolTip
        {
            get { return _toolTipText; }
        }

        public string ErrorStripeToolTip
        {
            get { return _toolTipText; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        public bool IsValid()
        {
            return TypeDeclaration.IsValid();
        }

        public DocumentRange CalculateRange()
        {
            return TypeDeclaration.GetNameDocumentRange();
        }
    }
}
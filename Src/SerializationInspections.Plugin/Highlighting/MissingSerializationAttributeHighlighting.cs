using JetBrains.Annotations;
using SerializationInspections.Plugin.Highlighting;
using JetBrains.ReSharper.Psi.Tree;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

[assembly: RegisterConfigurableSeverity(
    MissingSerializationAttributeHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingSerializationAttributeHighlighting.Title,
    MissingSerializationAttributeHighlighting.Description,
    Severity.WARNING,
    solutionAnalysisRequired: false)]

namespace SerializationInspections.Plugin.Highlighting
{
    /// <summary>
    /// A highlighting for missing serialization attributes.
    /// </summary>
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP",
        OverlapResolve = OverlapResolveKind.WARNING,
        ToolTipFormatString = Message)]
    public class MissingSerializationAttributeHighlighting : SerializationHighlightingBase
    {
        public const string SeverityId = "MissingSerializationAttribute";

        private const string Message = "{0} should be marked with the [Serializable] attribute";

        public const string Title = "Missing [Serializable] attribute";

        public const string Description = Title;

        public MissingSerializationAttributeHighlighting([NotNull] ITypeDeclaration typeDeclaration, [NotNull] string elementDescription)
            : base(typeDeclaration, string.Format(Message, elementDescription))
        {
        }
    }
}
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using SerializationInspections.Plugin.Highlighting;

[assembly: RegisterConfigurableSeverity(
    MissingSerializationAttributeHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingSerializationAttributeHighlighting.Title,
    MissingSerializationAttributeHighlighting.Description,
    Severity.WARNING)]

namespace SerializationInspections.Plugin.Highlighting
{
    /// <summary>
    /// A highlighting for missing serialization attributes.
    /// </summary>
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING, ToolTipFormatString = Message)]
    public class MissingSerializationAttributeHighlighting : SerializationHighlightingBase
    {
        public const string SeverityId = "MissingSerializationAttribute";
        public const string Title = "Missing [Serializable] attribute";
        private const string Message = "{0} should be marked with the [Serializable] attribute";
        public const string Description = Title;

        public MissingSerializationAttributeHighlighting(IClassLikeDeclaration classLikeDeclaration, string elementDescription)
            : base(classLikeDeclaration, string.Format(Message, elementDescription))
        {
        }
    }
}

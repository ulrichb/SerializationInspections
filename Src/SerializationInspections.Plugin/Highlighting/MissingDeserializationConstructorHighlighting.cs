using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using SerializationInspections.Plugin.Highlighting;

[assembly: RegisterConfigurableSeverity(
    MissingDeserializationConstructorHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingDeserializationConstructorHighlighting.Title,
    MissingDeserializationConstructorHighlighting.Description,
    Severity.WARNING)]

namespace SerializationInspections.Plugin.Highlighting
{
    /// <summary>
    /// A highlighting for missing deserialization constructors.
    /// </summary>
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING, ToolTipFormatString = Message)]
    public class MissingDeserializationConstructorHighlighting : SerializationHighlightingBase
    {
        public const string SeverityId = "MissingDeserializationConstructor";
        public const string Title = "Missing deserialization constructor";
        private const string Message = "Missing deserialization constructor";
        public const string Description = Title;

        public MissingDeserializationConstructorHighlighting(IClassLikeDeclaration classLikeDeclaration)
            : base(classLikeDeclaration, Message)
        {
        }
    }
}

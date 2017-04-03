using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;
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
    [ConfigurableSeverityHighlighting(SeverityId, "CSHARP", OverlapResolve = OverlapResolveKind.WARNING, ToolTipFormatString = Message)]
    public class MissingDeserializationConstructorHighlighting : SerializationHighlightingBase
    {
        public const string SeverityId = "MissingDeserializationConstructor";
        public const string Title = "Missing deserialization constructor";
        private const string Message = "Missing deserialization constructor";
        public const string Description = Title;

        public MissingDeserializationConstructorHighlighting([NotNull] ITypeDeclaration typeDeclaration)
            : base(typeDeclaration, Message)
        {
        }
    }
}

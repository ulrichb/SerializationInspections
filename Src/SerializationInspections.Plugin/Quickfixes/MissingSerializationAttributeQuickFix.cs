using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using SerializationInspections.Plugin.Highlighting;
using SerializationInspections.Plugin.Infrastructure;

namespace SerializationInspections.Plugin.Quickfixes
{
    /// <summary>
    /// A quick fix for the <see cref="MissingSerializationAttributeHighlighting"/> which adds the missing attribute.
    /// </summary>
    [QuickFix]
    public class MissingSerializationAttributeQuickFix : QuickFixBase
    {
        private readonly MissingSerializationAttributeHighlighting _highlighting;

        public MissingSerializationAttributeQuickFix([NotNull] MissingSerializationAttributeHighlighting highlighting)
        {
            _highlighting = highlighting;
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return GetValidAttributesOwnerDeclaration(_highlighting.TreeNode) != null;
        }

        public override string Text => "Add [Serializable] attribute";

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var attributesOwnerDeclaration = GetValidAttributesOwnerDeclaration(_highlighting.TreeNode);

            if (attributesOwnerDeclaration != null)
            {
                var elementFactory = CSharpElementFactory.GetInstance(attributesOwnerDeclaration, applyCodeFormatter: true);

                var serializableAttributeType = PredefinedType.SERIALIZABLE_ATTRIBUTE_CLASS.CreateTypeInContextOf(attributesOwnerDeclaration);
                var attribute = elementFactory.CreateAttribute(serializableAttributeType.GetTypeElement().NotNull());

                attributesOwnerDeclaration.AddAttributeAfter(attribute, null);
            }

            return null;
        }

        [CanBeNull]
        private IAttributesOwnerDeclaration GetValidAttributesOwnerDeclaration([NotNull] ITypeDeclaration typeDeclaration)
        {
            if (!ValidUtils.Valid(typeDeclaration))
                return null;

            return typeDeclaration as IAttributesOwnerDeclaration;
        }
    }
}
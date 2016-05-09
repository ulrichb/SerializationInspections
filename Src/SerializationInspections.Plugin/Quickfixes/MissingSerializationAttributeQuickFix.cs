using System;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
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
    public class MissingSerializationAttributeQuickFix : ValidDeclarationTypeQuickFixBase<IAttributesOwnerDeclaration>
    {
        public MissingSerializationAttributeQuickFix([NotNull] MissingSerializationAttributeHighlighting highlighting) :
            base(highlighting.TreeNode)
        {
        }

        public override string Text => "Add [Serializable] attribute";

        protected override Action<ITextControl> ExecuteOnDeclaration(IAttributesOwnerDeclaration attributesOwnerDeclaration)
        {
            var elementFactory = CSharpElementFactory.GetInstance(attributesOwnerDeclaration, applyCodeFormatter: true);

            var serializableAttributeType = PredefinedType.SERIALIZABLE_ATTRIBUTE_CLASS.CreateTypeInContextOf(attributesOwnerDeclaration);
            var attribute = elementFactory.CreateAttribute(serializableAttributeType.GetTypeElement().NotNull());

            attributesOwnerDeclaration.AddAttributeBefore(attribute, null);

            return null;
        }
    }
}
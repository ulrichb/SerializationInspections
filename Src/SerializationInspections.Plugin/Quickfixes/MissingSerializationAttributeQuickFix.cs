using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperExtensionsShared.QuickFixes;
using SerializationInspections.Plugin.Highlighting;
using SerializationInspections.Plugin.Infrastructure;

namespace SerializationInspections.Plugin.Quickfixes
{
    /// <summary>
    /// A quick fix for the <see cref="MissingSerializationAttributeHighlighting"/> which adds the missing attribute.
    /// </summary>
    [QuickFix]
    public class MissingSerializationAttributeQuickFix :
        SimpleQuickFixBase<MissingSerializationAttributeHighlighting, IClassLikeDeclaration>
    {
        public MissingSerializationAttributeQuickFix(MissingSerializationAttributeHighlighting highlighting) :
            base(highlighting)
        {
        }

        public override string Text => "Add [Serializable] attribute";

        protected override bool IsAvailableForTreeNode(IUserDataHolder cache) => true;

        [CanBeNull]
        protected override Action<ITextControl> ExecutePsiTransaction(ISolution _, IProgressIndicator __)
        {
            var classLikeDeclaration = Highlighting.TreeNode;

            var elementFactory = CSharpElementFactory.GetInstance(classLikeDeclaration, applyCodeFormatter: true);

            var serializableAttributeType = PredefinedType.SERIALIZABLE_ATTRIBUTE_CLASS.CreateTypeInContextOf(classLikeDeclaration);
            var attribute = elementFactory.CreateAttribute(serializableAttributeType.GetTypeElement().NotNull());

            classLikeDeclaration.AddAttributeBefore(attribute, null);

            return null;
        }
    }
}

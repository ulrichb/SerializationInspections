using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperExtensionsShared.QuickFixes;
using SerializationInspections.Plugin.Highlighting;
using SerializationInspections.Plugin.Infrastructure;

namespace SerializationInspections.Plugin.Quickfixes
{
    /// <summary>
    /// A quick fix for the <see cref="MissingDeserializationConstructorHighlighting"/> which generates a new constructor
    /// with an optional base call.
    /// </summary>
    [QuickFix]
    public class MissingDeserializationConstructorQuickFix :
        SimpleQuickFixBase<MissingDeserializationConstructorHighlighting, IClassLikeDeclaration>
    {
        public MissingDeserializationConstructorQuickFix(MissingDeserializationConstructorHighlighting highlighting) :
            base(highlighting)
        {
        }

        protected override bool IsAvailableForTreeNode(IUserDataHolder cache) => true;

        public override string Text => "Create deserialization constructor";

        [CanBeNull]
        protected override Action<ITextControl> ExecutePsiTransaction(ISolution _, IProgressIndicator __)
        {
            var classLikeDeclaration = Highlighting.HighlightingNode;

            var elementFactory = CSharpElementFactory.GetInstance(classLikeDeclaration, applyCodeFormatter: true);

            var constructorDeclaration = CreateDeserializationConstructor(classLikeDeclaration, elementFactory);

            var addedConstructorDeclaration = classLikeDeclaration.AddClassMemberDeclaration(constructorDeclaration);
            var offset = addedConstructorDeclaration.GetNameDocumentRange().TextRange.EndOffset;
            Assertion.Assert(offset >= 0, "offset >= 0");

            return textControl => textControl.Caret.MoveTo(offset, CaretVisualPlacement.Generic);
        }

        private static IConstructorDeclaration CreateDeserializationConstructor(
            IClassLikeDeclaration classLikeDeclaration,
            CSharpElementFactory elementFactory)
        {
            var result = elementFactory.CreateConstructorDeclaration();

            result.SetAccessRights(classLikeDeclaration is IClassDeclaration ? AccessRights.PROTECTED : AccessRights.PRIVATE);

            var infoParameterDeclaration = AddInfoParameter(result, classLikeDeclaration);
            var contextParameterDeclaration = AddContextParameter(result, classLikeDeclaration);

            if (HasBaseClassWithDeserializationConstructor(classLikeDeclaration))
            {
                var initializer = CreateBaseConstructorInitializer(elementFactory, infoParameterDeclaration, contextParameterDeclaration);
                result.SetInitializer(initializer);
            }

            return result;
        }

        private static IParameterDeclaration AddInfoParameter(IConstructorDeclaration constructorDeclaration, IDeclaration contextDeclaration)
        {
            var type = SerializationUtilities.SerializationInfoTypeName.CreateTypeInContextOf(contextDeclaration);
            return constructorDeclaration.AddParameterDeclarationBefore(ParameterKind.VALUE, type, "info", null);
        }


        private static IParameterDeclaration AddContextParameter(IConstructorDeclaration constructorDeclaration, IDeclaration contextDeclaration)
        {
            var type = SerializationUtilities.StreamingContextTypeName.CreateTypeInContextOf(contextDeclaration);
            return constructorDeclaration.AddParameterDeclarationBefore(ParameterKind.VALUE, type, "context", null);
        }

        private static bool HasBaseClassWithDeserializationConstructor(IClassLikeDeclaration classLikeDeclaration)
        {
            if (classLikeDeclaration.DeclaredElement is IClass declaredClass)
            {
                var superClass = declaredClass.GetSuperClass().NotNull("It's unexpected that the highlighting is displayed on System.Object");

                // NOTE: Yes, this will also return true if the base deserialization constructor is private/internal (which means that 
                // there is probably an issue). But it may be better to leave non-compilable code than having the missing base call.
                return SerializationUtilities.HasDeserializationConstructor(superClass);
            }

            return false;
        }


        private static IConstructorInitializer CreateBaseConstructorInitializer(
            CSharpElementFactory elementFactory,
            IParameterDeclaration infoParameterDeclaration,
            IParameterDeclaration contextParameterDeclaration)
        {
            var result = elementFactory.CreateBaseConstructorInitializer();

            var infoArgumentExpression = elementFactory.CreateExpression("$0", infoParameterDeclaration.DeclaredName);
            result.AddArgumentBefore(elementFactory.CreateArgument(ParameterKind.VALUE, infoArgumentExpression), null);

            var contextArgumentExpression = elementFactory.CreateExpression("$0", contextParameterDeclaration.DeclaredName);
            result.AddArgumentBefore(elementFactory.CreateArgument(ParameterKind.VALUE, contextArgumentExpression), null);
            return result;
        }
    }
}

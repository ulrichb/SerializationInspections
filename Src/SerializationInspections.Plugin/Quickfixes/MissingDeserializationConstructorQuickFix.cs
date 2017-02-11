using System;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
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
    /// A quick fix for the <see cref="MissingDeserializationConstructorHighlighting"/> which generates a new constructor
    /// with an optional base call.
    /// </summary>
    [QuickFix]
    public class MissingDeserializationConstructorQuickFix : ValidDeclarationTypeQuickFixBase<IClassLikeDeclaration>
    {
        public MissingDeserializationConstructorQuickFix([NotNull] MissingDeserializationConstructorHighlighting highlighting) :
            base(highlighting.TreeNode)
        {
        }

        public override string Text => "Create deserialization constructor";

        protected override Action<ITextControl> ExecuteOnDeclaration(IClassLikeDeclaration classLikeDeclaration)
        {
            var elementFactory = CSharpElementFactory.GetInstance(classLikeDeclaration, applyCodeFormatter: true);

            var constructorDeclaration = CreateDeserializationConstructor(classLikeDeclaration, elementFactory);

            var addedConstructorDeclaration = classLikeDeclaration.AddClassMemberDeclaration(constructorDeclaration);
            var offset = addedConstructorDeclaration.GetNameDocumentRange().TextRange.EndOffset;
            Assertion.Assert(offset >= 0, "offset >= 0");

            return textControl => textControl.Caret.MoveTo(offset, CaretVisualPlacement.Generic);
        }

        [NotNull]
        private static IConstructorDeclaration CreateDeserializationConstructor(
            [NotNull] IClassLikeDeclaration classLikeDeclaration,
            [NotNull] CSharpElementFactory elementFactory)
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

        [NotNull]
        private static IParameterDeclaration AddInfoParameter(
            [NotNull] IConstructorDeclaration constructorDeclaration,
            [NotNull] IDeclaration contextDeclaration)
        {
            var type = SerializationUtilities.SerializationInfoTypeName.CreateTypeInContextOf(contextDeclaration);
            return constructorDeclaration.AddParameterDeclarationBefore(ParameterKind.VALUE, type, "info", null);
        }

        [NotNull]
        private static IParameterDeclaration AddContextParameter(
            [NotNull] IConstructorDeclaration constructorDeclaration,
            [NotNull] IDeclaration contextDeclaration)
        {
            var type = SerializationUtilities.StreamingContextTypeName.CreateTypeInContextOf(contextDeclaration);
            return constructorDeclaration.AddParameterDeclarationBefore(ParameterKind.VALUE, type, "context", null);
        }

        private static bool HasBaseClassWithDeserializationConstructor([NotNull] IClassLikeDeclaration classLikeDeclaration)
        {
            var declaredClass = classLikeDeclaration.DeclaredElement as IClass;
            if (declaredClass == null)
                return false;

            var superClass = declaredClass.GetSuperClass().NotNull("It's unexpected that the highlighting is displayed on System.Object");

            // NOTE: Yes, this will also return true if the base deserialization constructor is private/internal (which means that 
            // there is probably an issue). But it may be better to leave non-compilable code than having the missing base call.
            return SerializationUtilities.HasDeserializationConstructor(superClass);
        }

        [NotNull]
        private static IConstructorInitializer CreateBaseConstructorInitializer(
            [NotNull] CSharpElementFactory elementFactory,
            [NotNull] IParameterDeclaration infoParameterDeclaration,
            [NotNull] IParameterDeclaration contextParameterDeclaration)
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

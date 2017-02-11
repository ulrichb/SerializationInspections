using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.Util;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace SerializationInspections.Plugin.Quickfixes
{
    /// <summary>
    /// A base class for Quick Fixes which provides <see cref="ExecuteOnDeclaration"/> for valid declarations
    /// of type <see cref="TDeclaration"/>.
    /// </summary>
    public abstract class ValidDeclarationTypeQuickFixBase<TDeclaration> : QuickFixBase where TDeclaration : class, IDeclaration
    {
        private readonly IDeclaration _declaration;

        protected ValidDeclarationTypeQuickFixBase(IDeclaration declaration)
        {
            _declaration = declaration;
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return GetValidDeclarationOrNull() != null;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var validDeclaration = GetValidDeclarationOrNull().NotNull( /* ensured by IsAvailable() */);

            return ExecuteOnDeclaration(validDeclaration);
        }

        [CanBeNull]
        protected abstract Action<ITextControl> ExecuteOnDeclaration([NotNull] TDeclaration declaration);

        [CanBeNull]
        private TDeclaration GetValidDeclarationOrNull()
        {
            return CheckForIsValid(_declaration) as TDeclaration;
        }

        [CanBeNull]
        [ExcludeFromCodeCoverage /* There is no easy way to test Quick Fixes with invalid declarations */]
        private IDeclaration CheckForIsValid(IDeclaration declaration)
        {
            return ValidUtils.Valid(declaration) ? declaration : null;
        }
    }
}

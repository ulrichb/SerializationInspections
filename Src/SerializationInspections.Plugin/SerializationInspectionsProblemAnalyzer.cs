using System.Collections.Generic;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.Util;
using JetBrains.Util.Logging;
using ReSharperExtensionsShared.ProblemAnalyzers;
using SerializationInspections.Plugin.Highlighting;
using SerializationInspections.Plugin.Infrastructure;

namespace SerializationInspections.Plugin
{
    /// <summary>
    /// A problem analyzer for the serialization inspections.
    /// </summary>
    [ElementProblemAnalyzer(
        typeof(IClassLikeDeclaration),
        HighlightingTypes = new[]
        {
            typeof(MissingSerializationAttributeHighlighting),
            typeof(MissingDeserializationConstructorHighlighting)
        })]
    public class SerializationInspectionsProblemAnalyzer : SimpleElementProblemAnalyzer<IClassLikeDeclaration, ITypeElement>
    {
        private static readonly ILogger Log = Logger.GetLogger(typeof(SerializationInspectionsProblemAnalyzer));

        public SerializationInspectionsProblemAnalyzer()
        {
            Log.Verbose(".ctor");
        }

        protected override void Run(
            IClassLikeDeclaration declaration,
            ITypeElement typeElement,
            ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            foreach (var highlighting in HandleTypeElement(declaration, typeElement))
            {
                consumer.AddHighlighting(highlighting);
            }
        }

        private IEnumerable<IHighlighting> HandleTypeElement(IClassLikeDeclaration declaration, ITypeElement typeElement)
        {
            var serializableAttributeTypeName = PredefinedType.SERIALIZABLE_ATTRIBUTE_CLASS;
            var serializableAttributeType = serializableAttributeTypeName.CreateTypeInContextOf(declaration);

            // Test if the SerializableAttribute is resolvable (e.g. not the case for Windows Phone targets)
            if (serializableAttributeType.GetTypeElement() != null)
            {
                var hasSerializableAttribute = typeElement.HasAttributeInstance(serializableAttributeTypeName, inherit: false);

                if (!hasSerializableAttribute)
                {
                    if (IsException(typeElement))
                    {
                        yield return new MissingSerializationAttributeHighlighting(declaration, "Exceptions");
                    }
                    else if (IsImplementingSerializableInterface(typeElement) && !(typeElement is IInterface))
                    {
                        yield return new MissingSerializationAttributeHighlighting(declaration, "A type implementing ISerializable");
                    }
                }

                if (hasSerializableAttribute && IsSerializable(typeElement) && !(typeElement is IDelegate))
                {
                    if (!SerializationUtilities.HasDeserializationConstructor(typeElement))
                    {
                        yield return new MissingDeserializationConstructorHighlighting(declaration);
                    }
                }
            }
        }

        private static bool IsException(ITypeElement typeElement)
        {
            return TypeFactory.CreateType(typeElement).IsSubtypeOf(PredefinedType.EXCEPTION_FQN.CreateTypeInContextOf(typeElement));
        }

        private static bool IsSerializable(ITypeElement typeElement)
        {
            return TypeFactory.CreateType(typeElement).IsSubtypeOf(PredefinedType.ISERIALIZABLE_FQN.CreateTypeInContextOf(typeElement));
        }

        private static bool IsImplementingSerializableInterface(ITypeElement typeElement)
        {
            return typeElement.GetSuperTypes().Contains(PredefinedType.ISERIALIZABLE_FQN.CreateTypeInContextOf(typeElement));
        }
    }
}

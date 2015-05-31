using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Infrastructure;
using JetBrains.Annotations;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using JetBrains.Util.Logging;
using SerializationInspections.Plugin.Highlighting;
using SerializationInspections.Plugin.Infrastructure;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

namespace SerializationInspections.Plugin
{
    /// <summary>
    /// A problem analyzer for the serialization inspections.
    /// </summary>
    [ElementProblemAnalyzer(typeof (ITypeDeclaration), HighlightingTypes = new[] {typeof (MissingDeserializationConstructorHighlighting)})]
    public class SerializationInspectionsProblemAnalyzer : ElementProblemAnalyzer<ITypeDeclaration>
    {
        private static readonly ILogger Log = Logger.GetLogger(typeof (SerializationInspectionsProblemAnalyzer));

        public SerializationInspectionsProblemAnalyzer()
        {
            Log.LogMessage(LoggingLevel.INFO, ".ctor");
        }

        protected override void Run(ITypeDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
#if DEBUG
            var stopwatch = Stopwatch.StartNew();
#endif

            var typeElement = element.DeclaredElement;

            var highlightingResults = HandleTypeElement(element, typeElement).ToList();

            highlightingResults.ForEach(x => consumer.AddHighlighting(x));

#if DEBUG
            var message = DebugUtilities.FormatIncludingContext(typeElement) + " => ["
                          + string.Join(", ", highlightingResults.Select(x => x.GetType().Name)) + "]";

            Log.LogMessage(LoggingLevel.VERBOSE, DebugUtilities.FormatWithElapsed(message, stopwatch));
#endif
        }

        private IEnumerable<IHighlighting> HandleTypeElement([NotNull] ITypeDeclaration declaration, [CanBeNull] ITypeElement typeElement)
        {
            if (typeElement != null)
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
        }

        private static bool IsException([NotNull] ITypeElement typeElement)
        {
            return TypeFactory.CreateType(typeElement).IsSubtypeOf(PredefinedType.EXCEPTION_FQN.CreateTypeInContextOf(typeElement));
        }

        private static bool IsSerializable([NotNull] ITypeElement typeElement)
        {
            return TypeFactory.CreateType(typeElement).IsSubtypeOf(PredefinedType.ISERIALIZABLE_FQN.CreateTypeInContextOf(typeElement));
        }

        private static bool IsImplementingSerializableInterface([NotNull] ITypeElement typeElement)
        {
            return typeElement.GetSuperTypes().Contains(PredefinedType.ISERIALIZABLE_FQN.CreateTypeInContextOf(typeElement));
        }
    }
}
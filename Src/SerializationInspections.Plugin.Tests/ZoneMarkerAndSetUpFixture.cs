using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;
using SerializationInspections.Plugin.Tests;

[assembly: RequiresSTA]

namespace SerializationInspections.Plugin.Tests
{
    [ZoneDefinition]
    public interface ISerializationInspectionsTestEnvironmentZone : ITestsZone, IRequire<PsiFeatureTestZone>
    {
    }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ISerializationInspectionsTestEnvironmentZone>
    {
    }
}

// ReSharper disable once CheckNamespace
[SetUpFixture]
public class TestEnvironmentSetUpFixture : ExtensionTestEnvironmentAssembly<ISerializationInspectionsTestEnvironmentZone>
{
}

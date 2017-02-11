using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;

namespace SerializationInspections.Plugin.Quickfixes
{
    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>
    {
    }
}

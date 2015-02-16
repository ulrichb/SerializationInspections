#if RESHARPER9

using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;

namespace SerializationInspections.Plugin
{
    /// <summary>
    /// ReSharper platform zone marker.
    /// </summary>
    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>
    {
    }
}

#endif
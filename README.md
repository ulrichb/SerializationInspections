:warning: This extension isn't maintained anymore. If you are interesed to become the new maintainer, please contact me in the Gitter chat.

# Serialization Inspections ReSharper Extension

[![Build status](https://teamcity.jetbrains.com/app/rest/builds/buildType:(id:OpenSourceProjects_SerializationInspections_Ci),branch:master,running:any/statusIcon.svg)](https://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_SerializationInspections_Ci)
[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/ulrichb/SerializationInspections?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
<a href="https://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_SerializationInspections_Ci&branch=master"><img src="https://dl.dropbox.com/s/254s0904t2qd31z/master-linecoverage.svg" alt="Line Coverage" title="Line Coverage"></a>
<a href="https://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_SerializationInspections_Ci&branch=master"><img src="https://dl.dropbox.com/s/yjhqgm81fawf9is/master-branchcoverage.svg" alt="Branch Coverage" title="Branch Coverage"></a>

[ReSharper Gallery Page](http://resharper-plugins.jetbrains.com/packages/ReSharper.SerializationInspections/)

[History of changes](History.md)

## Description

Serialization Inspections is simple ReSharper extension which adds the following inspections for binary serializable types.

- "Missing [Serializable] attribute" warning
    + For classes derived from `Exception` but without the `[Serializable]` attribute.
    + For types directly implementing [`ISerializable`](https://msdn.microsoft.com/en-us/library/vstudio/system.runtime.serialization.iserializable.aspx) but without the `[Serializable]` attribute.
    + This warning also offers a quick fix (Alt+Enter) to generate the attribute.

- "Missing deserialization constructor" warning
    + For `[Serializable]` types which are derived from `ISerializable` but have no [deserialization constructor](https://msdn.microsoft.com/en-us/library/vstudio/ty01x675.aspx) (like `protected MyClass(SerializationInfo info, StreamingContext context)`).
    + This warning also offers a quick fix (Alt+Enter) to generate the constructor.

# Serialization Inspections ReSharper Extension

[![Build status](https://ci.appveyor.com/api/projects/status/2s7coc39oicpr6p6/branch/master?svg=true)](https://ci.appveyor.com/project/ulrichb/serializationinspections/branch/master)
[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/ulrichb/SerializationInspections?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Line Coverage](https://dl.dropbox.com/s/us3qukmj4fs1dpt/master-linecoverage.svg)](https://ci.appveyor.com/api/projects/ulrichb/serializationinspections/artifacts/Build/Output/TestCoverage.zip?branch=master)
[![Branch Coverage](https://dl.dropbox.com/s/q74g292ut1uxgxu/master-branchcoverage.svg)](https://ci.appveyor.com/api/projects/ulrichb/serializationinspections/artifacts/Build/Output/TestCoverage.zip?branch=master)

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

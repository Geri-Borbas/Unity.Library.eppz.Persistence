# eppz.Persistence [![Build Status](https://travis-ci.org/eppz/Unity.Library.eppz.Persistence.png?branch=master)](https://travis-ci.org/eppz/Unity.Library.eppz.Persistence)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)

Object serialization (Binary, JSON, GZip).

## `String.cs`

* GZip `string` extensions
    + Using Base64 encoding of compressed bytes.
    + Using `System.IO.Compression` classes included in Unity 5.5+ (see issue `569612` on [5.5.0f3 Release Notes](https://unity3d.com/unity/whats-new/unity-5.5.0)).
    + `Zip()`
        + `"Batman".Zip()` gives you `"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA="`.
    + `Unzip()`
        + `"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=".Unzip()` gives you `"Batman"`.
    + Can be nicely decompressed / inflated using **Javascript** (using [pako](https://github.com/nodeca/pako))
        + See [http://jsfiddle.net/9yH7M/845/](http://jsfiddle.net/9yH7M/845/)
    + See test cases in [`Editor/Test/String.cs`](Editor/Test/String.cs) for more.

## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).

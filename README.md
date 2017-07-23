# eppz.Persistence [![Build Status](https://travis-ci.org/eppz/Unity.Test.eppz.png?branch=master)](https://travis-ci.org/eppz/Unity.Test.eppz)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)

Object serialization (Binary, JSON, Gzip) wrapped up for the everyday.

```csharp
// Object To JSON.
string myString = myObject.SerializeToString();

// Object to JSON with Gzip.
string myString = myObject.SerializeToString().Zip();

// Object to a JSON file (using `json` extension).
myObject.SerializeToFileAt(Application.persistentDataPath + "object");

// Or the same with `BinarySerializer`.
binarySerializer.SetDefaultSerializer();

// Object To Binary (as Base64 string).
string myString = myObject.SerializeToString();

// Object to Binray (as Gzipped Base64 string).
string myString = myObject.SerializeToString().Zip();

// Object to a Binary file (using `bytes` extension).
myObject.SerializeToFileAt(Application.persistentDataPath + "object");

// String to object.
Entity myObject = myString.DeserializeToObject<Entity>();

// Gzipped string to object.
Entity myObject = myString.Unzip().DeserializeToObject<Entity>();

// File to object (either JSON or Binary).
Entity myObject = serializer.FileToObject<Entity>(Application.persistentDataPath + "object");

// Resource to object (either JSON or Binary).
Entity myObject = serializer.ResourceToObject<Entity>(Application.persistentDataPath + "object");

// File or resource to object (either JSON or Binary).
Entity object = serializer.FileOrResourceToObject<Entity>(
    Application.persistentDataPath + "object",
    "object"
);
```

> 💡 See editor test fixture at [`Editor/Test/Serializer.cs`](Editor/Test/Serializer.cs) for details.

## [`Serializer.cs`](Serializer.cs)

A common interface to both (Binary, JSON) serializers. See editor tests at [`Editor/Test/Serializer.cs`](Editor/Test/Serializer.cs) for details (these are common tests used by both serializer), or the class itself at [`Serializer.cs`](Serializer.cs).

* `ObjectToString()`
    + Serialize an object to string.
* `ObjectToFile()`
    + Serialize an object to a given file.
* `StringToObject<T>()`
    + Deserialize a string to object.
* `FileToObject<T>()`
    + Deserialize a file to object.
* `ResourceToObject<T>()`
    + Deserialize a resource to object.
* `FileOrResourceToObject<T>()`
    + Deserialize a file to object, or deserialize a resource (if the file not found).

## [`BinarySerializer.cs`](BinarySerializer.cs)

* Wraps up `BinaryFormatter` under the hood.
* Uses `bytes` extension (recognized as asset by Unity).
* Base64 string interpretation.

## [`JSONSerializer.cs`](JSONSerializer.cs)

* Wraps up `JsonUtility` under the hood.
* Uses `json` extension (recognized as asset by Unity).
* JSON string interpretation.
* Can apply sources to existing objects.
    + `ApplyStringTo()`
        + Deserialize a string to an existing object.
    + `ApplyFileTo()`
        + Deserialize a file to an extisting object.
	+ `ApplyResourceTo()`
        + Deserialize a resource to an existing object.

## [`Extensions/String.cs`](Extensions/String.cs)

* Byte conversion
    + `string` to `byte[]` array
        + `Bytes()` (UTF8)
        + `Base64Bytes()`
        + See reverse counterparts in [`Extensions/Bytes.cs`](Extensions/Bytes.cs)
* Gzip
    + Using Base64 encoding of compressed bytes.
    + Using `System.IO.Compression` classes included in Unity 5.5+ (see issue `569612` on [5.5.0f3 Release Notes](https://unity3d.com/unity/whats-new/unity-5.5.0)).
    + `Zip()`
        + `"Batman".Zip()` gives you `"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA="`.
    + `Unzip()`
        + `"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=".Unzip()` gives you `"Batman"`.
    + Can be nicely decompressed / inflated using **Javascript** (using [pako](https://github.com/nodeca/pako)).
        + See [http://jsfiddle.net/9yH7M/845/](http://jsfiddle.net/9yH7M/845/)
    + See test cases in [`Editor/Test/String.cs`](Editor/Test/String.cs) for more.

The 600 character long Base64 string below matches the 1474 character long JSON minified string below. Which was 2672 characters beautified, that counts 22% compression ratio.

```
H4sIAAAAAAAAA62UT2/UMBDFv0pvvoRoxmN7PDlzQQIOLDdUIbMbdqN6kyXJImjV784EVLF/SrUp9SmxR5r387znOzNs0q5etGk3bLpxMNWnO9OmbW0q8zb16/rqY9+kdp3rKzSFScux+a5nY7+vCzPUuV6O9cpUX1MedGPXDc3YdO3nH6Z6BSUCehRx1kYKBMQHFT//VFiSIN47AdRv8IXpuzFNFaZyrpRpBYyewDK5wuRueXPQMDet/i4mhPcqepJvFt/2qa9V7Ltmtcp/AXTnhMia6/viX7h2Ji6WHj0TMcbIFhjohFYLKGK0whQDBnF0CEvoX5gOj+jOy+fNMkIMNjAihegDxLNRRkLgQJG8QJBo/wvuEes9ObvFNuX8bKtC6T2y2iz4gD46d2JUWyq5OL0Adoxi3SFb4PIiuCfkzrXaQ7LUYyoZPNozwQzOedbcgQVAOcoV0mzBHzbd9st+mB0Ji4EdWnA2ILOcuGY6B4kcovoKBECOZPrn3OyDg2aNnyS6qC8QkwpC587MHX6/U068leA4HiVX4CJvX5DXR01+rSy5WWsj7bPb397m+s1rJQVdiOb+F1EcMerCBQAA
```

```
{'shapeSnapshots':[{'name':'Large Triangle 1','active':true,'selected':false,'position_x':-0.10151994228363037,'position_y':-0.12396955490112305,'rotation':44.999996185302734,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 2']},{'name':'Large Triangle 2','active':true,'selected':false,'position_x':-1.5157337188720703,'position_y':-1.5381829738616943,'rotation':315,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 1']},{'name':'Middle Triangle','active':true,'selected':false,'position_x':-0.8086267113685608,'position_y':-0.8310763835906982,'rotation':315,'locked':false,'linkedShapeNames':['Square','Large Triangle 1','Large Triangle 2']},{'name':'Small Triangle 1','active':true,'selected':false,'position_x':0.5517618656158447,'position_y':2.267948627471924,'rotation':67.5,'locked':false,'linkedShapeNames':[]},{'name':'Small Triangle 2','active':true,'selected':false,'position_x':-0.10151970386505127,'position_y':2.7044572830200195,'rotation':135,'locked':false,'linkedShapeNames':[]},{'name':'Rhombus','active':true,'selected':false,'position_x':-1.2167412042617798,'position_y':1.2109876871109009,'rotation':157.5,'locked':false,'linkedShapeNames':[]},{'name':'Square','active':true,'selected':false,'position_x':0.3984801173210144,'position_y':-0.6239694952964783,'rotation':90,'locked':false,'linkedShapeNames':['Middle Triangle','Large Triangle 1','Large Triangle 2']}],'align':0,'puzzleID':'000011'}
``` 
## [`Extensions/Bytes.cs`](Extensions/Bytes.cs)

* String conversion
    + Byte array to `string`
        + `String()` (UTF8)
        + `Base64String()`
* Gzip
    + The base of the Gzip `string` extensions above
    + `Compress()`
    + `Decompress()`

## [`Extensions/Object.cs`](Extensions/Object.cs)

* Wrapper for `Serializer` functionality.
    + `SerializeToString()`
	+ `SerializeToFileAt()`

## [`Extensions/Stream.cs`](Extensions/Stream.cs)

* A single extension to mimic a .NET 4 behaviour in .NET 2 (to be used in Gzip extensions above).
    + `_CopyTo()`

## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).

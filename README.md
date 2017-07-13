# eppz.Persistence [![Build Status](https://travis-ci.org/eppz/Unity.Test.eppz.png?branch=master)](https://travis-ci.org/eppz/Unity.Test.eppz)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)

Object serialization (Binary, JSON, GZip).

## `String.cs` extensions

* GZip
    + Using Base64 encoding of compressed bytes.
    + Using `System.IO.Compression` classes included in Unity 5.5+ (see issue `569612` on [5.5.0f3 Release Notes](https://unity3d.com/unity/whats-new/unity-5.5.0)).
    + `Zip()`
        + `"Batman".Zip()` gives you `"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA="`.
    + `Unzip()`
        + `"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=".Unzip()` gives you `"Batman"`.
    + Can be nicely decompressed / inflated using **Javascript** (using [pako](https://github.com/nodeca/pako))
        + See [http://jsfiddle.net/9yH7M/845/](http://jsfiddle.net/9yH7M/845/)
    + See test cases in [`Editor/Test/String.cs`](Editor/Test/String.cs) for more.

The 600 character long Base64 string below matches the 1474 character long JSON minified string below. Which was 2672 characters beautified, that counts 22% compression ratio.

```
H4sIAAAAAAAAA62UT2/UMBDFv0pvvoRoxmN7PDlzQQIOLDdUIbMbdqN6kyXJImjV784EVLF/SrUp9SmxR5r387znOzNs0q5etGk3bLpxMNWnO9OmbW0q8zb16/rqY9+kdp3rKzSFScux+a5nY7+vCzPUuV6O9cpUX1MedGPXDc3YdO3nH6Z6BSUCehRx1kYKBMQHFT//VFiSIN47AdRv8IXpuzFNFaZyrpRpBYyewDK5wuRueXPQMDet/i4mhPcqepJvFt/2qa9V7Ltmtcp/AXTnhMia6/viX7h2Ji6WHj0TMcbIFhjohFYLKGK0whQDBnF0CEvoX5gOj+jOy+fNMkIMNjAihegDxLNRRkLgQJG8QJBo/wvuEes9ObvFNuX8bKtC6T2y2iz4gD46d2JUWyq5OL0Adoxi3SFb4PIiuCfkzrXaQ7LUYyoZPNozwQzOedbcgQVAOcoV0mzBHzbd9st+mB0Ji4EdWnA2ILOcuGY6B4kcovoKBECOZPrn3OyDg2aNnyS6qC8QkwpC587MHX6/U068leA4HiVX4CJvX5DXR01+rSy5WWsj7bPb397m+s1rJQVdiOb+F1EcMerCBQAA
```

```
{'shapeSnapshots':[{'name':'Large Triangle 1','active':true,'selected':false,'position_x':-0.10151994228363037,'position_y':-0.12396955490112305,'rotation':44.999996185302734,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 2']},{'name':'Large Triangle 2','active':true,'selected':false,'position_x':-1.5157337188720703,'position_y':-1.5381829738616943,'rotation':315,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 1']},{'name':'Middle Triangle','active':true,'selected':false,'position_x':-0.8086267113685608,'position_y':-0.8310763835906982,'rotation':315,'locked':false,'linkedShapeNames':['Square','Large Triangle 1','Large Triangle 2']},{'name':'Small Triangle 1','active':true,'selected':false,'position_x':0.5517618656158447,'position_y':2.267948627471924,'rotation':67.5,'locked':false,'linkedShapeNames':[]},{'name':'Small Triangle 2','active':true,'selected':false,'position_x':-0.10151970386505127,'position_y':2.7044572830200195,'rotation':135,'locked':false,'linkedShapeNames':[]},{'name':'Rhombus','active':true,'selected':false,'position_x':-1.2167412042617798,'position_y':1.2109876871109009,'rotation':157.5,'locked':false,'linkedShapeNames':[]},{'name':'Square','active':true,'selected':false,'position_x':0.3984801173210144,'position_y':-0.6239694952964783,'rotation':90,'locked':false,'linkedShapeNames':['Middle Triangle','Large Triangle 1','Large Triangle 2']}],'align':0,'puzzleID':'000011'}
``` 


## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).

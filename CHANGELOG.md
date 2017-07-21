# eppz.Persistence

* 0.4.6

	+ Conversion between `String` and `byte[]`
		+ Added `String.Base64Bytes()`
		+ Added `byte[].StringFromBase64()`
	+ `String.Unzip()` fix
		+ Used Base64 accordingly

* 0.4.51

	+ Fixes

* 0.4.5

	+ Resource tests moved up to `Serializer` as well
	+ `String` extensions
		+ Added `String.WithExtension(serializer)`
		+ Added `String.Bytes()`
			+ And `byte[].String()` counterpart
	+ `Serializer` one time setup implemented at `Serializer.Setup()`
		+ Subclasses should call `base.Setup()`
	+ `JSONSerializer.mode`
		+ A public property instead of parameter
		+ Statements can be a bit more readable using `Pretty()` and `Default()`

* 0.4.3

	+ Common `Serializer` functionality tests got moved up to `Serializer`
		+ Same base tests can be reused with `BinarySerializer` as well

* 0.4.2

	+ More `JSONSerializer` tests
		+ Covered errors (non-existent files, JSON errors)
		+ Covered `Apply...` implementations
		+ `Entity.Equals()` fix for `null` cases

* 0.4.0

	+ Grouped file system helpers to `Files`
	+ Serialization
		+ Changes extension using `Path.ChangeExtension()`
		+ Removed `Stream` deserialization (considered internal)
		+ Added deserialization as resource from `Resources` (considered external counterpart of `Stream` serialization)
		+ See `DeserializeFile`, `DeserializeResource` and `DeserializeFileOrResource`
	+ `JSONSerializer`
		+ Implemented the above
		+ Covered with tests (except `DeserializeFileOrResource`)
			+ Using `OneTimeTearDown` instead `TearDown` fix

* 0.3.51
	
	+ Create temp folder if not yet exist

* 0.3.5

	+ Even more `JSONSerializer` tests
	+ `Assertation` helper to assert (text) file content equality
	+ `JSONSerializer.Mode` made explicit (instead `bool` property)

* 0.3.4

	+ More `JSONSerializer` tests

* 0.3.2

	+ `JSONSerializer` tests

* 0.3.0

	+ Added `UnityEngine.dll` to Travis `before-install` phase
	+ Added `$(PackagesFolder)` for conditional project reference locations
	+ Serializers extracted from projects
		+ Refactor to instance methods
		+ `Serializer` base class
		+ `BinarySerializer`
		+ `JSONSerializer`

* 0.2.1

	+ Created `Stream._CopyTo()` fallback
	+ Updated tests, fiddles

* 0.2.0

	+ Using `System.IO.Compression`
	+ Using `Stream.CopyTo()`
	+ String extensions `Zip()` and `Unzip()` works fine
	+ Gives the same results as other libraries (like `pako.js`)

* 0.0.1

	+ Initial commit
		+ `.gitignore`
		+ `NUnit`
		+ Travis
		+ Meta
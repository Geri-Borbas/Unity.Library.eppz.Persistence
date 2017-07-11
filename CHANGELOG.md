# eppz.Persistence

* Doing

	+ `BinarySerializer`
		+ Implement string serialization (Base64)
		+ Implement `Apply` functionality (?)
			+ If so, move up templates to `Serializer`
	+ `JSONSerializer`
		+ Tests

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
//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using NUnit.Framework;


namespace EPPZ.Persistence.Editor.Test
{


	using Entities;
	using Mode = EPPZ.Persistence.JSONSerializer.Mode;


	[TestFixture]
	public class JSONSerializer : EPPZ.Persistence.Editor.Test.Serializer
	{


		// Cast.
		EPPZ.Persistence.JSONSerializer jsonSerializer
		{ get { return serializer as EPPZ.Persistence.JSONSerializer; } }

		string first_JSON, second_JSON, third_JSON, fourth_JSON;


		[OneTimeSetUp]
		public void Setup()
		{
			serializer = new EPPZ.Persistence.JSONSerializer();

			_Setup_Folders();
			_Setup_Models();

			// JSON representation (escaped double quotes).
			first_JSON = "{\"ID\":1,\"name\":\"First\",\"payloads\":[{\"ID\":1,\"name\":\"Alpha\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			second_JSON = "{\"ID\":2,\"name\":\"Second\",\"payloads\":[{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			third_JSON = "{\"ID\":3,\"name\":\"Third\",\"payloads\":[{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			fourth_JSON = "{\"ID\":4,\"name\":\"Fourth\",\"payloads\":[]}";

			first_string = "{\"ID\":1,\"name\":\"First\",\"payloads\":[{\"ID\":1,\"name\":\"Alpha\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			second_string = "{\"ID\":2,\"name\":\"Second\",\"payloads\":[{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			third_string = "{\"ID\":3,\"name\":\"Third\",\"payloads\":[{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			fourth_string = "{\"ID\":4,\"name\":\"Fourth\",\"payloads\":[]}";

			// _RecreateTestFiles();
			_Setup_Resources();
		}

		void _RecreateTestFiles()
		{
			jsonSerializer.SerializeObjectToFile(first, testFolderPath + "first", Mode.Pretty);
			jsonSerializer.SerializeObjectToFile(second, testFolderPath + "second", Mode.Pretty);
			jsonSerializer.SerializeObjectToFile(third, testFolderPath + "third", Mode.Pretty);
			jsonSerializer.SerializeObjectToFile(fourth, testFolderPath + "fourth", Mode.Pretty);	
		}

		[OneTimeTearDown]
		public void TearDown()
		{ _TearDown_Resources(); }


	#region String

		[Test]
		public void ApplyStringTo()
		{
			// Modify partial data.
			jsonSerializer.ApplyStringTo("{\"payloads\":[]}", third);
			jsonSerializer.ApplyStringTo("{\"ID\":3,\"name\":\"Third\"}", fourth);

			Assert.AreEqual(
				third,
				fourth
			);

			// Revert changes using `ApplyStringTo()`.
			jsonSerializer.ApplyStringTo(third_string, third);
			jsonSerializer.ApplyStringTo(fourth_string, fourth);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(third_string),
				third
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(fourth_string),
				fourth
			);

			// Error.
			jsonSerializer.ApplyStringTo("<ERROR>", fourth);
			Assert.AreEqual(
				serializer.StringToObject<Entity>(fourth_string),
				fourth
			);
		}

	#endregion


	#region File

		[Test]
		public void ApplyFileTo()
		{
			Entity empty = new Entity();

			jsonSerializer.ApplyFileTo(testFolderPath+"first", empty);
			Assert.AreEqual(
				empty,
				first
			);

			jsonSerializer.ApplyFileTo(testFolderPath+"second", empty);
			Assert.AreEqual(
				empty,
				second
			);

			jsonSerializer.ApplyFileTo(testFolderPath+"third", empty);
			Assert.AreEqual(
				empty,
				third
			);

			jsonSerializer.ApplyFileTo(testFolderPath+"fourth", empty);
			Assert.AreEqual(
				empty,
				fourth
			);

			// Error.
			jsonSerializer.ApplyFileTo("<ERROR>", empty);
			Assert.AreEqual(
				empty,
				fourth
			);
		}

		[Test]
		public void ObjectToFile_Pretty()
		{
			jsonSerializer.ObjectToFile(first, tempFolderPath+"first_test_pretty", Mode.Pretty);
			Files.AreEqual(
				testFolderPath+"first.json",
				tempFolderPath+"first_test_pretty.json"
			);

			jsonSerializer.ObjectToFile(second, tempFolderPath+"second_test_pretty", Mode.Pretty);
			Files.AreEqual(
				testFolderPath+"second.json",
				tempFolderPath+"second_test_pretty.json"
			);

			jsonSerializer.ObjectToFile(third, tempFolderPath+"third_test_pretty", Mode.Pretty);
			Files.AreEqual(
				testFolderPath+"third.json",
				tempFolderPath+"third_test_pretty.json"
			);

			jsonSerializer.ObjectToFile(fourth, tempFolderPath+"fourth_test_pretty", Mode.Pretty);
			Files.AreEqual(
				testFolderPath+"fourth.json",
				tempFolderPath+"fourth_test_pretty.json"
			);
		}

	#endregion
	

	#region Resource

		void _Setup_Resources()
		{
			Files.Copy(
				testFolderPath + "first.json",
				resourcesFolderPath + "first.json"
			);

			Files.Copy(
				testFolderPath + "second.json",
				resourcesFolderPath + "second.json"
			);

			Files.Copy(
				testFolderPath + "third.json",
				resourcesFolderPath + "third.json"
			);

			Files.Copy(
				testFolderPath + "fourth.json",
				resourcesFolderPath + "fourth.json"
			);

			AssetDatabase.ImportAsset("Assets/Resources/first.json");
			AssetDatabase.ImportAsset("Assets/Resources/second.json");
			AssetDatabase.ImportAsset("Assets/Resources/third.json");
			AssetDatabase.ImportAsset("Assets/Resources/fourth.json");

		}

		void _TearDown_Resources()
		{
			Files.DeleteAsset("Assets/Resources/first.json");
			Files.DeleteAsset("Assets/Resources/second.json");
			Files.DeleteAsset("Assets/Resources/third.json");
			Files.DeleteAsset("Assets/Resources/fourth.json");

			Files.DeleteAsset("Assets/Resources/first_test_pretty.json");
			Files.DeleteAsset("Assets/Resources/second_test_pretty.json");
			Files.DeleteAsset("Assets/Resources/third_test_pretty.json");
			Files.DeleteAsset("Assets/Resources/fourth_test_pretty.json");

			Files.DeleteAsset("Assets/Resources/first_test.json");
			Files.DeleteAsset("Assets/Resources/second_test.json");
			Files.DeleteAsset("Assets/Resources/third_test.json");
			Files.DeleteAsset("Assets/Resources/fourth_test.json");

			Files.DeleteFolderIfEmpty(resourcesFolderPath);
		}

		[Test]
		public void ApplyResourceTo()
		{
			Entity empty = new Entity();

			jsonSerializer.ApplyResourceTo("first", empty);
			Assert.AreEqual(
				empty,
				first
			);

			jsonSerializer.ApplyResourceTo("second", empty);
			Assert.AreEqual(
				empty,
				second
			);

			jsonSerializer.ApplyResourceTo("third", empty);
			Assert.AreEqual(
				empty,
				third
			);

			jsonSerializer.ApplyResourceTo("fourth", empty);
			Assert.AreEqual(
				empty,
				fourth
			);

			// Error.
			jsonSerializer.ApplyResourceTo("<ERROR>", empty);
			Assert.AreEqual(
				empty,
				fourth
			);
		}

		[Test]
		public void ObjectToResource()
		{			
			// Write to `Assets/Resources`, import, then read back as resource (using `UnityEngine.Resources`).
			serializer.ObjectToFile(first, resourcesFolderPath + "first_test");
			AssetDatabase.ImportAsset("Assets/Resources/first_test.json");
			Assert.AreEqual(
				first_string,
				(Resources.Load("first_test") as TextAsset).text
			);

			serializer.ObjectToFile(second, resourcesFolderPath + "second_test");
			AssetDatabase.ImportAsset("Assets/Resources/second_test.json");
			Assert.AreEqual(
				second_string,
				(Resources.Load("second_test") as TextAsset).text
			);

			serializer.ObjectToFile(third, resourcesFolderPath + "third_test");
			AssetDatabase.ImportAsset("Assets/Resources/third_test.json");
			Assert.AreEqual(
				third_string,
				(Resources.Load("third_test") as TextAsset).text
			);

			serializer.ObjectToFile(fourth, resourcesFolderPath + "fourth_test");
			AssetDatabase.ImportAsset("Assets/Resources/fourth_test.json");
			Assert.AreEqual(
				fourth_string,
				(Resources.Load("fourth_test") as TextAsset).text
			);
		}

	#endregion


	#region File or Resource

		[Test]
		public void FileOrResourceToObject()
		{
			// Load file if available.
			Assert.AreEqual(
				serializer.FileOrResourceToObject<Entity>(
					testFolderPath+"first",
					"<ERROR>"
				),
				first
			);

			// Load resource if no file available.
			Assert.AreEqual(
				serializer.FileOrResourceToObject<Entity>(
					"<ERROR>",
					"first"
				),
				first
			);

			// Error.
			Assert.IsNull(
				serializer.FileOrResourceToObject<Entity>(
					"<ERROR>",
					"<ERROR>"
				)
			);
		}

	#endregion


	}
}
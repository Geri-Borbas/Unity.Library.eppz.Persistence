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


		[OneTimeSetUp]
		public override void Setup()
		{
			base.Setup();

			// Instance.
			serializer = new EPPZ.Persistence.JSONSerializer();

			// JSON representation strings (escaped double quotes).
			first_string = "{\"ID\":1,\"name\":\"First\",\"payloads\":[{\"ID\":1,\"name\":\"Alpha\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			second_string = "{\"ID\":2,\"name\":\"Second\",\"payloads\":[{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			third_string = "{\"ID\":3,\"name\":\"Third\",\"payloads\":[{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			fourth_string = "{\"ID\":4,\"name\":\"Fourth\",\"payloads\":[]}";

			// Resources.
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

		void _RecreateTestFiles()
		{
			jsonSerializer.Pretty().ObjectToFile(first, testFolderPath + "first");
			jsonSerializer.Pretty().ObjectToFile(second, testFolderPath + "second");
			jsonSerializer.Pretty().ObjectToFile(third, testFolderPath + "third");
			jsonSerializer.Pretty().ObjectToFile(fourth, testFolderPath + "fourth");	
			jsonSerializer.Default();
		}

		[OneTimeTearDown]
		public void TearDown()
		{ 
			// Resources.
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
			jsonSerializer.Pretty().ObjectToFile(first, tempFolderPath+"first_test_pretty");
			Files.AreEqual(
				testFolderPath+"first.json",
				tempFolderPath+"first_test_pretty.json"
			);

			jsonSerializer.Pretty().ObjectToFile(second, tempFolderPath+"second_test_pretty");
			Files.AreEqual(
				testFolderPath+"second.json",
				tempFolderPath+"second_test_pretty.json"
			);

			jsonSerializer.Pretty().ObjectToFile(third, tempFolderPath+"third_test_pretty");
			Files.AreEqual(
				testFolderPath+"third.json",
				tempFolderPath+"third_test_pretty.json"
			);

			jsonSerializer.Pretty().ObjectToFile(fourth, tempFolderPath+"fourth_test_pretty");
			Files.AreEqual(
				testFolderPath+"fourth.json",
				tempFolderPath+"fourth_test_pretty.json"
			);

			jsonSerializer.Default();
		}

	#endregion
	

	#region Resource

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

	#endregion


	}
}
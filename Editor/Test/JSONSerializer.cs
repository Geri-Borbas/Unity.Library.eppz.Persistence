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
	using Extensions;


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

			// Folders.
			resourcePath = "JSON/";
			resourcesFolderPath = Application.dataPath + "/Resources/JSON/";
			testFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/JSON/";
			tempFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/JSON/.temp/";
			Files.CreateFolderIfNotExist(resourcesFolderPath);			
			Files.CreateFolderIfNotExist(tempFolderPath);

			// Instance.
			serializer = new EPPZ.Persistence.JSONSerializer();

			// JSON representation strings (escaped double quotes).
			first_string = "{\"ID\":1,\"name\":\"First\",\"payloads\":[{\"ID\":1,\"name\":\"Alpha\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			second_string = "{\"ID\":2,\"name\":\"Second\",\"payloads\":[{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			third_string = "{\"ID\":3,\"name\":\"Third\",\"payloads\":[{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			fourth_string = "{\"ID\":4,\"name\":\"Fourth\",\"payloads\":[]}";

			first_string_zip = "H4sIAAAAAAAAA6tW8nRRsjLUUcpLzE1VslJyyywqLlHSUSpIrMzJT0wpVrKKrkZT4phTkJEIVJKSWJIIlDbQMdQx0jHWMdEx1THTMdex0LGMrdWBaDKCa3JKLSFSjzFcj3tibi4hTbG1AB1cQuHCAAAA";
			second_string_zip = "H4sIAAAAAAAAA6tW8nRRsjLSUcpLzE1VslIKTk3Oz0tR0lEqSKzMyU9MKVayiq5GU+OUWpIIVJGSCKSsog10DHWMdIx1THRMdcx0zHUsdCxja3UgeozhetwTc3MJaYqtBQA++rdZjgAAAA==";
			third_string_zip = "H4sIAAAAAAAAA6tW8nRRsjLWUcpLzE1VslIKycgsSlHSUSpIrMzJT0wpVrKKrkZT4p6Ym5sIVJKSWJIIlDbQMdQx0jHWMdEx1THTMdex0LGMrY2tBQBoNFOrWQAAAA==";
			fourth_string_zip = "H4sIAAAAAAAAA6tW8nRRsjLRUcpLzE1VslJyyy8tKslQ0lEqSKzMyU9MKVayio6tBQB5M3WNJgAAAA==";			

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
				testFolderPath + "third.txt",
				resourcesFolderPath + "third.txt"
			);

			Files.Copy(
				testFolderPath + "fourth.txt",
				resourcesFolderPath + "fourth.txt"
			);

			AssetDatabase.ImportAsset("Assets/Resources/JSON/first.json");
			AssetDatabase.ImportAsset("Assets/Resources/JSON/second.json");
			AssetDatabase.ImportAsset("Assets/Resources/JSON/third.txt");
			AssetDatabase.ImportAsset("Assets/Resources/JSON/fourth.txt");
		}

		void _RecreateTestFiles()
		{
			serializer.ObjectToFile(first, testFolderPath + "first");
			serializer.ObjectToFile(second, testFolderPath + "second");
			serializer.ObjectToFile(third, testFolderPath + "third");
			serializer.ObjectToFile(fourth, testFolderPath + "fourth");		

			jsonSerializer.Pretty().ObjectToFile(first, testFolderPath + "first_pretty");
			jsonSerializer.Pretty().ObjectToFile(second, testFolderPath + "second_pretty");
			jsonSerializer.Pretty().ObjectToFile(third, testFolderPath + "third_pretty");
			jsonSerializer.Pretty().ObjectToFile(fourth, testFolderPath + "fourth_pretty");
			jsonSerializer.Default();

			System.IO.File.WriteAllText(testFolderPath + "first_string.txt", serializer.ObjectToString(first));
			System.IO.File.WriteAllText(testFolderPath + "second_string.txt", serializer.ObjectToString(second));
			System.IO.File.WriteAllText(testFolderPath + "third_string.txt", serializer.ObjectToString(third));
			System.IO.File.WriteAllText(testFolderPath + "fourth_string.txt", serializer.ObjectToString(fourth));

			System.IO.File.WriteAllText(testFolderPath + "first_string_zip.txt", serializer.ObjectToString(first).Zip());
			System.IO.File.WriteAllText(testFolderPath + "second_string_zip.txt", serializer.ObjectToString(second).Zip());
			System.IO.File.WriteAllText(testFolderPath + "third_string_zip.txt", serializer.ObjectToString(third).Zip());
			System.IO.File.WriteAllText(testFolderPath + "fourth_string_zip.txt", serializer.ObjectToString(fourth).Zip());
		}

		[OneTimeTearDown]
		public void TearDown()
		{ 			
			// Resources.
			Files.DeleteAsset("Assets/Resources/JSON/first.json");
			Files.DeleteAsset("Assets/Resources/JSON/second.json");
			Files.DeleteAsset("Assets/Resources/JSON/third.txt");
			Files.DeleteAsset("Assets/Resources/JSON/fourth.txt");

			Files.DeleteAsset("Assets/Resources/JSON/first_test.json");
			Files.DeleteAsset("Assets/Resources/JSON/second_test.json");
			Files.DeleteAsset("Assets/Resources/JSON/third_test.txt");
			Files.DeleteAsset("Assets/Resources/JSON/fourth_test.txt");

			Files.DeleteAsset("Assets/Resources/JSON/first_test_pretty.json");
			Files.DeleteAsset("Assets/Resources/JSON/second_test_pretty.json");
			Files.DeleteAsset("Assets/Resources/JSON/third_test_pretty.txt");
			Files.DeleteAsset("Assets/Resources/JSON/fourth_test_pretty.txt");

			Files.DeleteFolderIfEmpty(resourcesFolderPath);
			Files.DeleteFolderAnyway(tempFolderPath);
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
		public void FileToObject_Without_ManageFileExtensions()
		{
			// Opt-out file extension management.
			serializer.TurnOffFileExtensionManagement();

			Assert.IsNull(
				serializer.FileToObject<Entity>(testFolderPath + "first")
			);

			Assert.IsNull(
				serializer.FileToObject<Entity>(testFolderPath + "second")
			);

			Assert.IsNull(
				serializer.FileToObject<Entity>(testFolderPath + "third")
			);

			Assert.IsNull(
				serializer.FileToObject<Entity>(testFolderPath + "fourth")
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath + "first.json"),
				first
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath + "second.json"),
				second
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath + "third.txt"),
				third
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath + "fourth.txt"),
				fourth
			);

			// Opt-in file extension management (for the rest of the tests).
			serializer.TurnOnFileExtensionManagement();
		}	

		[Test]
		public void ApplyFileTo()
		{
			Entity empty = new Entity();

			jsonSerializer.ApplyFileTo(testFolderPath + "first", empty);
			Assert.AreEqual(
				empty,
				first
			);

			jsonSerializer.ApplyFileTo(testFolderPath + "second", empty);
			Assert.AreEqual(
				empty,
				second
			);

			jsonSerializer.ApplyFileTo(testFolderPath + "third", empty);
			Assert.AreEqual(
				empty,
				third
			);

			jsonSerializer.ApplyFileTo(testFolderPath + "fourth", empty);
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
			jsonSerializer.Pretty().ObjectToFile(first, tempFolderPath + "first_test_pretty");
			Files.AreEqual(
				testFolderPath + "first_pretty.json",
				tempFolderPath + "first_test_pretty.json"
			);

			jsonSerializer.Pretty().ObjectToFile(second, tempFolderPath + "second_test_pretty");
			Files.AreEqual(
				testFolderPath + "second_pretty.json",
				tempFolderPath + "second_test_pretty.json"
			);

			jsonSerializer.Pretty().ObjectToFile(third, tempFolderPath + "third_test_pretty");
			Files.AreEqual(
				testFolderPath + "third_pretty.txt",
				tempFolderPath + "third_test_pretty.json"
			);

			jsonSerializer.Pretty().ObjectToFile(fourth, tempFolderPath + "fourth_test_pretty");
			Files.AreEqual(
				testFolderPath + "fourth_pretty.txt",
				tempFolderPath + "fourth_test_pretty.json"
			);

			jsonSerializer.Default();
		}

		[Test]
		public void ObjectToFile_Without_ManageFileExtensions()
		{
			// Opt-out file extension management.
			serializer.TurnOffFileExtensionManagement();

			// No extension added silently.
			serializer.ObjectToFile(first, tempFolderPath+"first_test_extension");
			FileAssert.DoesNotExist(tempFolderPath + "first_test_extension.json");
			FileAssert.Exists(tempFolderPath + "first_test_extension");

			// Any extension can be used.
			serializer.ObjectToFile(first, tempFolderPath+"first_test_extension.txt");
			FileAssert.DoesNotExist(tempFolderPath + "first_test_extension.json");
			FileAssert.Exists(tempFolderPath + "first_test_extension.txt");

			// Opt-in file extension management (for the rest of the tests).
			serializer.TurnOnFileExtensionManagement();
		}

	#endregion
	

	#region Resource

		[Test]
		public void ApplyResourceTo()
		{
			Entity empty = new Entity();

			jsonSerializer.ApplyResourceTo(resourcePath + "first", empty);
			Assert.AreEqual(
				empty,
				first
			);

			jsonSerializer.ApplyResourceTo(resourcePath + "second", empty);
			Assert.AreEqual(
				empty,
				second
			);

			jsonSerializer.ApplyResourceTo(resourcePath + "third", empty);
			Assert.AreEqual(
				empty,
				third
			);

			jsonSerializer.ApplyResourceTo(resourcePath + "fourth", empty);
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
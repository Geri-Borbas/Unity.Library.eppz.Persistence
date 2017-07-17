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
	public class JSONSerializer
	{


		EPPZ.Persistence.JSONSerializer serializer;
		string testFolderPath;
		string tempFolderPath;

		Payload alpha, beta, gamma;
		Entity first, second, third, fourth;
		string first_JSON, second_JSON, third_JSON, fourth_JSON;


		[SetUp]
		public void Setup()
		{
			serializer = new EPPZ.Persistence.JSONSerializer();
			testFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/";
			tempFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/.temp/";

			// Object graph.
			alpha = new Payload(
				1,
				"Alpha",
				0,1,2,3,4,5,6,7,8,9
			);
			beta = new Payload(
				2,
				"Beta",
				0,1,2,3,4,5,6,7,8,9
			);
			gamma = new Payload(
				3,
				"Gamma",
				0,1,2,3,4,5,6,7,8,9
			);

			first = new Entity(
				1,
				"First",
				alpha,
				beta,
				gamma
			);

			second = new Entity(
				2,
				"Second",
				beta,
				gamma
			);

			third = new Entity(
				3,
				"Third",
				gamma
			);

			fourth = new Entity(
				4,
				"Fourth"
			);

					

			// JSON representation (escaped double quotes).
			first_JSON = "{\"ID\":1,\"name\":\"First\",\"payloads\":[{\"ID\":1,\"name\":\"Alpha\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			second_JSON = "{\"ID\":2,\"name\":\"Second\",\"payloads\":[{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			third_JSON = "{\"ID\":3,\"name\":\"Third\",\"payloads\":[{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			fourth_JSON = "{\"ID\":4,\"name\":\"Fourth\",\"payloads\":[]}";
		}

		void _RecreateTestFiles()
		{
			serializer.SerializeObjectToFile(first, testFolderPath + "first", Mode.Pretty);
			serializer.SerializeObjectToFile(second, testFolderPath + "second", Mode.Pretty);
			serializer.SerializeObjectToFile(third, testFolderPath + "third", Mode.Pretty);
			serializer.SerializeObjectToFile(fourth, testFolderPath + "fourth", Mode.Pretty);	
		}

		[TearDown]
		public void TeaerDown()
		{ }


	#region Model

		[Test]
		public void PayloadEquals()
		{
			Assert.AreEqual(
				new Payload(1, "Alpha", 0,1,2,3,4,5,6,7,8,9),
				new Payload(1, "Alpha", 0,1,2,3,4,5,6,7,8,9)
			);

			Assert.AreEqual(
				new Payload(1, "Alpha", 0,1,2,3,4,5,6,7,8,9),
				alpha
			);

			Assert.AreEqual(
				alpha,
				alpha
			);
		}

		[Test]
		public void EntityEquals()
		{
			Assert.AreEqual(
				new Entity(3, "Third", gamma),
				new Entity(3, "Third", gamma)
			);

			Assert.AreEqual(
				new Entity(3, "Third", new Payload(3, "Gamma", 0,1,2,3,4,5,6,7,8,9)),
				new Entity(3, "Third", gamma)
			);

			Assert.AreEqual(
				new Entity(3, "Third", gamma),
				third
			);

			Assert.AreEqual(
				new Entity(3, "Third", new Payload(3, "Gamma", 0,1,2,3,4,5,6,7,8,9)),
				third
			);

			Assert.AreEqual(
				third,
				third
			);
		}

	#endregion


	#region String

		[Test]
		public void StringToObject()
		{
			Assert.AreEqual(
				serializer.StringToObject<Entity>(first_JSON),
				first
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(second_JSON),
				second
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(third_JSON),
				third
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(fourth_JSON),
				fourth
			);
		}

		[Test]
		public void ObjectToString()
		{
			Assert.AreEqual(
				serializer.ObjectToString(first),
				first_JSON
			);

			Assert.AreEqual(
				serializer.ObjectToString(second),
				second_JSON
			);
			Assert.AreEqual(
				serializer.ObjectToString(third),
				third_JSON
			);

			Assert.AreEqual(
				serializer.ObjectToString(fourth),
				fourth_JSON
			);
		}

	#endregion


	#region File

		[Test]
		public void FileToObject()
		{
			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"first"),
				first
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"second"),
				second
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"third"),
				third
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"fourth"),
				fourth
			);
		}

		[Test]
		public void ObjectToFile_Pretty()
		{
			serializer.ObjectToFile(first, tempFolderPath+"first_test_pretty", Mode.Pretty);
			Assertation.AreFilesEqual(
				testFolderPath+"first.json",
				tempFolderPath+"first_test_pretty.json"
			);

			serializer.ObjectToFile(second, tempFolderPath+"second_test_pretty", Mode.Pretty);
			Assertation.AreFilesEqual(
				testFolderPath+"second.json",
				tempFolderPath+"second_test_pretty.json"
			);

			serializer.ObjectToFile(third, tempFolderPath+"third_test_pretty", Mode.Pretty);
			Assertation.AreFilesEqual(
				testFolderPath+"third.json",
				tempFolderPath+"third_test_pretty.json"
			);

			serializer.ObjectToFile(fourth, tempFolderPath+"fourth_test_pretty", Mode.Pretty);
			Assertation.AreFilesEqual(
				testFolderPath+"fourth.json",
				tempFolderPath+"fourth_test_pretty.json"
			);
		}

		[Test]
		public void ObjectToFile()
		{
			serializer.ObjectToFile(first, tempFolderPath+"first_test");
			Assert.AreEqual(
				first_JSON,
				File.ReadAllText(tempFolderPath+"first_test.json")
			);

			serializer.ObjectToFile(second, tempFolderPath+"second_test");
			Assert.AreEqual(
				second_JSON,
				File.ReadAllText(tempFolderPath+"second_test.json")
			);

			serializer.ObjectToFile(third, tempFolderPath+"third_test");
			Assert.AreEqual(
				third_JSON,
				File.ReadAllText(tempFolderPath+"third_test.json")
			);

			serializer.ObjectToFile(fourth, tempFolderPath+"fourth_test");
			Assert.AreEqual(
				fourth_JSON,
				File.ReadAllText(tempFolderPath+"fourth_test.json")
			);
		}

	#endregion
	

	}
}
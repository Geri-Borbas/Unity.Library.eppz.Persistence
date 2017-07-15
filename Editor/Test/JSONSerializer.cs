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
using NUnit.Framework;


namespace EPPZ.Persistence.Editor.Test
{


	using Entities;


	[TestFixture]
	public class JSONSerializer
	{


		EPPZ.Persistence.JSONSerializer serializer;
		string testFolderPath;

		Payload alpha, beta, gamma;
		Entity first, second, third, fourth;
		string first_JSON, second_JSON, third_JSON, fourth_JSON;


		[SetUp]
		public void Setup()
		{
			serializer = new EPPZ.Persistence.JSONSerializer();
			testFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/";

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

			serializer.SerializeObjectToFile(first, testFolderPath + "first", true);
			serializer.SerializeObjectToFile(second, testFolderPath + "second", true);
			serializer.SerializeObjectToFile(third, testFolderPath + "third", true);
			serializer.SerializeObjectToFile(fourth, testFolderPath + "fourth", true);			

			// JSON representation (escaped double quotes).
			first_JSON = "{\"ID\":1,\"name\":\"First\",\"payloads\":[{\"ID\":1,\"name\":\"Alpha\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			second_JSON = "{\"ID\":2,\"name\":\"Second\",\"payloads\":[{\"ID\":2,\"name\":\"Beta\",\"data\":[0,1,2,3,4,5,6,7,8,9]},{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			third_JSON = "{\"ID\":3,\"name\":\"Third\",\"payloads\":[{\"ID\":3,\"name\":\"Gamma\",\"data\":[0,1,2,3,4,5,6,7,8,9]}]}";
			fourth_JSON = "{\"ID\":4,\"name\":\"Fourth\",\"payloads\":[]}";
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
				serializer.FileToObject<Entity>(testFolderPath+"first.json"),
				first
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"second.json"),
				second
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"third.json"),
				third
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"fourth.json"),
				fourth
			);
		}

	#endregion
	

	}
}
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


	[TestFixture]
	public class JSONSerializer
	{


		EPPZ.Persistence.JSONSerializer serializer;
		string path; 

		Entity first;
		string first_JSON;
		Entity second;
		string second_JSON;


		[SetUp]
		public void Setup()
		{
			serializer = new EPPZ.Persistence.JSONSerializer();
			path = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/";

			first = new Entity(
				1,
				"First",
				2,3,4
			);
			second = new Entity(
				2,
				"Second",
				1,3,4
			);

			first_JSON = "{\"ID\":1,\"name\":\"First\",\"entities\":[2,3,4]}";
			second_JSON = "{\"ID\":2,\"name\":\"Second\",\"entities\":[1,3,4]}";			
			
		}

		[TearDown]
		public void TeaerDown()
		{ }

		[Test]
		public void Test_StringToObject()
		{
			Assert.AreEqual(
				serializer.StringToObject<Entity>(first_JSON),
				first
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(second_JSON),
				second
			);

			Assert.AreNotEqual(
				serializer.StringToObject<Entity>(first_JSON),
				serializer.StringToObject<Entity>(second_JSON)
			);
		}

		[Test]
		public void Test_ObjectToString()
		{
			Assert.AreEqual(
				serializer.ObjectToString(first),
				first_JSON
			);

			Assert.AreEqual(
				serializer.ObjectToString(second),
				second_JSON
			);

			Assert.AreNotEqual(
				serializer.ObjectToString(first),
				serializer.ObjectToString(second)
			);
		}

		[Test]
		public void Test_FileToObject()
		{
			Assert.AreEqual(
				serializer.FileToObject<Entity>(path+"second.json"),
				second
			);
		}
	}
}
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


	[Serializable]
	public class Foo
	{
		public string bar;
	}


	[TestFixture]
	public class JSONSerializer
	{


		EPPZ.Persistence.JSONSerializer serializer;
		Foo foo;
		string JSON;


		[SetUp]
		public void Setup()
		{
			serializer = new EPPZ.Persistence.JSONSerializer();
			foo = new Foo();
			foo.bar = "data";
			JSON = "{\"bar\":\"data\"}";
		}

		[TearDown]
		public void TeaerDown()
		{ }

		[Test]
		public void Test_StringToObject()
		{
			Assert.AreEqual(
				serializer.StringToObject<Foo>(JSON).bar,
				foo.bar
			);
		}

		[Test]
		public void Test_ObjectToString()
		{
			Assert.AreEqual(
				serializer.ObjectToString(foo),
				JSON
			);
		}
	}
}
//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#if UNITY_5
using UnityEngine;
using UnityEditor;
#endif
using System;
using NUnit.Framework;


namespace EPPZ.Persistence.Editor.Test
{


	[TestFixture]
	public class String
	{


		[SetUp]
		public void Setup()
		{ }

		[TearDown]
		public void TeaerDown()
		{ }

		[Test]
		public void Test_Zip()
		{
			Assert.AreEqual(
				"H4sIAAAAAAAAAwvOz01VKEktLlEoLinKzEtXKMlXSM7PLShKLS7WAwAr6BTWHQAAAA==",
				"Some test string to compress.".Zip()
			);
			
			Assert.AreEqual(
				"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=",
				"Batman".Zip()
			);
		}

		[Test]
		public void Test_Unzip()
		{			
			// See pako.js counterpart at http://jsfiddle.net/9yH7M/847/
			Assert.AreEqual(
				"Some test string to compress.",
				"H4sIAAAAAAAAAwvOz01VKEktLlEoLinKzEtXKMlXSM7PLShKLS7WAwAr6BTWHQAAAA==".Unzip()
			);		

			// See pako.js counterpart at http://jsfiddle.net/9yH7M/845/
			Assert.AreEqual(
				"Batman",
				"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=".Unzip()
			);
		}
	}
}
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


	[TestFixture]
	public class BinarySerializer : EPPZ.Persistence.Editor.Test.Serializer
	{


		// Cast.
		EPPZ.Persistence.BinarySerializer binarySerializer
		{ get { return serializer as EPPZ.Persistence.BinarySerializer; } }


		[OneTimeSetUp]
		public override void Setup()
		{
			base.Setup();

			// Instance.
			serializer = new EPPZ.Persistence.BinarySerializer();

			// _RecreateTestFiles();

			// JSON representation strings (escaped double quotes).
			first_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAEAAAAGAwAAAAVGaXJzdAkEAAAABAQAAACrAVN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkLCBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcywgVmVyc2lvbj0wLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBAAAL0VQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuUGF5bG9hZFtdAgAAAAgICQUAAAADAAAAAAAAAAcFAAAAAAEAAAADAAAABC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQCAAAACQYAAAAJBwAAAAkIAAAABQYAAAAtRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkAwAAAAJJRARuYW1lBGRhdGEAAQcICAIAAAABAAAABgkAAAAFQWxwaGEJCgAAAAEHAAAABgAAAAIAAAAGCwAAAARCZXRhCQwAAAABCAAAAAYAAAADAAAABg0AAAAFR2FtbWEJDgAAAA8KAAAACgAAAAgAAAAAAQAAAAIAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAJAAAADwwAAAAKAAAACAAAAAABAAAAAgAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAkAAAAPDgAAAAoAAAAIAAAAAAEAAAACAAAAAwAAAAQAAAAFAAAABgAAAAcAAAAIAAAACQAAAAs=";
			second_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAIAAAAGAwAAAAZTZWNvbmQJBAAAAAQEAAAAqwFTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0VQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuUGF5bG9hZCwgQXNzZW1ibHktQ1NoYXJwLUVkaXRvci1maXJzdHBhc3MsIFZlcnNpb249MC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsXV0DAAAABl9pdGVtcwVfc2l6ZQhfdmVyc2lvbgQAAC9FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWRbXQIAAAAICAkFAAAAAgAAAAAAAAAHBQAAAAABAAAAAgAAAAQtRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkAgAAAAkGAAAACQcAAAAFBgAAAC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQDAAAAAklEBG5hbWUEZGF0YQABBwgIAgAAAAIAAAAGCAAAAARCZXRhCQkAAAABBwAAAAYAAAADAAAABgoAAAAFR2FtbWEJCwAAAA8JAAAACgAAAAgAAAAAAQAAAAIAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAJAAAADwsAAAAKAAAACAAAAAABAAAAAgAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAkAAAAL";
			third_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAMAAAAGAwAAAAVUaGlyZAkEAAAABAQAAACrAVN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkLCBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcywgVmVyc2lvbj0wLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBAAAL0VQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuUGF5bG9hZFtdAgAAAAgICQUAAAABAAAAAAAAAAcFAAAAAAEAAAABAAAABC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQCAAAACQYAAAAFBgAAAC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQDAAAAAklEBG5hbWUEZGF0YQABBwgIAgAAAAMAAAAGBwAAAAVHYW1tYQkIAAAADwgAAAAKAAAACAAAAAABAAAAAgAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAkAAAAL";
			fourth_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAQAAAAGAwAAAAZGb3VydGgKCw==";

			// Resources.
			Files.Copy(
				testFolderPath + "first.bytes",
				resourcesFolderPath + "first.bytes"
			);

			Files.Copy(
				testFolderPath + "second.bytes",
				resourcesFolderPath + "second.bytes"
			);

			Files.Copy(
				testFolderPath + "third.bytes",
				resourcesFolderPath + "third.bytes"
			);

			Files.Copy(
				testFolderPath + "fourth.bytes",
				resourcesFolderPath + "fourth.bytes"
			);

			AssetDatabase.ImportAsset("Assets/Resources/first.bytes");
			AssetDatabase.ImportAsset("Assets/Resources/second.bytes");
			AssetDatabase.ImportAsset("Assets/Resources/third.bytes");
			AssetDatabase.ImportAsset("Assets/Resources/fourth.bytes");
		}

		void _RecreateTestFiles()
		{
			serializer.ObjectToFile(first, testFolderPath + "first");
			serializer.ObjectToFile(second, testFolderPath + "second");
			serializer.ObjectToFile(third, testFolderPath + "third");
			serializer.ObjectToFile(fourth, testFolderPath + "fourth");

			System.IO.File.WriteAllText(testFolderPath + "first.txt", serializer.ObjectToString(first));
			System.IO.File.WriteAllText(testFolderPath + "second.txt", serializer.ObjectToString(second));
			System.IO.File.WriteAllText(testFolderPath + "third.txt", serializer.ObjectToString(third));
			System.IO.File.WriteAllText(testFolderPath + "fourth.txt", serializer.ObjectToString(fourth));
		}

		[OneTimeTearDown]
		public void TearDown()
		{ 
			// Resources.
			Files.DeleteAsset("Assets/Resources/first.bytes");
			Files.DeleteAsset("Assets/Resources/second.bytes");
			Files.DeleteAsset("Assets/Resources/third.bytes");
			Files.DeleteAsset("Assets/Resources/fourth.bytes");

			Files.DeleteAsset("Assets/Resources/first_test.bytes");
			Files.DeleteAsset("Assets/Resources/second_test.bytes");
			Files.DeleteAsset("Assets/Resources/third_test.bytes");
			Files.DeleteAsset("Assets/Resources/fourth_test.bytes");

			Files.DeleteFolderIfEmpty(resourcesFolderPath);
		}


	#region String

	#endregion


	#region File

	#endregion
	

	#region Resource

	#endregion


	}
}
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
	public class BinarySerializer : EPPZ.Persistence.Editor.Test.Serializer
	{


		// Cast.
		EPPZ.Persistence.BinarySerializer binarySerializer
		{ get { return serializer as EPPZ.Persistence.BinarySerializer; } }


		[OneTimeSetUp]
		public override void Setup()
		{
			base.Setup();

			// Folders.
			resourcePath = "Binary/";
			resourcesFolderPath = Application.dataPath + "/Resources/Binary/";
			testFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/Binary/";
			tempFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/Binary/.temp/";
			Files.CreateFolderIfNotExist(resourcesFolderPath);	
			Files.CreateFolderIfNotExist(tempFolderPath);

			// Instance.
			serializer = new EPPZ.Persistence.BinarySerializer();

			// JSON representation strings (escaped double quotes).
			first_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAEAAAAGAwAAAAVGaXJzdAkEAAAABAQAAACrAVN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkLCBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcywgVmVyc2lvbj0wLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBAAAL0VQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuUGF5bG9hZFtdAgAAAAgICQUAAAADAAAAAAAAAAcFAAAAAAEAAAADAAAABC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQCAAAACQYAAAAJBwAAAAkIAAAABQYAAAAtRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkAwAAAAJJRARuYW1lBGRhdGEAAQcICAIAAAABAAAABgkAAAAFQWxwaGEJCgAAAAEHAAAABgAAAAIAAAAGCwAAAARCZXRhCQwAAAABCAAAAAYAAAADAAAABg0AAAAFR2FtbWEJDgAAAA8KAAAACgAAAAgAAAAAAQAAAAIAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAJAAAADwwAAAAKAAAACAAAAAABAAAAAgAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAkAAAAPDgAAAAoAAAAIAAAAAAEAAAACAAAAAwAAAAQAAAAFAAAABgAAAAcAAAAIAAAACQAAAAs=";
			second_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAIAAAAGAwAAAAZTZWNvbmQJBAAAAAQEAAAAqwFTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW0VQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuUGF5bG9hZCwgQXNzZW1ibHktQ1NoYXJwLUVkaXRvci1maXJzdHBhc3MsIFZlcnNpb249MC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsXV0DAAAABl9pdGVtcwVfc2l6ZQhfdmVyc2lvbgQAAC9FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWRbXQIAAAAICAkFAAAAAgAAAAAAAAAHBQAAAAABAAAAAgAAAAQtRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkAgAAAAkGAAAACQcAAAAFBgAAAC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQDAAAAAklEBG5hbWUEZGF0YQABBwgIAgAAAAIAAAAGCAAAAARCZXRhCQkAAAABBwAAAAYAAAADAAAABgoAAAAFR2FtbWEJCwAAAA8JAAAACgAAAAgAAAAAAQAAAAIAAAADAAAABAAAAAUAAAAGAAAABwAAAAgAAAAJAAAADwsAAAAKAAAACAAAAAABAAAAAgAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAkAAAAL";
			third_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAMAAAAGAwAAAAVUaGlyZAkEAAAABAQAAACrAVN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbRVBQWi5QZXJzaXN0ZW5jZS5FZGl0b3IuVGVzdC5FbnRpdGllcy5QYXlsb2FkLCBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcywgVmVyc2lvbj0wLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGxdXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBAAAL0VQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuUGF5bG9hZFtdAgAAAAgICQUAAAABAAAAAAAAAAcFAAAAAAEAAAABAAAABC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQCAAAACQYAAAAFBgAAAC1FUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQDAAAAAklEBG5hbWUEZGF0YQABBwgIAgAAAAMAAAAGBwAAAAVHYW1tYQkIAAAADwgAAAAKAAAACAAAAAABAAAAAgAAAAMAAAAEAAAABQAAAAYAAAAHAAAACAAAAAkAAAAL";
			fourth_string = "AAEAAAD/////AQAAAAAAAAAMAgAAACBBc3NlbWJseS1DU2hhcnAtRWRpdG9yLWZpcnN0cGFzcwUBAAAALEVQUFouUGVyc2lzdGVuY2UuRWRpdG9yLlRlc3QuRW50aXRpZXMuRW50aXR5AwAAAAJJRARuYW1lCHBheWxvYWRzAAEDCKsBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tFUFBaLlBlcnNpc3RlbmNlLkVkaXRvci5UZXN0LkVudGl0aWVzLlBheWxvYWQsIEFzc2VtYmx5LUNTaGFycC1FZGl0b3ItZmlyc3RwYXNzLCBWZXJzaW9uPTAuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAgAAAAQAAAAGAwAAAAZGb3VydGgKCw==";

			first_string_zip = "H4sIAAAAAAAAA6VUXXOjIBT9SatmnVkf9gEwkFjjVFJBfRPtmEaSZqrGj1+/CI2d2bdOfUCBw7nnnnsRgC0AwP+1PCAGj+cAajUiCMtNJAUP2tej7SfO6VReQUc5vVXEm0Ke38prZJUEz+WQwOVguGVxgt/7hLCpdORcEdZnTtKvZySV5SZWc9cqUnrL08Pj2wXDwhAEFNA+47ZEO3h65eM943QGYOujpxYmG83ZhU10F2SU2YbehOPOYVNJcWFTwQ/9C1kwYOR2hxMMi1BCqXTeyg1VmEiGDWtUvHv55iZ5Gllq3ldEWgVns8J+xozb/Vbl5bAuu4xumEQvBcFTiWycK6zY7Lv8IifFOWRpNIcI8jwN5oJ7/fML6A/o9xCegeKIbLGjdnlJPKXPrnbBSaChTnbsTRB5PjrMZcT7yPlvT1xZK7Bdae91XQAxnjBSKO4KNHoR6kKhD8AiV+d0dH3hjG3OI2uJX07uLueuLC/yHDbjknd9YJ2gDMb8zY21TpW3wpzzo/vIp2eEzRVysbgutZJS8cRZKlvh4Cb8Ti9MQ80uuv53cbYWH4wfCPoVH60qDeSz0leldMr40O4xtJXHt8zq5Cuj98Jhvdq3VX2rND5oH1JHKr32DJmntN8kwp7z8HtpvND6ft8lBLuCeKccd5+e13sUJ8uHv94EUGLz3q7rUPXAD/oKLRwoznSzQ13fZq95zVr3kzr9f4cgoaeKKO1xuUfAhNFD3SwvHPNxKMg2QKbndmZvGTWWIM1HUZ7SE4oHk772I/vyo7Y0F3VwJ/g28PX5P086T+OrsTBeef1VCEhMny/zYcUGGjToBcNjGMyR2vyiHkWB8apn94XV+T0bLe9r4M8jBjKsovBX3uWKRXqr/fsPpXzL1SQFAAA=";
			second_string_zip = "H4sIAAAAAAAAA6WTzXabMBCFH6n8hJ6ylLAlTDGnIpYE2iFIjI1w3ALG8PQVcuzkdNcTFgOCq+HONyMA1gCA1bflAgTcry3Y6xhAWLqJkjzqXp7tFXXqujyBPuXpucL+FHNxLk+JVWI0lyOFy8Z4zQhFbwPFbCodNVeYDblDh8celarSJXrtWUWWnkW2vT97YFwyRFEK0iHntgpCWL/w6yXn6QzAehX87CB1Tc4+bpKLxFeVu+lZOt4cN5WSLZsKvh12eNGAK7d7RBEsYgWV9nku3VRrEhU3rNH/u5QHj4ossfR6qLCyCs5mrX3/J+k2a12Xw/q8vXoxTXYFRlMZ2EhorXQ3vWjVpHOOeZbMcQC5yKK54P7waweGbfA0xkegcyS2DFO7bKmv/dlVGNUyGPc0ZAeJ1fHZYR7D/h/Bn3x5Yp1EdmXYg80S8I2J2Amu621JZCADsjQN/B7R7kX7F9waiON3ErOjruMiT5qpwwbt55xP3rbQmnyFJLf+vzcUI09ivxbaM9F1Cm4fZNj0xE7e8iwaY3pnabeFrr/SPSvdbbdBwjCXzpP/zkIz0Tlc1lUhm8TO1v6YVbao0+xIpWes4EksMkWl0yt5tIeKXzstWS21QuWfl76XI3vV3r8LUr9WranjIvd6cAP/C71OZUYM700AGmQQ7x9nIYS3cwE/3pM+ZZDwg0dMz00PvKN49u6zMTDM5irwkDwtbJUqJ4/kmeqkg5pbjgYvMSDlckPQnDf7K/NqOIFGrSH2asnpWmBk5QRAOO43n2cqMMI0EFlaB6QxtUEzZ/kSbrz3b8ZX6qBe8nUUmO8/IuN5/wkQeeRdfUCit9ld1uNDa/auxm65/TR5wL9ct0sws31jbvyEH1rjNf4LQzAq2LQEAAA=";
			third_string_zip = "H4sIAAAAAAAAA6VUy3arIBT9pKumDBwCCiY1rmoCqDPBrjwkua6q8fH1F7Vp77SrZyCim8Pe+xyA0IcQen/mgDF8xh6ezBMjpDaRlmLXvB9sjznns7rDNhFJXVJ3DEVeq3tkKUom1TM0Lwx9HjPyt2OUj8rRU0l5lzms+1qjE602sZkDq0iTOk/3z3cA+znDbpfApMuErXGAzu9ieGQimSD0PfzaILZZcrZhFT0kHXS2SWrpgCmsSi1vfCzEvjvSGQMHYbeEEVSEGmnDs1abxGAiHVa8Mvs91AWwPI0sM+9Kqq1C8MlgP/eMm61vdDm8zW4DCFl0LCgZFbZJbrBys23zmx5Nzj5LoynESOTpbiqE270dYbfHL314hSZHZMsgsdWNuYafXQa7s8T9iQX8Iqm+HhwOOHU/cvHiyjtvJLHLxXtTAhN09YSzguoxh9VcLIiWQuEPyCOwaDoATzpDk4vImvdXIwhyAbS66WtYDbPu0563MuEoFhcQLzyNboO55gfw1NNxyqcSAyLvc620NnniLNWNdEgV/qQXxv7Eb0v9H/JqzT6sfmDklWKwynSn3wy/Mk3GTPTNliDbeFxnVqvfefIoHN6Z/7apb5nGqw+pow1fe0LcNdxrjYnrPP2eGy+0ft53jBIgqXvOSfvp+WmLY7Za/BWKrKP/9R2ZHvhFX+E5B46zeSBoOWe/yuct9CrtIwrOUjA/p8TKYohQf9r+30to7aXAnK02i6vtPPP6BfC6cFqFrtq/163Cl7th4Rx8Y6vlyP8Du94VH0QEAAA=";
			fourth_string_zip = "H4sIAAAAAAAAAz1Ry3KDMAz8pPIIBw45GAImxDDFiW3wDZtMSDApUyA8vr5O2lQHjXZmtZJWAAQAgN3HM0AG3pGAi86+50k7VYLF/flo7ohV1/IOBsxwV0F3QYx38p4aEoarnIj3bEQBzUj4NRJIF2mptYJ0LCwy/vcorKSdaewYZY47nifv2gHTUyGOMcBjwUzlR159ZvOjYHgFINj5h94j9ktzQE36EHBWhY07YTkraiolWrqULBlP8MkBMzOHkIReiZSn9J6dtLHmpAo1tNHzHvLqEJ6nhsZjBZVRMrpq7t/MrN8H+i6LDkU7O4ikpxKGi/TNkGuusPcDb9WiNaciT1fke4zn8Voyd/w8gTHxNxO6Aa2RmiLCpmyJq/czqyiuhT9dSESvAqrb0aIOhe43ZxtX3GkvQrN6ef/7DPjrCYfCpksFLwd/2m5/AHrKrKe0AQAA";

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
				testFolderPath + "third.txt",
				resourcesFolderPath + "third.txt"
			);

			Files.Copy(
				testFolderPath + "fourth.txt",
				resourcesFolderPath + "fourth.txt"
			);

			AssetDatabase.ImportAsset("Assets/Resources/Binary/first.bytes");
			AssetDatabase.ImportAsset("Assets/Resources/Binary/second.bytes");
			AssetDatabase.ImportAsset("Assets/Resources/Binary/third.txt");
			AssetDatabase.ImportAsset("Assets/Resources/Binary/fourth.txt");
		}

		void _RecreateTestFiles()
		{
			serializer.manageFileExtensions = false;
			serializer.ObjectToFile(first, testFolderPath + "first.bytes");
			serializer.ObjectToFile(second, testFolderPath + "second.bytes");
			serializer.ObjectToFile(third, testFolderPath + "third.txt");
			serializer.ObjectToFile(fourth, testFolderPath + "fourth.txt");
			serializer.manageFileExtensions = true;

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
			Files.DeleteAsset("Assets/Resources/Binary/first.bytes");
			Files.DeleteAsset("Assets/Resources/Binary/second.bytes");
			Files.DeleteAsset("Assets/Resources/Binary/third.txt");
			Files.DeleteAsset("Assets/Resources/Binary/fourth.txt");

			Files.DeleteAsset("Assets/Resources/Binary/first_test.bytes");
			Files.DeleteAsset("Assets/Resources/Binary/second_test.bytes");
			Files.DeleteAsset("Assets/Resources/Binary/third_test.txt");
			Files.DeleteAsset("Assets/Resources/Binary/fourth_test.txt");

			Files.DeleteFolderIfEmpty(resourcesFolderPath);
			Files.DeleteFolderAnyway(tempFolderPath);
		}


	#region String

	#endregion


	#region File

		[Test]
		public void ObjectToFile_Without_ManageFileExtensions()
		{
			// Opt-out file extension management.
			serializer.TurnOffFileExtensionManagement();

			// No extension added silently.
			serializer.ObjectToFile(first, tempFolderPath+"first_test_extension");
			FileAssert.DoesNotExist(tempFolderPath + "first_test_extension.bytes");
			FileAssert.Exists(tempFolderPath + "first_test_extension");

			// Any extension can be used.
			serializer.ObjectToFile(first, tempFolderPath+"first_test_extension.archive");
			FileAssert.DoesNotExist(tempFolderPath + "first_test_extension.bytes");
			FileAssert.Exists(tempFolderPath + "first_test_extension.archive");

			// Opt-in file extension management (for the rest of the tests).
			serializer.TurnOnFileExtensionManagement();
		}	

	#endregion
	

	#region Resource

	#endregion


	}
}
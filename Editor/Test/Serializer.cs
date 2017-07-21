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


	/// <summary>
	/// Being an `abstract class` tests will only run in subclasses.
	/// </summary>
	[TestFixture]
	public abstract class Serializer
	{


		/// <summary>
		/// Tests in this fixture covers base functionality of every `Serializer`.
		/// </summary>
		protected EPPZ.Persistence.Serializer serializer;

		// Folders.
		protected string testFolderPath;
		protected string tempFolderPath;
		protected string resourcesFolderPath;

		// Model.
		protected Payload alpha, beta, gamma;
		protected Entity first, second, third, fourth;
		protected string first_string, second_string, third_string, fourth_string;


		public virtual void Setup()
		{
			// Folders.
			testFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/";
			tempFolderPath = Application.dataPath + "/Plugins/eppz!/Persistence/Editor/Test/Entities/.temp/";
			resourcesFolderPath = Application.dataPath + "/Resources/";
			Files.CreateFolderIfNotExist(tempFolderPath);
			Files.CreateFolderIfNotExist(resourcesFolderPath);

			// Models.
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
		}


	#region Models

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

			// `null` payloads.
			Assert.AreEqual(
				new Entity(0, "Empty"),
				new Entity(0, "Empty")
			);

			// `null` payloads.
			Assert.AreNotEqual(
				new Entity(3, "Third", new Payload(3, "Gamma", 0,1,2,3,4,5,6,7,8,9)),
				new Entity(0, "Empty")
			);

			// `null` payloads.
			Assert.AreNotEqual(
				new Entity(0, "Empty"),
				new Entity(3, "Third", new Payload(3, "Gamma", 0,1,2,3,4,5,6,7,8,9))
			);
		}

	#endregion


	#region String

		[Test]
		public void StringToObject()
		{
			Assert.AreEqual(
				serializer.StringToObject<Entity>(first_string),
				first
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(second_string),
				second
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(third_string),
				third
			);

			Assert.AreEqual(
				serializer.StringToObject<Entity>(fourth_string),
				fourth
			);

			// Error.
			Assert.IsNull(
				serializer.StringToObject<Entity>("<ERROR>")
			);
		}

		[Test]
		public void ObjectToString()
		{
			Assert.AreEqual(
				serializer.ObjectToString(first),
				first_string
			);

			Assert.AreEqual(
				serializer.ObjectToString(second),
				second_string
			);
			Assert.AreEqual(
				serializer.ObjectToString(third),
				third_string
			);

			Assert.AreEqual(
				serializer.ObjectToString(fourth),
				fourth_string
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

			// Error.
			Assert.IsNull(
				serializer.FileToObject<Entity>("<ERROR>")
			);
		}

		[Test]
		public void ObjectToFile()
		{			
			serializer.ObjectToFile(first, tempFolderPath+"first_test");
			Assert.AreEqual(
				first_string,
				File.ReadAllText(serializer.GetFilePathWithExtension(tempFolderPath+"first_test"))
			);

			serializer.ObjectToFile(second, tempFolderPath+"second_test");
			Assert.AreEqual(
				second_string,
				File.ReadAllText(serializer.GetFilePathWithExtension(tempFolderPath+"second_test"))
			);

			serializer.ObjectToFile(third, tempFolderPath+"third_test");
			Assert.AreEqual(
				third_string,
				File.ReadAllText(serializer.GetFilePathWithExtension(tempFolderPath+"third_test"))
			);

			serializer.ObjectToFile(fourth, tempFolderPath+"fourth_test");
			Assert.AreEqual(
				fourth_string,
				File.ReadAllText(serializer.GetFilePathWithExtension(tempFolderPath+"fourth_test"))
			);
		}

		[Test]
		public void FileToObject_Extensions()
		{
			// Extension gets added silently (See `Serializer.GetFilePathWithExtension()`).
			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"first"),
				first
			);

			// Any other extension gets replaced silently.
			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"second.json"),
				second
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"third.zip"),
				third
			);

			Assert.AreEqual(
				serializer.FileToObject<Entity>(testFolderPath+"fourth.bytes"),
				fourth
			);
		}

		[Test]
		public void ObjectToFile_Extensions()
		{
			// Extension gets added silently (See `Serializer.GetFilePathWithExtension()`).
			serializer.ObjectToFile(first, tempFolderPath+"first_test");
			Files.AreEqual(
				testFolderPath+"first_test."+serializer.Extension,
				tempFolderPath+"first_test."+serializer.Extension
			);

			// Any other extension gets replaced silently (see examples of extension appending).
			serializer.ObjectToFile(second, tempFolderPath+"second_test.json");
			Files.AreEqual(
				testFolderPath+"second_test".WithExtension(serializer),
				tempFolderPath+"second_test".WithExtension(serializer)
			);

			serializer.ObjectToFile(third, tempFolderPath+"third_test.zip");
			Files.AreEqual(
				Path.ChangeExtension(testFolderPath+"third_test", serializer.Extension),
				Path.ChangeExtension(tempFolderPath+"third_test", serializer.Extension)
			);

			serializer.ObjectToFile(fourth, tempFolderPath+"fourth_test.bytes");
			Files.AreEqual(
				serializer.GetFilePathWithExtension(testFolderPath+"fourth_test"),
				serializer.GetFilePathWithExtension(tempFolderPath+"fourth_test")
			);
		}	

	#endregion


	#region Resource

		[Test]
		public void ResourceToObject()
		{
			Assert.AreEqual(
				first,
				serializer.ResourceToObject<Entity>("first")
			);
			
			Assert.AreEqual(
				second,
				serializer.ResourceToObject<Entity>("second")
			);
			
			Assert.AreEqual(
				third,
				serializer.ResourceToObject<Entity>("third")
			);
			
			Assert.AreEqual(
				fourth,
				serializer.ResourceToObject<Entity>("fourth")
			);
		}

		[Test]
		public void ObjectToResource()
		{			
			// Write to `Assets/Resources`, import, then read back as resource (using `UnityEngine.Resources`).
			serializer.ObjectToFile(first, resourcesFolderPath + "first_test");
			AssetDatabase.ImportAsset("Assets/Resources/first_test".WithExtension(serializer));
			Assert.AreEqual(
				first_string.Bytes(),
				(Resources.Load("first_test") as TextAsset).bytes
			);

			serializer.ObjectToFile(second, resourcesFolderPath + "second_test");
			AssetDatabase.ImportAsset("Assets/Resources/second_test".WithExtension(serializer));
			Assert.AreEqual(
				second_string.Bytes(),
				(Resources.Load("second_test") as TextAsset).bytes
			);

			serializer.ObjectToFile(third, resourcesFolderPath + "third_test");
			AssetDatabase.ImportAsset("Assets/Resources/third_test".WithExtension(serializer));
			Assert.AreEqual(
				third_string.Bytes(),
				(Resources.Load("third_test") as TextAsset).bytes
			);

			serializer.ObjectToFile(fourth, resourcesFolderPath + "fourth_test");
			AssetDatabase.ImportAsset("Assets/Resources/fourth_test".WithExtension(serializer));
			Assert.AreEqual(
				fourth_string.Bytes(),
				(Resources.Load("fourth_test") as TextAsset).bytes
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
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


namespace EPPZ.Persistence
{


	using Extensions;


    public class Serializer
    {
        

		public bool log;

		/// <summary>
		/// Managing file extensions (see `#Manage file extensions` region) can be opted-out here.
		/// </summary>		
		public bool manageFileExtensions = true;

		public Serializer TurnOffFileExtensionManagement()
		{
			manageFileExtensions = false;
			return this;
		}
		
		public Serializer TurnOnFileExtensionManagement()
		{
			manageFileExtensions = true;
			return this;
		}


	#region Singleton

		static Serializer defaultSerializer;

		/// <summary>
		/// Method that returns a `Serializer` in any way.
		/// </summary>
		/// <param name="serializer">Any serializer instance (or `null`).</param>
		public static Serializer SerializerOrDefault(Serializer serializer)
		{
			if (serializer != null) return serializer;
			if (defaultSerializer == null) defaultSerializer = new JSONSerializer();
			return defaultSerializer;
		}

		/// <summary>
		/// Sets this `Serializer` as the default.
		/// </summary>
		public void SetDefaultSerializer()
		{ defaultSerializer = this; }

	#endregion


	#region Manage file extensions

		public string PrimaryFileExtension
		{ get { return FileExtensions[0]; } }

		public string SecondaryFileExtension
		{ get { return FileExtensions[1]; } }

		public string GetExistingFilePathWithFileExtensions(string filePath)
		{
			// Only if file extension management turned on.
			if (manageFileExtensions == false) return filePath;

			for (int index = 0; index < FileExtensions.Length; index++)
			{
				string eachExtension = FileExtensions[index];
				string eachFilePath = Path.ChangeExtension(filePath, eachExtension);
				if (File.Exists(eachFilePath))
				{ return eachFilePath; }
			}

			Debug.LogWarning("No existing file with any extension at `"+filePath+"`. Returning path using primary `"+PrimaryFileExtension+"` extension.");
			return Path.ChangeExtension(filePath, PrimaryFileExtension);
		}

		public string CreateFilePathWithPrimaryFileExtension(string filePath)
		{
			// Only if file extension management turned on.
			if (manageFileExtensions == false) return filePath;

			return Path.ChangeExtension(filePath, PrimaryFileExtension);
		}

		public string CreateFilePathWithSecondaryFileExtension(string filePath)
		{
			// Only if file extension management turned on.
			if (manageFileExtensions == false) return filePath;

			return Path.ChangeExtension(filePath, SecondaryFileExtension);
		}

        public bool IsFileExistWithFileExtensions(string filePath)
		{
			// Only if file extension management turned on.
			if (manageFileExtensions == false) return File.Exists(filePath);

			for (int index = 0; index < FileExtensions.Length; index++)
			{
				string eachExtension = FileExtensions[index];
				if (File.Exists(Path.ChangeExtension(filePath, eachExtension)))
				{ return true; }
			}

			Debug.LogWarning("No existing file with any extension at `"+filePath+"`.");
			return false;
		}

	#endregion


	#region Aliases
		
		public string ObjectToString(object _object)
		{ return SerializeObjectToString(_object); }

		public void ObjectToFile(object _object, string filePath)
		{ SerializeObjectToFile(_object, filePath); }


		public T StringToObject<T>(string _string)
		{ return DeserializeString<T>(_string); }

		/// <summary>
		/// Uses `System.IO.File.ReadAllText` to load file content.
		/// </summary>
		/// <param name="filePath">Absolute file system path of file.</param>
		public T FileToObject<T>(string filePath)
		{ return DeserializeFile<T>(filePath); }

		/// <summary>
		/// Uses `UnityEngine.Resource.Load` to load file content.
		/// </summary>
		/// <param name="resourcePath">Path of resource relative to `Assets/Resources` folder of project.</param>
		public T ResourceToObject<T>(string resourcePath)
		{ return DeserializeResource<T>(resourcePath); }

		/// <summary>
		/// Lookup absolute `filePath` first, then `resourcePath` in resources.
		/// Useful when providing defaults for files in `Assets/Resources`.
		/// </summary>
		/// <param name="filePath">Absolute file system path of file.</param>
		/// <param name="resourcePath">Path of resource relative to `Assets/Resources` folder of project.</param>
		public T FileOrResourceToObject<T>(string filePath, string resourcePath)
		{ return DeserializeFileOrResource<T>(filePath, resourcePath); }

	#endregion


	#region Templates

		public virtual string[] FileExtensions
		{ get { return new string[0]; } }


		public virtual string SerializeObjectToString(object _object)
		{ return null; }

		public virtual void SerializeObjectToFile(object _object, string filePath)
		{ return; }


		public virtual T DeserializeString<T>(string _string)
		{ return default(T); }

		public virtual T DeserializeFile<T>(string filePath)
		{ return default(T); }

		public virtual T DeserializeResource<T>(string resourcePath)
		{ return default(T); }

		public virtual T DeserializeFileOrResource<T>(string filePath, string resourcePath)
		{
			T _object = default(T);

			_object = DeserializeFile<T>(filePath);
			if (_object == null)
			{
				Warning("Can't find file `" + filePath + "`.");
				Log("Load `" + resourcePath + "` as resource.");
				_object = DeserializeResource<T>(resourcePath);
				if (_object == null)
				{ Warning("Can't find resource `" + resourcePath + "`."); }
			}
			else
			{ Log("Load `" + filePath + "` as file."); }

			return _object;
		}

		void Log(string message)
		{ if (log) Debug.Log(message); }

		void Warning(string message)
		{ if (log) Debug.LogWarning(message); }


	#endregion
	

    }   
}
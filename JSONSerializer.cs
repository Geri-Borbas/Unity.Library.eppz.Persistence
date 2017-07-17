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


    public class JSONSerializer : Serializer
    {


	#region Aliases

		public enum Mode { Default, Pretty }

		public void ApplyStringTo(string _string, object _object)
		{ ApplyDeserializedStringToObject(_string, _object); }

		public void ApplyFileTo(string filePath, object _object)
		{ ApplyDeserializedFileToObject(filePath, _object); }

		public void ApplyStreamTo(Stream stream, object _object)
		{ ApplyDeserializedStreamToObject(stream, _object); }

		public void ObjectToFile(object _object, string filePath, Mode mode)
		{ SerializeObjectToFile(_object, filePath, mode); }

	#endregion


	#region Overrides

		public override string Extension
		{ get { return "json"; } }


		public override string SerializeObjectToString(object _object)
		{ return SerializeObjectToString(_object, Mode.Default); }

		public string SerializeObjectToString(object _object, Mode mode)
		{ return JsonUtility.ToJson(_object, (mode == Mode.Pretty)); }

		public override void SerializeObjectToFile(object _object, string filePath)
		{ SerializeObjectToFile(_object, filePath, Mode.Default); }

		public void SerializeObjectToFile(object _object, string filePath, Mode mode)
		{
			string JSON = JsonUtility.ToJson(_object, (mode == Mode.Pretty));
			File.WriteAllText(GetFilePathWithExtension(filePath), JSON);	
		}


		public override T DeserializeString<T>(string _string)
		{ 
			T _object = default(T);
			try
			{ _object = JsonUtility.FromJson<T>(_string); }
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }

			return _object;
		}

		public void ApplyDeserializedStringToObject(string _string, object _object)
		{
			try
			{ JsonUtility.FromJsonOverwrite(_string, _object); }
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }
		}

		public override T DeserializeFile<T>(string filePath)
		{
			if (IsFileExist(filePath) == false) return default(T); // Only if saved any

			T _object = default(T);
			try
			{
				string JSON = File.ReadAllText(GetFilePathWithExtension(filePath));
				_object = JsonUtility.FromJson<T>(JSON);
			}
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }

			return _object;
		}

		public void ApplyDeserializedFileToObject(string filePath, object _object)
		{
			if (IsFileExist(filePath) == false) return; // Only if saved any

			try
			{
				string JSON = File.ReadAllText(GetFilePathWithExtension(filePath));
				JsonUtility.FromJsonOverwrite(JSON, _object);
			}
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }
		}

		public override T DeserializeStream<T>(Stream stream)
		{ 
			T _object = default(T);
			try
			{
				StreamReader reader = new StreamReader(stream);
				string JSON = reader.ReadToEnd();
				_object = JsonUtility.FromJson<T>(JSON);
			}
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }

			return _object;
		}

		public void ApplyDeserializedStreamToObject(Stream stream, object _object)
		{
			try
			{
				StreamReader reader = new StreamReader(stream);
				string JSON = reader.ReadToEnd();
				JsonUtility.FromJsonOverwrite(JSON, _object);
			}
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }
		}

	#endregion
	

    }   
}
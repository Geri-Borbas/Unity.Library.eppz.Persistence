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


    public class JSONSerializer : Serializer
    {


	#region Mode

		public enum Mode { Default, Pretty }
		public Mode mode;
		
		public Serializer Pretty()
		{
			mode = Mode.Pretty;
			return this;
		}
		
		public Serializer Default()
		{
			mode = Mode.Default;
			return this;
		}

	#endregion


	#region Aliases

		public void ApplyStringTo(string _string, object _object)
		{ ApplyDeserializedStringToObject(_string, _object); }

		public void ApplyFileTo(string filePath, object _object)
		{ ApplyDeserializedFileToObject(filePath, _object); }

		public void ApplyResourceTo(string resourcePath, object _object)
		{ ApplyDeserializedResourceToObject(resourcePath, _object); }

	#endregion


	#region Overrides

		public override string Extension
		{ get { return "json"; } }

	#endregion


	#region String

		public override string SerializeObjectToString(object _object)
		{ return JsonUtility.ToJson(_object, (mode == Mode.Pretty)); }

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

	#endregion


	#region File

		public override void SerializeObjectToFile(object _object, string filePath)
		{
			string JSON = JsonUtility.ToJson(_object, (mode == Mode.Pretty));
			File.WriteAllText(GetFilePathWithExtension(filePath), JSON);	
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

	#endregion


	#region Resource

		public override T DeserializeResource<T>(string resourcePath)
		{ 
			T _object = default(T);
			try
			{
				TextAsset textAsset = Resources.Load(resourcePath) as TextAsset;
				if (textAsset == null)
				{ return _object; }
				string JSON = textAsset.text;
				_object = JsonUtility.FromJson<T>(JSON);
			}
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }

			return _object;
		}
		
		public void ApplyDeserializedResourceToObject(string resourcePath, object _object)
		{
			try
			{
				TextAsset textAsset = Resources.Load(resourcePath) as TextAsset;
				if (textAsset == null)
				{ return; }
				string JSON = textAsset.text;
				JsonUtility.FromJsonOverwrite(JSON, _object);
			}
			catch (Exception exception)
			{ Debug.LogWarning("Serializer exception: "+exception); }
		}

	#endregion
	

    }   
}
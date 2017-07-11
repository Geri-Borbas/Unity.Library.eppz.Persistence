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


    public class Serializer
    {
        

	#region Aliases

		public string GetFilePathWithExtension(string filePath)
		{ return filePath + "." + Extension; }

        public bool IsFileExist(string filePath)
		{ return File.Exists(GetFilePathWithExtension(filePath)); }


		public string ObjectToString(object _object)
		{ return SerializeObjectToString(_object); }

		public void ObjectToFile(object _object, string filePath)
		{ SerializeObjectToFile(_object, filePath); }


		public T StringToObject<T>(string _string)
		{ return DeserializeString<T>(_string); }

		public T FileToObject<T>(string filePath)
		{ return DeserializeFile<T>(filePath); }

		public T StreamToObject<T>(Stream stream)
		{ return DeserializeStream<T>(stream); }

	#endregion


	#region Templates

		public virtual string Extension
		{ get { return null; } }

		public virtual string SerializeObjectToString(object _object)
		{ return null; }

		public virtual void SerializeObjectToFile(object _object, string filePath)
		{ return; }

		public virtual T DeserializeString<T>(string _string)
		{ return default(T); }

		public virtual T DeserializeFile<T>(string filePath)
		{ return default(T); }

		public virtual T DeserializeStream<T>(Stream stream)
		{ return default(T); }

	#endregion
	

    }   
}
//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using NUnit.Framework;
#endif

using System.IO;


namespace EPPZ.Persistence
{


	public static class Files
	{


        public static void CreateFolder(string path)
        { CreateFolderIfNotExist(path); }

		public static void CreateFolderIfNotExist(string path)
		{
			if (!Directory.Exists(path))
            { Directory.CreateDirectory(path); }	
		}

        public static void Copy(string from, string to)
        { CopyFileIfNotExist(from, to); }

		public static void CopyFileIfNotExist(string from, string to)
		{
			if (!File.Exists(from)) return;
			if (File.Exists(to)) return;
			File.Copy(from, to);
		}

        public static void Delete(string path)
        { DeleteFileIfExists(path); }

		public static void DeleteFileIfExists(string path)
		{
			if (!File.Exists(path)) return;
			File.Delete(path);
		}

        public static void DeleteAsset(string assetPath)
        { DeleteAssetIfExists(assetPath); }

		public static void DeleteAssetIfExists(string assetPath)
		{
			if (!File.Exists(assetPath)) return;
			
			#if UNITY_EDITOR			
			AssetDatabase.DeleteAsset(assetPath);
			#endif
		}

		public static void DeleteFolderIfEmpty(string path)
		{
			if (!Directory.Exists(path)) return; // Only directory if exists at all
			
			// Get a fresh directory info.
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			directoryInfo.Refresh();
			int fileCount = directoryInfo.GetFiles().Length;		
			int directoryCount = directoryInfo.GetDirectories().Length;			

			if (fileCount > 0) return; // Only if no files within
			if (directoryCount > 0) return; // Only if no folders within
			Directory.Delete(path);
		}

		public static void DeleteFolderAnyway(string path)
		{
			if (!Directory.Exists(path)) return; // Only directory if exists at all
			Directory.Delete(path, true);
		}

		public static void AreEqual(string path_a, string path_b)
        {
            using (StreamReader stream_a = File.OpenText(path_a))
            {
                using (StreamReader stream_b = File.OpenText(path_b))
                {
                    string string_a = stream_a.ReadToEnd();
                    string string_b = stream_b.ReadToEnd();

					#if UNITY_EDITOR
                    Assert.AreEqual(string_a, string_b);
					#endif
                }
            }
        }
	}
}

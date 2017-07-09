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
using System.IO;
using System.Text;
using System.IO.Compression;


namespace EPPZ.Persistence
{


	public static class String
	{


		/// <summary>
		/// A fallback for `Stream.CopyTo()` (only introduced in .NET 4).
		/// </summary>
		public static void _CopyTo(this Stream this_, Stream outputStream, int bufferSize = 4096)
        {
            byte[] bytes = new byte[bufferSize];
            int bytesRead;
            while ((bytesRead = this_.Read(bytes, 0, bytes.Length)) != 0)
            { outputStream.Write(bytes, 0, bytesRead); }
        }

		public static string Zip(this string this_)
		{
			byte[] stringBytes = Encoding.UTF8.GetBytes(this_);
			byte[] outputBytes = CompressBytes(stringBytes);
			return Convert.ToBase64String(outputBytes);
		}

		static byte[] CompressBytes(byte[] inputBytes)
		{
			using (MemoryStream inputStream = new MemoryStream(inputBytes))
				using (MemoryStream outputStream = new MemoryStream())
				{
					using (GZipStream zipStream = new GZipStream(outputStream, CompressionMode.Compress))
					{ inputStream._CopyTo(zipStream); }
					return outputStream.ToArray();
				}
		}

		public static string Unzip(this string this_)
		{
			byte[] inputBytes = Convert.FromBase64String(this_);
			byte[] outputBytes = DecompressBytes(inputBytes);
			return Encoding.UTF8.GetString(outputBytes);
		}

		static byte[] DecompressBytes(byte[] bytes)
		{
			using (MemoryStream inputStream = new MemoryStream(bytes))
    			using (MemoryStream outputStream = new MemoryStream())
				{
					using (GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
					{ zipStream._CopyTo(outputStream); }
					return outputStream.ToArray();
				}
		}
	}
}
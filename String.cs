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
using System.Text;
using System.IO.Compression;


namespace EPPZ.Persistence
{


	public static class Bytes_Extensions
	{


		/// <summary>
		/// Get string from UTF8 bytes.
		/// </summary>
		public static string String(this byte[] this_)
		{ return Encoding.UTF8.GetString(this_); }

		/// <summary>
		/// Get string from Base64 bytes.
		/// </summary>
		public static string StringFromBase64(this byte[] this_)
		{ return Convert.ToBase64String(this_); }
	}


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

		/// <summary>
		/// An alias to `Serializer.GetFilePathWithExtension()`.
		/// </summary>
		public static string WithExtension(this string this_, Serializer serializer)
		{ return serializer.GetFilePathWithExtension(this_); }

		/// <summary>
		/// Get UTF8 bytes from string.
		/// </summary>
		public static byte[] Bytes(this string this_)
		{ return Encoding.UTF8.GetBytes(this_); }

		/// <summary>
		/// Get Base64 bytes from string.
		/// </summary>
		public static byte[] Base64Bytes(this string this_)
		{ return Convert.FromBase64String(this_); }

		public static string StringFromBytes(byte[] bytes)
		{ return Convert.ToBase64String(bytes); }

		public static string Zip(this string this_)
		{ return CompressBytes(this_.Bytes()).StringFromBase64(); }

		public static string Unzip(this string this_)
		{ return DecompressBytes(this_.Base64Bytes()).String(); }

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
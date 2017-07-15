//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections.Generic;


namespace EPPZ.Persistence.Editor.Test.Entities
{


	[Serializable]
	public class Payload : IEquatable<Payload>
	{


        public int ID;
		public string name;
		public int[] data;
        string dataToString
        {
            get
            {
                string string_ = "";
                foreach (int eachData in data)
                { string_ += eachData+","; }
                return string_;
            }
        }

		public Payload(int ID, string name, params int[] data)
		{
			this.ID = ID;
			this.name = name;
			this.data = data;			
		}

		public bool Equals(Payload other)
		{
			return (
				this.ID == other.ID &&
				this.name == other.name &&
				this.dataToString == other.dataToString
			);
		}
	}
}
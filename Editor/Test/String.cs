//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using NUnit.Framework;


namespace EPPZ.Persistence.Editor.Test
{


	using Extensions;


	[TestFixture]
	public class String
	{


		[OneTimeSetUp]
		public void Setup()
		{ }

		[OneTimeTearDown]
		public void TearDown()
		{ }

		[Test]
		public void Zip()
		{
			Assert.AreEqual(
				"H4sIAAAAAAAAAwvOz01VKEktLlEoLinKzEtXKMlXSM7PLShKLS7WAwAr6BTWHQAAAA==",
				"Some test string to compress.".Zip()
			);
			
			Assert.AreEqual(
				"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=",
				"Batman".Zip()
			);

			string JSON = "{'shapeSnapshots':[{'name':'Large Triangle 1','active':true,'selected':false,'position_x':-0.10151994228363037,'position_y':-0.12396955490112305,'rotation':44.999996185302734,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 2']},{'name':'Large Triangle 2','active':true,'selected':false,'position_x':-1.5157337188720703,'position_y':-1.5381829738616943,'rotation':315,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 1']},{'name':'Middle Triangle','active':true,'selected':false,'position_x':-0.8086267113685608,'position_y':-0.8310763835906982,'rotation':315,'locked':false,'linkedShapeNames':['Square','Large Triangle 1','Large Triangle 2']},{'name':'Small Triangle 1','active':true,'selected':false,'position_x':0.5517618656158447,'position_y':2.267948627471924,'rotation':67.5,'locked':false,'linkedShapeNames':[]},{'name':'Small Triangle 2','active':true,'selected':false,'position_x':-0.10151970386505127,'position_y':2.7044572830200195,'rotation':135,'locked':false,'linkedShapeNames':[]},{'name':'Rhombus','active':true,'selected':false,'position_x':-1.2167412042617798,'position_y':1.2109876871109009,'rotation':157.5,'locked':false,'linkedShapeNames':[]},{'name':'Square','active':true,'selected':false,'position_x':0.3984801173210144,'position_y':-0.6239694952964783,'rotation':90,'locked':false,'linkedShapeNames':['Middle Triangle','Large Triangle 1','Large Triangle 2']}],'align':0,'puzzleID':'000011'}";
			string ZIP = "H4sIAAAAAAAAA62UT2/UMBDFv0pvvoRoxmN7PDlzQQIOLDdUIbMbdqN6kyXJImjV784EVLF/SrUp9SmxR5r387znOzNs0q5etGk3bLpxMNWnO9OmbW0q8zb16/rqY9+kdp3rKzSFScux+a5nY7+vCzPUuV6O9cpUX1MedGPXDc3YdO3nH6Z6BSUCehRx1kYKBMQHFT//VFiSIN47AdRv8IXpuzFNFaZyrpRpBYyewDK5wuRueXPQMDet/i4mhPcqepJvFt/2qa9V7Ltmtcp/AXTnhMia6/viX7h2Ji6WHj0TMcbIFhjohFYLKGK0whQDBnF0CEvoX5gOj+jOy+fNMkIMNjAihegDxLNRRkLgQJG8QJBo/wvuEes9ObvFNuX8bKtC6T2y2iz4gD46d2JUWyq5OL0Adoxi3SFb4PIiuCfkzrXaQ7LUYyoZPNozwQzOedbcgQVAOcoV0mzBHzbd9st+mB0Ji4EdWnA2ILOcuGY6B4kcovoKBECOZPrn3OyDg2aNnyS6qC8QkwpC587MHX6/U068leA4HiVX4CJvX5DXR01+rSy5WWsj7bPb397m+s1rJQVdiOb+F1EcMerCBQAA";
			Assert.AreEqual(
				ZIP,
				JSON.Zip()
			);
		}

		[Test]
		public void Unzip()
		{			
			// See pako.js counterpart at http://jsfiddle.net/9yH7M/847/
			Assert.AreEqual(
				"Some test string to compress.",
				"H4sIAAAAAAAAAwvOz01VKEktLlEoLinKzEtXKMlXSM7PLShKLS7WAwAr6BTWHQAAAA==".Unzip()
			);		

			// See pako.js counterpart at http://jsfiddle.net/9yH7M/845/
			Assert.AreEqual(
				"Batman",
				"H4sIAAAAAAAAA3NKLMlNzAMAOC2+JQYAAAA=".Unzip()
			);

			// See pako.js counterpart at http://jsfiddle.net/9yH7M/849/
			string JSON = "{'shapeSnapshots':[{'name':'Large Triangle 1','active':true,'selected':false,'position_x':-0.10151994228363037,'position_y':-0.12396955490112305,'rotation':44.999996185302734,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 2']},{'name':'Large Triangle 2','active':true,'selected':false,'position_x':-1.5157337188720703,'position_y':-1.5381829738616943,'rotation':315,'locked':false,'linkedShapeNames':['Square','Middle Triangle','Large Triangle 1']},{'name':'Middle Triangle','active':true,'selected':false,'position_x':-0.8086267113685608,'position_y':-0.8310763835906982,'rotation':315,'locked':false,'linkedShapeNames':['Square','Large Triangle 1','Large Triangle 2']},{'name':'Small Triangle 1','active':true,'selected':false,'position_x':0.5517618656158447,'position_y':2.267948627471924,'rotation':67.5,'locked':false,'linkedShapeNames':[]},{'name':'Small Triangle 2','active':true,'selected':false,'position_x':-0.10151970386505127,'position_y':2.7044572830200195,'rotation':135,'locked':false,'linkedShapeNames':[]},{'name':'Rhombus','active':true,'selected':false,'position_x':-1.2167412042617798,'position_y':1.2109876871109009,'rotation':157.5,'locked':false,'linkedShapeNames':[]},{'name':'Square','active':true,'selected':false,'position_x':0.3984801173210144,'position_y':-0.6239694952964783,'rotation':90,'locked':false,'linkedShapeNames':['Middle Triangle','Large Triangle 1','Large Triangle 2']}],'align':0,'puzzleID':'000011'}";
			string ZIP = "H4sIAAAAAAAAA62UT2/UMBDFv0pvvoRoxmN7PDlzQQIOLDdUIbMbdqN6kyXJImjV784EVLF/SrUp9SmxR5r387znOzNs0q5etGk3bLpxMNWnO9OmbW0q8zb16/rqY9+kdp3rKzSFScux+a5nY7+vCzPUuV6O9cpUX1MedGPXDc3YdO3nH6Z6BSUCehRx1kYKBMQHFT//VFiSIN47AdRv8IXpuzFNFaZyrpRpBYyewDK5wuRueXPQMDet/i4mhPcqepJvFt/2qa9V7Ltmtcp/AXTnhMia6/viX7h2Ji6WHj0TMcbIFhjohFYLKGK0whQDBnF0CEvoX5gOj+jOy+fNMkIMNjAihegDxLNRRkLgQJG8QJBo/wvuEes9ObvFNuX8bKtC6T2y2iz4gD46d2JUWyq5OL0Adoxi3SFb4PIiuCfkzrXaQ7LUYyoZPNozwQzOedbcgQVAOcoV0mzBHzbd9st+mB0Ji4EdWnA2ILOcuGY6B4kcovoKBECOZPrn3OyDg2aNnyS6qC8QkwpC587MHX6/U068leA4HiVX4CJvX5DXR01+rSy5WWsj7bPb397m+s1rJQVdiOb+F1EcMerCBQAA";
			Assert.AreEqual(
				JSON,
				ZIP.Unzip()
			);
		}
	}
}
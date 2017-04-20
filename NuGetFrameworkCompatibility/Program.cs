//
// Program.cs
//
// Author:
//       Matt Ward <matt.ward@xamarin.com>
//
// Copyright (c) 2017 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using NuGet.Frameworks;
using System.Linq;

namespace NuGetFrameworkCompatibility
{
	class Program
	{
		public static void Main (string[] args)
		{
			try {
				var program = new Program ();
				program.Run ();
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		void Run ()
		{
			// Json.NET 9.0.1
			var frameworks = new [] {
				"net20",
				"net35",
				"net40",
				"net45",
				"netstandard1.0",
				"portable-net40+sl5+wp80+win8+wpa81",
				"portable-net45+wp80+win8+wpa81"
			};

			// Json.NET 10.0.2
			var frameworks2 = new [] {
				"net20",
				"net35",
				"net40",
				"net45",
				"netstandard1.0",
				"netstandard1.3",
				"portable-net40+sl5+wp80+win8+wpa81",
				"portable-net45+wp80+win8+wpa81"
			};

			var frameworks3 = new [] {
				"portable-net45+win8",
				"netstandard1.1",
			};

			NuGetFramework nearest = GetNearestFramework ("xamarinios10", frameworks3);

			if (nearest != null) {
				Console.WriteLine ("Nearest framework: {0}", nearest.GetShortFolderName ());
			} else {
				Console.WriteLine ("No nearest framework.");
			}
		}

		NuGetFramework GetNearestFramework (string target, IEnumerable<string> frameworks)
		{
			NuGetFramework targetFramework = NuGetFramework.Parse (target);

			var possibleFrameworks = frameworks
				.Select (framework => NuGetFramework.Parse (framework))
				.ToList ();
			
			var reducer = new FrameworkReducer ();
			return reducer.GetNearest (targetFramework, possibleFrameworks);
		}
	}
}

#region Copyright (c) 2011-2019 Technosoftware GmbH. All rights reserved
//-----------------------------------------------------------------------------
// Copyright (c) 2011-2019 Technosoftware GmbH. All rights reserved
// Web: https://technosoftware.com 
// 
// Purpose: 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// SPDX-License-Identifier: MIT
//-----------------------------------------------------------------------------
#endregion Copyright (c) 2011-2019 Technosoftware GmbH. All rights reserved

#region Using Directives
using System;
using OpcClientSdk;
using OpcClientSdk.Hda;
using OpcClientSdk.Da;
#endregion

namespace HdaConsole
{

	/// <summary>
	/// Simple OPC HDA Client Application
	/// </summary>
	class ConsoleApplication
	{

		///////////////////////////////////////////////////////////////////////
		#region Event Handlers

		/// <summary>
		/// Called when a read request completes.
		/// </summary>
		public void OnReadComplete(IOpcRequest request, TsCHdaItemValueCollection[] results)
		{
			Console.WriteLine("OnReadComplete():");

		}

		#endregion

		///////////////////////////////////////////////////////////////////////
		#region OPC Sample Functionality

		void DoOPCCalls()
		{
			try
			{
				const string serverUrl = "opchda://localhost/OPCSample.OpcHdaServer";

				Console.WriteLine();
				Console.WriteLine("Simple OPC HDA Client based on the OPC DA/AE/HDA Client SDK .NET");
				Console.WriteLine("----------------------------------------------------------------");
				Console.Write("   Press <Enter> to connect to "); Console.WriteLine(serverUrl);
				Console.ReadLine();
				Console.WriteLine("   Please wait...");

				//OpcBase.ValidateLicense("License Key");

				TsCHdaServer myHdaServer = new TsCHdaServer();

				myHdaServer.Connect(serverUrl);

				Console.WriteLine("   Connected, press <Enter> to add a trend.");
				Console.ReadLine();

                // Add a trend and set the properties for reading
                TsCHdaTrend trend = new TsCHdaTrend(myHdaServer) { StartTime = new TsCHdaTime(new DateTime(2004, 01, 01, 00, 00, 00)), EndTime = new TsCHdaTime(new DateTime(2004, 01, 01, 06, 00, 00)), IncludeBounds = true, MaxValues = 1000 };

				OpcItem itemID = new OpcItem("Static Data/Ramp [15 min]");

                trend.Timestamps.Add(new DateTime(2016, 01, 01, 00, 00, 00));
                OpcItemResult[] results = null;
				IOpcRequest request = null;

				results = trend.ReadRaw(new TsCHdaItem[] { trend.AddItem(itemID) }, null, new TsCHdaReadValuesCompleteEventHandler(OnReadComplete), out request);

				// read the historic data of the specified item
				TsCHdaItemValueCollection[] items = trend.ReadRaw(new TsCHdaItem[] { trend.AddItem(itemID) });
				foreach (TsCHdaItemValueCollection item in items)
				{
					Console.WriteLine(String.Format("{0}", item.ItemName));

					foreach (TsCHdaItemValue val in item)
					{
						if (((int)val.Quality.GetCode() & (int)TsDaQualityMasks.QualityMask) != (int)TsDaQualityBits.Good)
							Console.WriteLine(string.Format("      {0}, {1}", val.Timestamp, val.Quality));
						else
							Console.WriteLine(string.Format("      {0}, {1}", val.Timestamp, val.Value.ToString()));
					}
				}

                trend.Timestamps.Add(new DateTime(2016, 01, 01, 00, 00, 00));
                items = trend.ReadAtTime(new TsCHdaItem[] { trend.AddItem(itemID) });
                foreach (TsCHdaItemValueCollection item in items)
                {
                    Console.WriteLine(String.Format("{0}", item.ItemName));


                    foreach (TsCHdaItemValue val in item)
                    {
                        if (((int)val.Quality.GetCode() & (int)TsDaQualityMasks.QualityMask) != (int)TsDaQualityBits.Good)
                            Console.WriteLine(string.Format("      {0}, {1}", val.Timestamp, val.Quality));
                        else
                            Console.WriteLine(string.Format("      {0}, {1}", val.Timestamp, val.Value.ToString()));
                    }
                }
                Console.WriteLine("   Historical Data Trend read, press <Enter> to disconnect from the server.");
				myHdaServer.Disconnect(); 
                myHdaServer.Dispose();
				Console.ReadLine();
				Console.WriteLine("   Disconnected from the server.");
				Console.WriteLine();

			}
			catch (OpcResultException e)
			{
				Console.WriteLine("   " + e.Message);
				Console.ReadLine();
				return;
			}
			catch (Exception e)
			{
				Console.WriteLine("   " + e.Message);
				Console.ReadLine();
				return;
			}
		}

        #endregion

        ///////////////////////////////////////////////////////////////////////
		#region Main Entry

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ConsoleApplication myClass = new ConsoleApplication();

			myClass.DoOPCCalls();
		}

		#endregion
	}
}

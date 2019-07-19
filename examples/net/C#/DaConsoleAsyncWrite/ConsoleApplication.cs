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
using OpcClientSdk.Da;
#endregion

namespace DaConsoleAsyncWrite
{

	/// <summary>
	/// Simple OPC DA Client Application
	/// </summary>
	class ConsoleApplication
	{

        ///////////////////////////////////////////////////////////////////////
        #region Event Handlers
        /// <summary>
		/// A delegate to receive data change updates from the server.
		/// </summary>
		/// <param name="subscriptionHandle">
		/// A unique identifier for the subscription assigned by the client. If the parameter
		///	<see cref="TsCDaSubscriptionState.ClientHandle">ClientHandle</see> is not defined this
		///	parameter is empty.</param>
		/// <param name="requestHandle">
		///	An identifier for the request assigned by the caller. This parameter is empty if
		///	the	corresponding parameter	in the calls Read(), Write() or Refresh() is not	defined.
		///	Can	be used	to Cancel an outstanding operation.
		///	</param>
		/// <param name="values">
		///	<para class="MsoBodyText" style="MARGIN: 1pt 0in">The set of changed values.</para>
		///	<para class="MsoBodyText" style="MARGIN: 1pt 0in">Each value will always have
		///	item’s ClientHandle field specified.</para>
		/// </param>
		public void OnDataChangedEvent( object subscriptionHandle, object requestHandle, TsCDaItemValueResult[] values)  
		{
			if (requestHandle != null)
			{
				Console.Write("DataChange() for requestHandle :"); Console.WriteLine(requestHandle.GetHashCode().ToString()); 
			}
			else
			{
			        Console.WriteLine("DataChange():"); 
			}
			for (int i = 0; i < values.GetLength(0); i++)
			{
				Console.Write("Client Handle : ");     Console.WriteLine(values[i].ClientHandle);      
				if (values[i].Result.IsSuccess()) 
				{
                    if ( values[i].Value.GetType().IsArray )
                    {
                        UInt16[] arrValue = (UInt16[])values[i].Value;
                        for (int j = 0; j < arrValue.GetLength(0); j++)
                        {
                            Console.Write(String.Format("Value[{0}]      : ", j)); Console.WriteLine(arrValue[j]);
                        }
                    }
                    else
                    {
                        TsCDaItemValueResult valueResult = (TsCDaItemValueResult)values[i];
                        TsCDaQuality quality = new TsCDaQuality(193);
                        valueResult.Quality = quality;
                        string message = String.Format("\r\n\tQuality: is not good : {0} Code:{1} LimitBits: {2} QualityBits: {3} VendorBits: {4}", valueResult.Quality, valueResult.Quality.GetCode(), valueResult.Quality.LimitBits, valueResult.Quality.QualityBits, valueResult.Quality.VendorBits);
                        if (valueResult.Quality.QualityBits != TsDaQualityBits.Good && valueResult.Quality.QualityBits != TsDaQualityBits.GoodLocalOverride)
                        {
                            Console.WriteLine(message);
                        }

					    Console.Write("Value         : ");  Console.WriteLine(values[i].Value);
                    }
					Console.Write("Time Stamp    : ");  Console.WriteLine(values[i].Timestamp.ToString());
					Console.Write("Quality       : ");  Console.WriteLine(values[i].Quality);
				}
				Console.Write("Result        : ");     Console.WriteLine(values[i].Result.Description());
			}
			Console.WriteLine();
			Console.WriteLine();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientHandle"></param>
		/// <param name="results"></param>
		public void OnReadCompleteEvent(object clientHandle, TsCDaItemValueResult[] results)
		{
			Console.WriteLine("Read completed");
			foreach (TsCDaItemValueResult readResult in results)
			{
				Console.Write("Item Name     : ");  Console.WriteLine(readResult.ItemName);
				Console.Write("Value         : ");  Console.WriteLine(readResult.Value);
				Console.Write("Time Stamp    : ");  Console.WriteLine(readResult.Timestamp.ToString());
				Console.Write("Quality       : ");  Console.WriteLine(readResult.Quality);

			}
			Console.WriteLine();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientHandle"></param>
		/// <param name="results"></param>
		public void OnWriteCompleteEvent(object clientHandle, OpcItemResult[] results)
		{
			foreach (OpcItemResult res in results)
			{
			}
        }

        public void OnCancelCompleteEvent(object requestHandle)
        {
            Console.WriteLine("Transaction successfully cancelled");
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region OPC Sample Functionality

        void DoOPCCalls()
		{
			try
			{

                const string serverUrl = "opcda://localhost/Technosoftware.DaSample";

                Console.WriteLine();
                Console.WriteLine("Simple OPC DA Client based on the OPC DA/AE/HDA Client SDK .NET");
                Console.WriteLine("--------------------------------------------------------------");
                Console.Write("   Press <Enter> to connect to "); Console.WriteLine(serverUrl);
                Console.ReadLine();
                Console.WriteLine("   Please wait...");

                //OpcBase.ValidateLicense("License Key");
                TsCDaServer myDaServer = new TsCDaServer();

                // Connect to the server
                myDaServer.Connect(serverUrl);

                OpcServerStatus status = myDaServer.GetServerStatus();

				Console.WriteLine("   Connected, press <Enter> to create an active group object and add several items.");
				Console.ReadLine();

				// Add a group with default values Active = true and UpdateRate = 500ms
				TsCDaSubscription group;
                TsCDaSubscriptionState groupState = new TsCDaSubscriptionState { Name = "MyGroup" /* Group Name*/ };
				group = (TsCDaSubscription) myDaServer.CreateSubscription(groupState);

				// Add Items
				TsCDaItem[] items = new TsCDaItem[2];
				TsCDaItemResult[] itemResults;                
                items[0] = new TsCDaItem();
				items[0].ItemName = "SimpleTypes.InOut.Integer";      // Item Name
				items[0].ClientHandle = 100;                          // Client Handle
                items[0].Active = true;
                items[0].ActiveSpecified = true;
				
                items[1] = new TsCDaItem();
				items[1].ItemName = "SimpleTypes.InOut.Short";        // Item Name
				items[1].ClientHandle = 200;                          // Client Handle
                items[1].Active = false;
                items[1].ActiveSpecified = true;
    		
				TsCDaItem[] arAddedItems;
				itemResults = group.AddItems(items);

				for (int i = 0; i < itemResults.GetLength(0); i++)
				{
					if ( itemResults[i].Result.IsError() )
					{
                        Console.WriteLine(String.Format("   Item {0} could not be added to the group", itemResults[i].ItemName));
					}
				}
  				arAddedItems = itemResults;

                OpcItemResult[] res;
                IOpcRequest m_ITRequest;

                TsCDaItemValue[] arItemValues = new TsCDaItemValue[1];
                arItemValues[0] = new TsCDaItemValue();
                arItemValues[0].ClientHandle = 100;
                arItemValues[0].ItemName = "SimpleTypes.InOut.Short";

                int val = 0;
                do
                {
                    arItemValues[0].Value = val;

                    res = group.Write(arItemValues, 321, new TsCDaWriteCompleteEventHandler(OnWriteCompleteEvent), out m_ITRequest);
                    val++;
                } while (val < 1000);

                Console.ReadLine();


				group.Dispose();                                    // optionally, it's not required
				myDaServer.Disconnect();                            // optionally, it's not required
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
			catch(Exception e)
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
		[MTAThread]
		static void Main(string[] args)
		{
            ConsoleApplication myClass = new ConsoleApplication();

			myClass.DoOPCCalls();
        }
        #endregion
    }
}

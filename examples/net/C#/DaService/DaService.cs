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
using System.Diagnostics;
using System.Threading;
using OpcClientSdk;
using OpcClientSdk.Da;
#endregion

namespace DaService
{
	public class Service : System.ServiceProcess.ServiceBase
	{

		private TsCDaServer myDaServer = null;
		private TsCDaSubscription group = null;

		//=====================================================================
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
		public void DataChangeHandler(object subscriptionHandle, object requestHandle, TsCDaItemValueResult[] values)
		{
			if (requestHandle != null)
			{
				Console.WriteLine("DataChange() for requestHandle :"); Console.WriteLine(requestHandle.GetHashCode().ToString());
			}
			else
			{
				Console.WriteLine("DataChange():");
			}
			for (int i = 0; i < values.GetLength(0); i++)
			{
				Console.WriteLine("Client Handle : " + values[i].ClientHandle.ToString());
				if (values[i].Result.IsSuccess())
				{
					Console.WriteLine("Value         : " + values[i].Value.ToString());
					Console.WriteLine("Time Stamp    : " + values[i].Timestamp.ToString());
					Console.WriteLine("Quality       : " + values[i].Quality.ToString());
				}
				Console.WriteLine("Result        : " + values[i].Result.Description());
			}
		}

		#endregion


		//=====================================================================
		#region Main Entry
		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;

			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}
		#endregion

		#region Visual C# Designer
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// EMailService
			// 
			this.ServiceName = "Technosoftware EMail Service";
		}
		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Service Components
		public Service()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			const string serverName = "opcda://localhost/Technosoftware.DaSample";

			try
			{
				myDaServer = new TsCDaServer();

				myDaServer.Connect(serverName);
                OpcServerStatus status = myDaServer.GetServerStatus();

				// Add a group with default values Active = true and UpdateRate = 500ms
				TsCDaSubscriptionState groupState = new TsCDaSubscriptionState();
				groupState.Name = "MyGroup";                          // Group Name
				group = (TsCDaSubscription)myDaServer.CreateSubscription(groupState);

				// Add Items
				TsCDaItem[] items = new TsCDaItem[3];
				TsCDaItemResult[] itemResults;
				items[0] = new TsCDaItem();
				items[0].ItemName = "SimulatedData.Ramp";             // Item Name
				items[0].ClientHandle = 100;                          // Client Handle
				items[0].MaxAgeSpecified = true;
				items[0].MaxAge = Int32.MaxValue;
				items[1] = new TsCDaItem();
				items[1].ItemName = "SimulatedData.Random";           // Item Name
				items[1].ClientHandle = 150;                          // Client Handle
				items[2] = new TsCDaItem();
				items[2].ItemName = "InOut_I4";                       // Item Name
				items[2].ClientHandle = 200;                          // Client Handle

				TsCDaItem[] arAddedItems;
				itemResults = group.AddItems(items);

				for (int i = 0; i < itemResults.GetLength(0); i++)
				{
					if (itemResults[i].Result.IsError())
					{
						Console.WriteLine("   Item " + itemResults[i].ItemName + "could not be added to the group");
					}
				}

				arAddedItems = itemResults;

				group.DataChangedEvent += new TsCDaDataChangedEventHandler(DataChangeHandler);
				Console.WriteLine("Wait 5 seconds ...");
				//System.Threading.Thread.Sleep(5000);
			}
			catch (OpcResultException e)
			{
				Console.WriteLine("   " + e.Message);
				return;
			}
			catch (Exception e)
			{
				Console.WriteLine("   " + e.Message);
				return;
			}

		}


		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			try
			{
				Console.WriteLine("Remove Groups ...");
				if (group != null)
				{
					group.Dispose();
					group = null;
				}
				Console.WriteLine("Disconnect from Server ...");
				if (myDaServer != null)
				{
					myDaServer.Disconnect();
					myDaServer = null;
				}
				Console.WriteLine("Done.");

			}
			catch (OpcResultException e)
			{
				Console.WriteLine("   " + e.Message);
				return;
			}
			catch (Exception e)
			{
				Console.WriteLine("   " + e.Message);
				return;
			}
		}
		#endregion

		#region Public Members
		public EventLog ServiceEventLog
		{
			get { return m_serviceEventLog; }
			set { m_serviceEventLog = value; }
		}
		public Mutex DbMutex
		{
			get { return m_DbMutex; }
		}
		public System.Data.DataTable SignalListe;
		public System.Data.DataTable EMailKonfiguration;

		public DateTime LastGoodUpdate;

		#endregion

		#region Private Members

		private System.Diagnostics.EventLog m_serviceEventLog;

		private Mutex m_DbMutex = null;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
	}

	class ServiceDefinition
	{
		static public string AgentServiceName = "Technosoftware Service";
		static public string AgentServiceVersion = "";
	}
}

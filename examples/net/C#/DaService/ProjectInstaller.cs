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
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
#endregion

namespace DaService
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
		private System.ServiceProcess.ServiceInstaller serviceInstaller1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProjectInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			AfterInstall +=new InstallEventHandler(ProjectInstaller_AfterInstall);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstaller1
			//       
			// The services run under the system account.
			this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;

			//this.serviceProcessInstaller1.Password = null;
			//this.serviceProcessInstaller1.Username = null;
			// 
			// serviceInstaller1
			// 
			this.serviceInstaller1.DisplayName = DaService.ServiceDefinition.AgentServiceName + DaService.ServiceDefinition.AgentServiceVersion;
			this.serviceInstaller1.ServiceName = DaService.ServiceDefinition.AgentServiceName + DaService.ServiceDefinition.AgentServiceVersion;

			this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
																					  this.serviceProcessInstaller1,
																					  this.serviceInstaller1});

		}
		#endregion

		private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
		{
			SetServiceDescription();
		}

		public void SetServiceDescription()
		{
			Microsoft.Win32.RegistryKey rgkSystem;
			Microsoft.Win32.RegistryKey rgkCurrentControlSet;
			Microsoft.Win32.RegistryKey rgkServices;
			Microsoft.Win32.RegistryKey rgkService;
			Microsoft.Win32.RegistryKey rgkConfig;

			//Open the HKEY_LOCAL_MACHINE\SYSTEM key
			rgkSystem = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
			//Open CurrentControlSet
			rgkCurrentControlSet = rgkSystem.OpenSubKey("CurrentControlSet");
			//Go to the services key
			rgkServices = rgkCurrentControlSet.OpenSubKey("Services");
			//Open the key for your service, and allow writing
												//******!!!!!! SERVICE NAME !!!!!!******
			rgkService = rgkServices.OpenSubKey(DaService.ServiceDefinition.AgentServiceName + DaService.ServiceDefinition.AgentServiceVersion, true);
			//Add your service's description as a REG_SZ value named "Description"
												//******!!!!!! SERVICE DESCRIPTION !!!!!!******'
			rgkService.SetValue("Description", "Sample OPC DA Client based on .NET");
			//(Optional) Add some custom information your service will use...
			rgkConfig = rgkService.CreateSubKey("Parameters");
		}
	}
}

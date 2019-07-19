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
using System.Collections.Generic;
using System.Windows.Forms;
using OpcClientSdk;
using OpcClientSdk.Da;
#endregion

namespace DaSimpleSync
{
	/// <summary>
	/// Zusammenfassung für frmMain.
	/// </summary>
	public class frmMain : Form
	{
		private Button btnConnect;
		private Button btnAddItem;
		private Button btnRead;
		private Button btnWrite;
		private Button btnDone;
		private ComboBox CboxListServer;
		private TextBox txtboxOpcItem;
		private RichTextBox txtboxRead;
		private TextBox txtboxWrite;
		private GroupBox GboxSplit2;
		private GroupBox GboxSplit1;
		private Label LOpcServer;
		private Label LOpcItem;

		//----------------------------------------------------------------------------------------------------------------------
		// Member Variables
		//----------------------------------------------------------------------------------------------------------------------
		
		private TsCDaServer m_OpcDaServer = new TsCDaServer();
		private TsCDaSubscription m_pOpcGroup;
		private TsCDaItem[] m_arAddedItems;

        private string m_sDefaultServer = "Technosoftware.DaSample";
        private string m_sDefaultItem = "CTT.SimpleTypes.InOut.Integer";

		private SubclassedSystemMenu mobjSubclassedSystemMenu ;

		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			btnConnect = new System.Windows.Forms.Button();
			btnAddItem = new System.Windows.Forms.Button();
			btnRead = new System.Windows.Forms.Button();
			btnWrite = new System.Windows.Forms.Button();
			btnDone = new System.Windows.Forms.Button();
			LOpcServer = new System.Windows.Forms.Label();
			LOpcItem = new System.Windows.Forms.Label();
			CboxListServer = new System.Windows.Forms.ComboBox();
			txtboxOpcItem = new System.Windows.Forms.TextBox();
			txtboxRead = new System.Windows.Forms.RichTextBox();
			txtboxWrite = new System.Windows.Forms.TextBox();
			GboxSplit2 = new System.Windows.Forms.GroupBox();
			GboxSplit1 = new System.Windows.Forms.GroupBox();
			SuspendLayout();
			// 
			// btnConnect
			// 
			btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			btnConnect.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnConnect.Location = new System.Drawing.Point(104, 43);
			btnConnect.Name = "btnConnect";
			btnConnect.TabIndex = 0;
			btnConnect.Text = "&Connect";
			btnConnect.Click += new System.EventHandler(btnConnect_Click);
			// 
			// btnAddItem
			// 
			btnAddItem.Enabled = false;
			btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			btnAddItem.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnAddItem.Location = new System.Drawing.Point(104, 108);
			btnAddItem.Name = "btnAddItem";
			btnAddItem.TabIndex = 1;
			btnAddItem.Text = "&Add Item";
			btnAddItem.Click += new System.EventHandler(btnAddItem_Click);
			// 
			// btnRead
			// 
			btnRead.Enabled = false;
			btnRead.FlatStyle = System.Windows.Forms.FlatStyle.System;
			btnRead.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnRead.Location = new System.Drawing.Point(8, 168);
			btnRead.Name = "btnRead";
			btnRead.TabIndex = 2;
			btnRead.Text = "&Read";
			btnRead.Click += new System.EventHandler(btnRead_Click);
			// 
			// btnWrite
			// 
			btnWrite.Enabled = false;
			btnWrite.FlatStyle = System.Windows.Forms.FlatStyle.System;
			btnWrite.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnWrite.Location = new System.Drawing.Point(8, 235);
			btnWrite.Name = "btnWrite";
			btnWrite.TabIndex = 3;
			btnWrite.Text = "&Write";
			btnWrite.Click += new System.EventHandler(btnWrite_Click);
			// 
			// btnDone
			// 
			btnDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			btnDone.Font = new System.Drawing.Font("Tahoma", 8.25F);
			btnDone.Location = new System.Drawing.Point(192, 304);
			btnDone.Name = "btnDone";
			btnDone.TabIndex = 4;
			btnDone.Text = "&Done";
			btnDone.Click += new System.EventHandler(btnDone_Click);
			// 
			// LOpcServer
			// 
			LOpcServer.Font = new System.Drawing.Font("Tahoma", 8.25F);
			LOpcServer.Location = new System.Drawing.Point(8, 16);
			LOpcServer.Name = "LOpcServer";
			LOpcServer.Size = new System.Drawing.Size(96, 32);
			LOpcServer.TabIndex = 5;
			LOpcServer.Text = "OPC DA 2.0/3.0 Server";
			// 
			// LOpcItem
			// 
			LOpcItem.Font = new System.Drawing.Font("Tahoma", 8.25F);
			LOpcItem.Location = new System.Drawing.Point(8, 80);
			LOpcItem.Name = "LOpcItem";
			LOpcItem.Size = new System.Drawing.Size(88, 16);
			LOpcItem.TabIndex = 6;
			LOpcItem.Text = "OPC Item ID";
			// 
			// CboxListServer
			// 
			CboxListServer.Font = new System.Drawing.Font("Tahoma", 8.25F);
			CboxListServer.Location = new System.Drawing.Point(104, 16);
			CboxListServer.Name = "CboxListServer";
			CboxListServer.Size = new System.Drawing.Size(328, 21);
			CboxListServer.TabIndex = 7;
			// 
			// txtboxOpcItem
			// 
			txtboxOpcItem.Enabled = false;
			txtboxOpcItem.Font = new System.Drawing.Font("Tahoma", 8.25F);
			txtboxOpcItem.Location = new System.Drawing.Point(104, 80);
			txtboxOpcItem.Name = "txtboxOpcItem";
			txtboxOpcItem.Size = new System.Drawing.Size(328, 21);
			txtboxOpcItem.TabIndex = 8;
			txtboxOpcItem.Text = "";
			// 
			// txtboxRead
			// 
			txtboxRead.BackColor = System.Drawing.SystemColors.Control;
			txtboxRead.Font = new System.Drawing.Font("Tahoma", 8.25F);
			txtboxRead.Location = new System.Drawing.Point(104, 168);
			txtboxRead.Name = "txtboxRead";
			txtboxRead.ReadOnly = true;
			txtboxRead.Size = new System.Drawing.Size(328, 48);
			txtboxRead.TabIndex = 9;
			txtboxRead.Text = "";
			// 
			// txtboxWrite
			// 
			txtboxWrite.Enabled = false;
			txtboxWrite.Font = new System.Drawing.Font("Tahoma", 8.25F);
			txtboxWrite.Location = new System.Drawing.Point(104, 235);
			txtboxWrite.Name = "txtboxWrite";
			txtboxWrite.Size = new System.Drawing.Size(328, 21);
			txtboxWrite.TabIndex = 9;
			txtboxWrite.Text = "";
			// 
			// GboxSplit2
			// 
			GboxSplit2.Location = new System.Drawing.Point(8, 271);
			GboxSplit2.Name = "GboxSplit2";
			GboxSplit2.Size = new System.Drawing.Size(424, 8);
			GboxSplit2.TabIndex = 10;
			GboxSplit2.TabStop = false;
			// 
			// GboxSplit1
			// 
			GboxSplit1.Location = new System.Drawing.Point(8, 144);
			GboxSplit1.Name = "GboxSplit1";
			GboxSplit1.Size = new System.Drawing.Size(424, 8);
			GboxSplit1.TabIndex = 10;
			GboxSplit1.TabStop = false;
			// 
			// frmMain
			// 
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			ClientSize = new System.Drawing.Size(442, 342);
			Controls.Add(GboxSplit2);
			Controls.Add(txtboxRead);
			Controls.Add(txtboxOpcItem);
			Controls.Add(CboxListServer);
			Controls.Add(LOpcItem);
			Controls.Add(LOpcServer);
			Controls.Add(btnDone);
			Controls.Add(btnWrite);
			Controls.Add(btnRead);
			Controls.Add(btnAddItem);
			Controls.Add(btnConnect);
			Controls.Add(txtboxWrite);
			Controls.Add(GboxSplit1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmMain";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "TsOpc - Simple OPC DA Synchronous Read & Write Sample";
			Load += new System.EventHandler(frmMain_Load);
			ResumeLayout(false);
			
		}
		#endregion

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try 
			{
				/// WindowsXP New Style 
				Application.EnableVisualStyles();
				Application.DoEvents();
				Application.Run(new frmMain());
			}
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK ,MessageBoxIcon.Warning);
			}

		}


		//----------------------------------------------------------------------------------------------------------------------
		// frmMainmobjSubclassedSystemMenu_LaunchDialog_Load
		// -------------------------------------------------
		// This method open frmAbout -> SubClassedSystemMenu
		//----------------------------------------------------------------------------------------------------------------------
		private void mobjSubclassedSystemMenu_LaunchDialog(object sender, System.EventArgs e)
		{
		}


		//----------------------------------------------------------------------------------------------------------------------
		// frmMain_Load
		// -----------------
		// This method add About OPC... Menu and Remove unnecessary Menu Item
		// CboxListServer list all DA Server and set the correct Default or Item Value
		//----------------------------------------------------------------------------------------------------------------------
		private void frmMain_Load(object sender, System.EventArgs e)
		{
			mobjSubclassedSystemMenu = new SubclassedSystemMenu(Handle.ToInt32(), "&About TsOpcDaNetSimpleSync...");
			mobjSubclassedSystemMenu.RemoveMenus(this, true, false, true, true, true, true, false);
			mobjSubclassedSystemMenu.LaunchDialog += new System.EventHandler(mobjSubclassedSystemMenu_LaunchDialog);

			txtboxWrite.Text = "100";

            OpcResult res;

            List<OpcServer> servers = OpcDiscovery.GetServers(OpcSpecification.OPC_DA_20);

            if (servers != null && servers.Count > 0)
            {
                CboxListServer.Text = m_sDefaultServer;
                int i = 0;
                foreach (OpcServer server in servers)
                {
                    CboxListServer.Items.Add(server.ServerName);
                    if (server.ServerName == m_sDefaultServer)
                    {
                        CboxListServer.SelectedIndex = i;
                    }
                    i++;
                }
            }
		
			if (CboxListServer.SelectedIndex == -1 & !(CboxListServer.Items.Count == 0)) 
			{
				CboxListServer.SelectedIndex = 0;
			}
		}


		//----------------------------------------------------------------------------------------------------------------------
		// btnConnect_Click
		//-----------------------
		// This method tries to connect to the specified OPC Server.
		// If connected successfully, this method also registers the name of the client with the server and
		// adds an OPC Group.
		//----------------------------------------------------------------------------------------------------------------------
		private void btnConnect_Click(object sender, System.EventArgs e)
		{	            						
			try
			{
				Cursor = Cursors.WaitCursor;
					
				/// Check the correct sDefaultItem 
				if (CboxListServer.Text == m_sDefaultServer) 
				{
					txtboxOpcItem.Text = m_sDefaultItem;
				} 
				else 
				{
					txtboxOpcItem.Text = null;
				}

                OpcUrl opcUrl = new OpcUrl(OpcSpecification.OPC_DA_20, OpcUrlScheme.DA, CboxListServer.Text);
                m_OpcDaServer.Connect(opcUrl, null);	/// Connect now with Server
				Cursor = Cursors.Default;							/// Set default cursor

				/// All succeeded, update buttons and text fields
				CboxListServer.Enabled = false;
				btnConnect.Enabled = false;
				txtboxOpcItem.Enabled = true;
				btnAddItem.Enabled = true;
			}
			catch(OpcResultException exe)
			{
				Cursor = Cursors.Default;							/// Set default cursor
				MessageBox.Show(exe.Message,exe.Source, MessageBoxButtons.OK ,MessageBoxIcon.Warning);
			}
			catch(Exception ex) 
			{
				Cursor = Cursors.Default;							/// Set default cursor
				MessageBox.Show(ex.Message,ex.Source, MessageBoxButtons.OK ,MessageBoxIcon.Warning);
			}
		}


		//----------------------------------------------------------------------------------------------------------------------
		// btnAddItem_Click
		//--------------------
		// This method tries to add the specified item to the preceding added group object.
		//----------------------------------------------------------------------------------------------------------------------
		private void btnAddItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				TsCDaSubscriptionState groupState = new TsCDaSubscriptionState();
				groupState.Name = "MyGroup";                          // Group Name
				m_pOpcGroup = (TsCDaSubscription) m_OpcDaServer.CreateSubscription(groupState);

				TsCDaItemResult[] res;
				TsCDaItem[] items = new TsCDaItem[1];
				items[0] = new TsCDaItem();
				items[0].ItemName = txtboxOpcItem.Text;           // Item Name
				items[0].ClientHandle = 100;                          // Client Handle

				res = m_pOpcGroup.AddItems(items);
					
				if (res[0].Result.IsSuccess())
				{
					if (!(res[0].Result.IsOk()))					// Note: Since this sample adds only one item it's required that AddItems()
					{
						return;										// succeeds for all specified items (in this case only one).
					}
					else
					{
						m_arAddedItems = res;
					}
				}else{
					MessageBox.Show("AddItems() method failed: " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


				/// All succeeded, update buttons and text fields
				txtboxOpcItem.Enabled = false;
				btnAddItem.Enabled = false;
				btnRead.Enabled = true;
				btnWrite.Enabled = true;
				txtboxWrite.Enabled = true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}

		}


		//----------------------------------------------------------------------------------------------------------------------
		// btnRead_Click
		//-------------------
		// A read operation is called for the added item.
		//----------------------------------------------------------------------------------------------------------------------
		private void btnRead_Click(object sender, System.EventArgs e)
		{
			TsCDaItemValueResult[] res;
			
			res = m_pOpcGroup.Read(m_arAddedItems);
                        
			if (res[0].Result.IsError())
			{
				MessageBox.Show("Read operation failed: " + res[0].Result.Description(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			} else {
				
				if (res[0].Result.IsSuccess())
				{
					/// Display the read results
					string sReadResult;
					sReadResult = "Value: \t\t" + res[0].Value +"\n"+
					"Quality: \t\t" + res[0].Quality +"\n"+
					"TimeStamp: \t" + res[0].Timestamp;
					txtboxRead.Text = sReadResult;
				} else {  
					MessageBox.Show("Cannot read value : " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
			}
		}


		//----------------------------------------------------------------------------------------------------------------------
		// btnWrite_Click
		//-------------------
		// A write operation with the specified value is called for the added item.
		//----------------------------------------------------------------------------------------------------------------------
		private void btnWrite_Click(object sender, System.EventArgs e)
		{
		OpcItemResult[] res;
        TsCDaItemValue[] WriteItem = new TsCDaItemValue[1];
		WriteItem[0] = new TsCDaItemValue();
		WriteItem[0].ItemName = m_arAddedItems[0].ItemName;
        WriteItem[0].ServerHandle = m_arAddedItems[0].ServerHandle;
        WriteItem[0].Value = txtboxWrite.Text;

        Cursor = Cursors.WaitCursor; 
        res = m_pOpcGroup.Write(WriteItem);

		if (res[0].Result.IsError())
		{
			MessageBox.Show("Write operation failed: " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		} else {
				if (!(res[0].Result.IsOk()))
				{
					MessageBox.Show("Cannot write value : " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				} else {
					double uDefaultValue = System.Convert.ToDouble(txtboxWrite.Text);
					txtboxWrite.Text = System.Convert.ToString(uDefaultValue + 1);
				}
			}
		}
		

		//----------------------------------------------------------------------------------------------------------------------
		// btnDone_Click
		//---------------------
		// Close Form frmMain
		//----------------------------------------------------------------------------------------------------------------------
		private void btnDone_Click(object sender, System.EventArgs e)
		{
			Close();
		}

	}
}

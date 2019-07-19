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

namespace DaSimpleAsync
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
        private Button btnRefresh;
        private Button btnCancel;
        private CheckBox chboxNotifications;

        //----------------------------------------------------------------------------------------------------------------------
        // Member Variables
        //----------------------------------------------------------------------------------------------------------------------

        private TsCDaServer m_OpcDaServer = new TsCDaServer();
        private TsCDaSubscription m_pOpcGroup;
        private TsCDaItem[] m_arAddedItems;
        private IOpcRequest m_ITRequest;

        private string m_sDefaultServer = "Technosoftware.DaSample";
        private string m_sDefaultItem = "SimulatedData.Ramp";

        private SubclassedSystemMenu mobjSubclassedSystemMenu;

        private delegate void addTextFromThreadDelegate(string name);

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.Container components;

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
            btnRefresh = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            chboxNotifications = new System.Windows.Forms.CheckBox();
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
            btnWrite.Location = new System.Drawing.Point(8, 264);
            btnWrite.Name = "btnWrite";
            btnWrite.TabIndex = 3;
            btnWrite.Text = "&Write";
            btnWrite.Click += new System.EventHandler(btnWrite_Click);
            // 
            // btnDone
            // 
            btnDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btnDone.Font = new System.Drawing.Font("Tahoma", 8.25F);
            btnDone.Location = new System.Drawing.Point(192, 328);
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
            txtboxWrite.Location = new System.Drawing.Point(104, 264);
            txtboxWrite.Name = "txtboxWrite";
            txtboxWrite.Size = new System.Drawing.Size(328, 21);
            txtboxWrite.TabIndex = 9;
            txtboxWrite.Text = "";
            // 
            // GboxSplit2
            // 
            GboxSplit2.Location = new System.Drawing.Point(8, 296);
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
            // btnRefresh
            // 
            btnRefresh.Enabled = false;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btnRefresh.Font = new System.Drawing.Font("Tahoma", 8.25F);
            btnRefresh.Location = new System.Drawing.Point(8, 200);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "R&efresh";
            btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            btnCancel.Location = new System.Drawing.Point(8, 232);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 12;
            btnCancel.Text = "&Cancel";
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // chboxNotifications
            // 
            chboxNotifications.Checked = true;
            chboxNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            chboxNotifications.Enabled = false;
            chboxNotifications.FlatStyle = System.Windows.Forms.FlatStyle.System;
            chboxNotifications.Font = new System.Drawing.Font("Tahoma", 8.25F);
            chboxNotifications.Location = new System.Drawing.Point(104, 224);
            chboxNotifications.Name = "chboxNotifications";
            chboxNotifications.Size = new System.Drawing.Size(328, 24);
            chboxNotifications.TabIndex = 13;
            chboxNotifications.Text = "Data Change Notifications enabled";
            chboxNotifications.CheckedChanged += new System.EventHandler(chboxNotifications_CheckedChanged);
            // 
            // frmMain
            // 
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(444, 368);
            Controls.Add(chboxNotifications);
            Controls.Add(btnCancel);
            Controls.Add(btnRefresh);
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
            Text = "TsOpc - Simple OPC DA Asynchronous Read & Write Sample";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            mobjSubclassedSystemMenu = new SubclassedSystemMenu(Handle.ToInt32(), "&About DaSimpleSync...");
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
            catch (OpcResultException exe)
            {
                Cursor = Cursors.Default;							/// Set default cursor
                MessageBox.Show(exe.Message, exe.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;							/// Set default cursor
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                groupState.ClientHandle = "test";
                groupState.Deadband = 0;
                groupState.UpdateRate = 1000;
                groupState.KeepAlive = 10000;
                m_pOpcGroup = (TsCDaSubscription)m_OpcDaServer.CreateSubscription(groupState);

                TsCDaItemResult[] itemResults;
                TsCDaItem[] items = new TsCDaItem[1];
                items[0] = new TsCDaItem();
                items[0].ItemName = txtboxOpcItem.Text;           // Item Name
                items[0].ClientHandle = 100;                          // Client Handle

                itemResults = m_pOpcGroup.AddItems(items);
                m_arAddedItems = itemResults;

                /// Activate data change subscription
                m_pOpcGroup.DataChangedEvent += new TsCDaDataChangedEventHandler(DataChangeHandler);

                /// All succeeded, update buttons and text fields
                txtboxOpcItem.Enabled = false;
                btnAddItem.Enabled = false;
                btnRead.Enabled = true;
                btnRefresh.Enabled = true;
                btnCancel.Enabled = true;
                btnWrite.Enabled = true;
                txtboxWrite.Enabled = true;
                chboxNotifications.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


        //----------------------------------------------------------------------------------------------------------------------
        // btnRead_Click
        //-------------------
        // A read operation is called for the added item.
        //----------------------------------------------------------------------------------------------------------------------
        private void btnRead_Click(object sender, System.EventArgs e)
        {
            OpcItemResult[] res;
            res = m_pOpcGroup.Read(m_arAddedItems, 100, new TsCDaReadCompleteEventHandler(ReadCompleteHandler), out m_ITRequest);
            if (res[0].Result.IsError())
            {
                MessageBox.Show("Read operation failed: " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        ///---------------------------------------------------------------------------------------------------------------------
        /// btnRefresh_Click
        ///----------------------
        /// Forces a data change notification for all active items. 
        ///---------------------------------------------------------------------------------------------------------------------
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            object requestHandle = 1234;
            IOpcRequest request;

            try
            {
                m_pOpcGroup.Refresh(requestHandle, out request);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Refresh operation failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ///---------------------------------------------------------------------------------------------------------------------
        /// btnCancel_Click
        /// -------------------
        /// Requests the server to cancel an outstanding asynchronous transaction
        ///---------------------------------------------------------------------------------------------------------------------
        private void btnCancel_Click(object sender, System.EventArgs e)
        {

            try
            {
                m_pOpcGroup.Cancel(m_ITRequest, new TsCDaCancelCompleteEventHandler(CancelCompleteHandler));
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is no outstanding asynchronous transaction which can be canceled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //----------------------------------------------------------------------------------------------------------------------
        // btnWrite_Click
        //-------------------
        // A write operation with the specified value is called for the added item.
        //----------------------------------------------------------------------------------------------------------------------
        private void btnWrite_Click(object sender, System.EventArgs e)
        {
            try
            {
                TsCDaItemValue[] writeValues = new TsCDaItemValue[1];
                writeValues[0] = new TsCDaItemValue();

                writeValues[0].ServerHandle = m_arAddedItems[0].ServerHandle;
                writeValues[0].Value = txtboxWrite.Text;

                OpcItemResult[] res;
                res = m_pOpcGroup.Write(writeValues, 321, new TsCDaWriteCompleteEventHandler(WriteCompleteCallback), out m_ITRequest);

                if (res[0].Result.IsError())
                {
                    MessageBox.Show("WriteAsync operation failed: " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (!(res[0].Result.IsSuccess()))
                    {
                        MessageBox.Show("Cannot write value : " + res[0].Result.Description(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        /// All succeeded
                        /// Predefine a new default value for the next write operation
                        double uDefaultValue = System.Convert.ToDouble(txtboxWrite.Text);
                        txtboxWrite.Text = System.Convert.ToString(uDefaultValue + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        ///---------------------------------------------------------------------------------------------------------------------
        /// chboxNotifications_CheckedChanged
        ///-------------------------------------
        /// Enables or disables data change notifications via the Data Change Subscription. 
        ///---------------------------------------------------------------------------------------------------------------------
        private void chboxNotifications_CheckedChanged(object sender, System.EventArgs e)
        {
            bool fEnable = false;

            if (fEnable == chboxNotifications.Checked)
            {
                fEnable = false;
            }
            else
            {
                fEnable = true;
            }

            if (!(null == m_pOpcGroup))
            {
                try
                {
                    m_pOpcGroup.SetEnabled(fEnable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(fEnable + "Enabling:" + "Disabling" + "of Data Change Notifications failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


        ///---------------------------------------------------------------------------------------------------------------------
        /// DataChangeHandler
        ///---------------------
        /// Receives exception based data changes as well as the completion results of asynchronous refresh operations. 
        ///---------------------------------------------------------------------------------------------------------------------

        private void DataChangeHandler(object subscriptionHandle, object requestHandle, TsCDaItemValueResult[] e)
        {
            for (int i = 0; i < e.GetLength(0); i++)
            {
                try
                {
                    string sReadResult;
                    if (e[i].Result.IsSuccess())
                    {
                        sReadResult = "Value: \t\t" + e[0].Value + "\n" +
                        "Quality: \t\t" + e[0].Quality + "\n" +
                        "TimeStamp: \t" + e[0].Timestamp;
                        txtboxRead.Invoke(new addTextFromThreadDelegate(addTextFromThread), new object[] { sReadResult }); // 28.11.2005 FI Änderung von VS/2003 nach VS/2005
                    }
                    else
                    {
                        sReadResult = "Result: \t" + e[i].Result.Description();
                        txtboxRead.Invoke(new addTextFromThreadDelegate(addTextFromThread), new object[] { sReadResult }); // 28.11.2005 FI Änderung von VS/2003 nach VS/2005
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (true)
                {
                    try
                    {
                        MessageBox.Show("Refresh operation complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

        }

        //----------------------------------------------------------------------------------------------------------------
        // addTextFromThread
        // ---------------------
        // Set the string to Me.txtboxRead.Text -> New Visual Studio 2005
        //----------------------------------------------------------------------------------------------------------------
        private void addTextFromThread(string sValue)
        {
            txtboxRead.Text = sValue;
        }

        ///---------------------------------------------------------------------------------------------------------------------
        /// ReadCompleteHandler
        /// ---------------------
        /// Receives the completion results of asynchronous read operations.
        ///---------------------------------------------------------------------------------------------------------------------
        private void ReadCompleteHandler(object requestHandle, TsCDaItemValueResult[] e)
        {
            /// This sample implementation has only one item.
            /// Please see the DataChangeHandler() implementation
            /// above for the handling of several items.

            try
            {
                string sReadResult;
                if (e[0].Result.IsSuccess())
                {
                    sReadResult = "Value: \t\t" + e[0].Value + "\n" +
                    "Quality: \t\t" + e[0].Quality + "\n" +
                    "TimeStamp: \t" + e[0].Timestamp;
                    txtboxRead.Invoke(new addTextFromThreadDelegate(addTextFromThread), new object[] { sReadResult }); // 28.11.2005 FI Änderung von VS/2003 nach VS/2005
                }
                else
                {
                    sReadResult = "Result: \t" + e[0].Result.Description();
                    txtboxRead.Invoke(new addTextFromThreadDelegate(addTextFromThread), new object[] { sReadResult }); // 28.11.2005 FI Änderung von VS/2003 nach VS/2005
                }

                /// Display the 'operation complete' message
                MessageBox.Show("Read operation complete: " + e[0].Result.Description(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        ///---------------------------------------------------------------------------------------------------------------------
        /// WriteCompleteHandler
        /// ---------------------
        /// Receives the completion results of asynchronous write operations.
        ///---------------------------------------------------------------------------------------------------------------------
        private void WriteCompleteCallback(object requestHandle, OpcItemResult[] e)
        {
            /// This sample implementation has only one item.
            /// Please see the DataChangeHandler() implementation
            /// above for the handling of several items.
            try
            {
                MessageBox.Show("Write operation complete: " + e[0].Result.Description(), "WriteComplete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        ///---------------------------------------------------------------------------------------------------------------------
        /// CancelCompleteHandler
        /// ---------------------
        /// Receives the acknowledgement of canceled asynchronous operations.
        ///---------------------------------------------------------------------------------------------------------------------
        private void CancelCompleteHandler(object requestHandle)
        {
            try
            {
                MessageBox.Show("Transaction with ID %ud successfully canceled.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}


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

namespace DaBrowser
{

    public partial class MainForm : Form
    {

        #region Constants

        const string _defaultDiscoveryServer = "http://expressInterface.com/XiDISCO/serverDiscovery";

        #endregion

        #region Fields

        private TsCDaBrowseFilters _filter = new TsCDaBrowseFilters();
        private TsCDaServer _myServer = new TsCDaServer();

        #endregion

        #region Constructors, Destructor, Initialization

        public MainForm()
        {
            InitializeComponent();

        }

        #endregion

        #region Data Grid View Handling

        private void UpdateDataGrid()
        {
            Cursor originalCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            List<OpcServer> opcDa20Servers = OpcDiscovery.GetServers(OpcSpecification.OPC_DA_20);

            // Clear the contents of the datagrid
            _serverGridView.Rows.Clear();

            // Update the datagrid. One row per Xi server
            opcDa20Servers.ForEach(server =>
            {
                object[] values = new object[6];
                values[0] = server.ServerName;
                values[1] = server.Url;
                values[2] = "";
                values[3] = true;
                values[4] = false;
                values[5] = false;
                _serverGridView.Rows.Add(values);
            });

            Cursor = originalCursor; 
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region UI Click event handlers

        private void ButtonDiscover_Click(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void ServerGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Cursor originalCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            _textBoxServerUrl.Text = _serverGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

            Cursor = originalCursor;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!(_myServer == null))
            {
                if (_myServer.IsConnected)
                {
                    _myServer.Disconnect();
                }
            }

            Close();
        }

        //----------------------------------------------------------------------------------------------------------------------
        // btnConnect_Click
        //-----------------------
        // This method tries to connect to the specified OPC Server.
        // If connected successfully, this method also registers the name of the client with the server and
        // adds an OPC Group.
        //----------------------------------------------------------------------------------------------------------------------
        private void btnConnect_Click(object sender, EventArgs e)
        {
            TsCDaBrowseElement[] BrowserElement;
            TsCDaBrowsePosition BrowserPos;
            OpcItem Path = new OpcItem("");

            _filter.BrowseFilter = TsCDaBrowseFilter.All;
            _filter.ReturnAllProperties = true;
            _filter.ReturnPropertyValues = true;

            try
            {
                Cursor = Cursors.WaitCursor;
                if (!_myServer.IsConnected)
                {
                    // No connection to server, connect to it
                    _textBoxServerUrl.Enabled = false;

                    _myServer.Connect(_textBoxServerUrl.Text);	        // Connect now with Server
                    BrowserElement = _myServer.Browse(Path, _filter, out BrowserPos);

                    /// All succeeded, update buttons and text fields
                    _textBoxServerUrl.Enabled = false;
                    _btnConnect.Text = "Disconnect";
                    BrowseCTRL.ShowSingleServer(_myServer, _filter);
                    PropertiesCTRL.Initialize(null);
                }
                else
                {
                    // connection to server exists, disconnect to it
                    _textBoxServerUrl.Enabled = true;

                    BrowseCTRL.Clear();
                    PropertiesCTRL.Initialize(null);
                    _myServer.Disconnect();
                    _btnConnect.Text = "Connect";
                }
                Cursor = Cursors.Default;							// Set default cursor
            }
            catch (OpcResultException exe)
            {
                Cursor = Cursors.Default;							/// Set default cursor
                MessageBox.Show(exe.Message, exe.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _textBoxServerUrl.Enabled = true;
                _btnConnect.Text = "Connect";
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;							/// Set default cursor
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _textBoxServerUrl.Enabled = true;
                _btnConnect.Text = "Connect";
            }
        }

        /// <summary>
        /// Called when a server is picked in the browse control.
        /// </summary>
        private void OnElementSelected(TsCDaBrowseElement element)
        {
            PropertiesCTRL.Initialize(element);
        }
        #endregion

    }
}

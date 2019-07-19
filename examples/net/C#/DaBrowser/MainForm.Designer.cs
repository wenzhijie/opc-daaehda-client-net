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

namespace DaBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._groupBrowse = new System.Windows.Forms.GroupBox();
            this.PropertiesCTRL = new DaBrowser.PropertyListViewCtrl();
            this.BrowseCTRL = new DaBrowser.BrowseTreeCtrl();
            this._btnConnect = new System.Windows.Forms.Button();
            this._groupDiscovery = new System.Windows.Forms.GroupBox();
            this._serverGridView = new System.Windows.Forms.DataGridView();
            this._serverName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._daSupported = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._aeSupported = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._hdaSupported = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._buttonDiscover = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this._labelServerUrl = new System.Windows.Forms.Label();
            this._textBoxServerUrl = new System.Windows.Forms.TextBox();
            this._groupBrowse.SuspendLayout();
            this._groupDiscovery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._serverGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _groupBrowse
            // 
            this._groupBrowse.Controls.Add(this.PropertiesCTRL);
            this._groupBrowse.Controls.Add(this.BrowseCTRL);
            this._groupBrowse.Location = new System.Drawing.Point(12, 314);
            this._groupBrowse.Name = "_groupBrowse";
            this._groupBrowse.Size = new System.Drawing.Size(805, 243);
            this._groupBrowse.TabIndex = 21;
            this._groupBrowse.TabStop = false;
            this._groupBrowse.Text = "Browse Address Space";
            // 
            // PropertiesCTRL
            // 
            this.PropertiesCTRL.AllowDrop = true;
            this.PropertiesCTRL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertiesCTRL.Location = new System.Drawing.Point(287, 19);
            this.PropertiesCTRL.Name = "PropertiesCTRL";
            this.PropertiesCTRL.Size = new System.Drawing.Size(512, 218);
            this.PropertiesCTRL.TabIndex = 14;
            // 
            // BrowseCTRL
            // 
            this.BrowseCTRL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BrowseCTRL.Location = new System.Drawing.Point(6, 19);
            this.BrowseCTRL.Name = "BrowseCTRL";
            this.BrowseCTRL.Size = new System.Drawing.Size(275, 218);
            this.BrowseCTRL.TabIndex = 13;
            this.BrowseCTRL.ElementSelected += new DaBrowser.ElementSelected_EventHandler(this.OnElementSelected);
            // 
            // _btnConnect
            // 
            this._btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._btnConnect.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._btnConnect.Location = new System.Drawing.Point(742, 288);
            this._btnConnect.Name = "_btnConnect";
            this._btnConnect.Size = new System.Drawing.Size(75, 25);
            this._btnConnect.TabIndex = 16;
            this._btnConnect.Text = "&Connect";
            this._btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // _groupDiscovery
            // 
            this._groupDiscovery.Controls.Add(this._serverGridView);
            this._groupDiscovery.Controls.Add(this._buttonDiscover);
            this._groupDiscovery.Location = new System.Drawing.Point(12, 11);
            this._groupDiscovery.Name = "_groupDiscovery";
            this._groupDiscovery.Size = new System.Drawing.Size(808, 273);
            this._groupDiscovery.TabIndex = 20;
            this._groupDiscovery.TabStop = false;
            this._groupDiscovery.Text = "Discover Servers";
            // 
            // _serverGridView
            // 
            this._serverGridView.AllowUserToAddRows = false;
            this._serverGridView.AllowUserToDeleteRows = false;
            this._serverGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._serverGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._serverGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._serverName,
            this._url,
            this._vendor,
            this._daSupported,
            this._aeSupported,
            this._hdaSupported});
            this._serverGridView.Location = new System.Drawing.Point(7, 50);
            this._serverGridView.Name = "_serverGridView";
            this._serverGridView.ReadOnly = true;
            this._serverGridView.RowHeadersVisible = false;
            this._serverGridView.Size = new System.Drawing.Size(795, 217);
            this._serverGridView.TabIndex = 1;
            this._serverGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ServerGridView_RowEnter);
            // 
            // _serverName
            // 
            this._serverName.FillWeight = 0.4F;
            this._serverName.HeaderText = "Server Name";
            this._serverName.Name = "_serverName";
            this._serverName.ReadOnly = true;
            // 
            // _url
            // 
            this._url.FillWeight = 0.25F;
            this._url.HeaderText = "Url";
            this._url.Name = "_url";
            this._url.ReadOnly = true;
            // 
            // _vendor
            // 
            this._vendor.FillWeight = 0.25F;
            this._vendor.HeaderText = "Vendor";
            this._vendor.Name = "_vendor";
            this._vendor.ReadOnly = true;
            // 
            // _daSupported
            // 
            this._daSupported.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this._daSupported.FillWeight = 97.97267F;
            this._daSupported.HeaderText = "DA";
            this._daSupported.Name = "_daSupported";
            this._daSupported.ReadOnly = true;
            this._daSupported.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._daSupported.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._daSupported.Width = 47;
            // 
            // _aeSupported
            // 
            this._aeSupported.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this._aeSupported.FillWeight = 101.6467F;
            this._aeSupported.HeaderText = "AE";
            this._aeSupported.Name = "_aeSupported";
            this._aeSupported.ReadOnly = true;
            this._aeSupported.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._aeSupported.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._aeSupported.Width = 46;
            // 
            // _hdaSupported
            // 
            this._hdaSupported.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this._hdaSupported.FillWeight = 97.08738F;
            this._hdaSupported.HeaderText = "HDA";
            this._hdaSupported.Name = "_hdaSupported";
            this._hdaSupported.ReadOnly = true;
            this._hdaSupported.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._hdaSupported.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._hdaSupported.Width = 55;
            // 
            // _buttonDiscover
            // 
            this._buttonDiscover.Location = new System.Drawing.Point(7, 19);
            this._buttonDiscover.Name = "_buttonDiscover";
            this._buttonDiscover.Size = new System.Drawing.Size(131, 25);
            this._buttonDiscover.TabIndex = 0;
            this._buttonDiscover.Text = "Discover Servers";
            this._buttonDiscover.UseVisualStyleBackColor = true;
            this._buttonDiscover.Click += new System.EventHandler(this.ButtonDiscover_Click);
            // 
            // btnDone
            // 
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDone.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnDone.Location = new System.Drawing.Point(742, 563);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 25);
            this.btnDone.TabIndex = 17;
            this.btnDone.Text = "&Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // _labelServerUrl
            // 
            this._labelServerUrl.AutoSize = true;
            this._labelServerUrl.Location = new System.Drawing.Point(16, 294);
            this._labelServerUrl.Name = "_labelServerUrl";
            this._labelServerUrl.Size = new System.Drawing.Size(102, 13);
            this._labelServerUrl.TabIndex = 23;
            this._labelServerUrl.Text = "Selected Server Url:";
            // 
            // _textBoxServerUrl
            // 
            this._textBoxServerUrl.Location = new System.Drawing.Point(193, 290);
            this._textBoxServerUrl.Name = "_textBoxServerUrl";
            this._textBoxServerUrl.Size = new System.Drawing.Size(543, 20);
            this._textBoxServerUrl.TabIndex = 22;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 591);
            this.Controls.Add(this._labelServerUrl);
            this.Controls.Add(this._textBoxServerUrl);
            this.Controls.Add(this._groupBrowse);
            this.Controls.Add(this._btnConnect);
            this.Controls.Add(this._groupDiscovery);
            this.Controls.Add(this.btnDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Discover DA Servers Sample";
            this._groupBrowse.ResumeLayout(false);
            this._groupDiscovery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._serverGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBrowse;
        private PropertyListViewCtrl PropertiesCTRL;
        private BrowseTreeCtrl BrowseCTRL;
        private System.Windows.Forms.Button _btnConnect;
        private System.Windows.Forms.GroupBox _groupDiscovery;
        private System.Windows.Forms.DataGridView _serverGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn _serverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _url;
        private System.Windows.Forms.DataGridViewTextBoxColumn _vendor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _daSupported;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _aeSupported;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _hdaSupported;
        private System.Windows.Forms.Button _buttonDiscover;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Label _labelServerUrl;
        private System.Windows.Forms.TextBox _textBoxServerUrl;

    }
}


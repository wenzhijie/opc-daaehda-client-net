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
using System.Windows.Forms;
using OpcClientSdk;
using OpcClientSdk.Da;
#endregion

namespace DaBrowser
{
    ///////////////////////////////////////////////////////////////////////
    #region Asynchronous Delegates

    /// <summary>
	/// Use to receive notifications when a server node is 'picked'.
	/// </summary>
	public delegate void ServerPicked_EventHandler(TsCDaServer server);

	/// <summary>
	/// Use to receive notifications when a item node is 'picked'.
	/// </summary>
	public delegate void ItemPicked_EventHandler(OpcItem itemID);
	
	/// <summary>
	/// Use to receive notifications when a tree node is selected.
	/// </summary>
	public delegate void ElementSelected_EventHandler(TsCDaBrowseElement element);

    #endregion

    /// <summary>
	/// A tree control use to navigate the address space of an OPC DA server.
	/// </summary>
	public class BrowseTreeCtrl : System.Windows.Forms.UserControl
	{
        ///////////////////////////////////////////////////////////////////////
        #region Fields

        private System.Windows.Forms.TreeView BrowseTV;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region Constructors, Destructor, Initialization
        
        public BrowseTreeCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			BrowseTV.ImageList = Resources.Instance.ImageList;
			Clear();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				// release all server objects.
				Clear();

				if(components != null)
				{
					components.Dispose();
				}
			}
			
			base.Dispose( disposing );
		}

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region Component Designer generated code
        /// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.BrowseTV = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // BrowseTV
            // 
            this.BrowseTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrowseTV.Location = new System.Drawing.Point(0, 0);
            this.BrowseTV.Name = "BrowseTV";
            this.BrowseTV.Size = new System.Drawing.Size(400, 400);
            this.BrowseTV.TabIndex = 0;
            this.BrowseTV.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.BrowseTV_BeforeExpand);
            this.BrowseTV.DoubleClick += new System.EventHandler(this.PickMI_Click);
            this.BrowseTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.BrowseTV_AfterSelect);
            this.BrowseTV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BrowseTV_MouseDown);
            // 
            // BrowseTreeCtrl
            // 
            this.Controls.Add(this.BrowseTV);
            this.Name = "BrowseTreeCtrl";
            this.Size = new System.Drawing.Size(400, 400);
            this.ResumeLayout(false);

		}
		#endregion
				
		/// <summary>
		/// The underlying tree view. 
		/// </summary>
		public TreeView View {get{return BrowseTV;}}

		/// <summary>
		/// The server associated with the currently selected node.
		/// </summary>
		public TsCDaServer SelectedServer 
		{
			get
			{ 
				TsCDaServer server = FindServer(BrowseTV.SelectedNode); 

				if (server != null)
				{
					return (TsCDaServer)server.Duplicate();
				}

				return null;
			}
		}

		/// <summary>
		/// The currently selected item.
		/// </summary>
		public OpcItem SelectedItem 
		{
			get
			{			
				TreeNode node = BrowseTV.SelectedNode;

				if (IsBrowseElementNode(node))
				{
					TsCDaBrowseElement element = (TsCDaBrowseElement)node.Tag;

					if (element.IsItem)
					{
						return new OpcItem(element.ItemPath, element.ItemName);
					}
				}

				if (IsItemPropertyNode(node))
				{
					TsCDaItemProperty property = (TsCDaItemProperty)node.Tag;

					if (property.ItemName != null && ItemPicked != null)
					{
						return new OpcItem(property.ItemPath, property.ItemName);
					}
				}

				return null;
			}
		}
		
		/// <summary>
		/// Use to receive notifications when a server node is 'picked'.
		/// </summary>
		public event ServerPicked_EventHandler ServerPicked;

		/// <summary>
		/// Use to receive notifications when a item node is 'picked'.
		/// </summary>
		public event ItemPicked_EventHandler ItemPicked;

		/// <summary>
		/// Use to receive notifications when an element is selected.
		/// </summary>
		public event ElementSelected_EventHandler ElementSelected;

		/// <summary>
		/// The current filters to apply when expanding nodes.
		/// </summary>
		private TsCDaBrowseFilters m_filters = null;

		/// <summary>
		/// References to well-known root nodes.
		/// </summary>
		private TreeNode m_singleServer = null;


		/// <summary>
		/// Browses the address space for a single server.
		/// </summary>
		public void ShowSingleServer(TsCDaServer server, TsCDaBrowseFilters filters)
		{
			if (server == null) throw new ArgumentNullException("server");

			Clear();

			m_filters   = (filters == null)?new TsCDaBrowseFilters():filters;

			m_singleServer                    = new TreeNode(server.ServerName);
			m_singleServer.ImageIndex         = Resources.IMAGE_LOCAL_SERVER;
			m_singleServer.SelectedImageIndex = Resources.IMAGE_LOCAL_SERVER;
			m_singleServer.Tag                = server.Duplicate();

			Connect(m_singleServer);
			BrowseTV.Nodes.Add(m_singleServer);	
		}

		/// <summary>
		/// Connects to the server and browses for top level nodes.
		/// </summary>
		private void Connect(TreeNode node)
		{
			try
			{
				if (!IsServerNode(node)) return;

				// get the server for the current node.
				TsCDaServer server = (TsCDaServer)node.Tag;

				// connect to server if not already connected.
				if (!server.IsConnected)
				{
					server.Connect(FindConnectData(node));
				}

				// browse for top level elements.
				Browse(node);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Disconnects from the server and clear all children.
		/// </summary>
		private void Disconnect(TreeNode node)
		{
			try
			{
				if (!IsServerNode(node)) return;

				// get the server for the current node.
				TsCDaServer server = (TsCDaServer)node.Tag;

				// connect to server if not already connected.
				if (server.IsConnected)
				{
					server.Disconnect();
				}
				
				node.Nodes.Clear();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Finds the network connectData for the specified node.
		/// </summary>
		private OpcConnectData FindConnectData(TreeNode node)
		{
			if (node != null)
			{
				if (node.Tag != null && node.Tag.GetType() == typeof(OpcConnectData))
				{
					return (OpcConnectData)node.Tag;
				}

				return FindConnectData(node.Parent);
			}

			if (BrowseTV.Tag != null && BrowseTV.Tag.GetType() == typeof(OpcConnectData))
			{
				return (OpcConnectData)BrowseTV.Tag;
			}

			return null;
		}


		/// <summary>
		/// Browses for children of the element at the current node.
		/// </summary>
		private void Browse(TreeNode node)
		{
			try
			{
				// get the server for the current node.
				TsCDaServer server = FindServer(node);

				// get the current element to use for a browse.
				TsCDaBrowseElement  parent = null;
				OpcItem itemID = null;

				if (node.Tag != null && node.Tag.GetType() == typeof(TsCDaBrowseElement))
				{
					parent = (TsCDaBrowseElement)node.Tag;
					itemID = new OpcItem(parent.ItemPath, parent.ItemName);
				}

				// clear the node children.
				node.Nodes.Clear();

				// add properties
				if (parent != null && parent.Properties != null)
				{
					foreach (TsCDaItemProperty property in parent.Properties)
					{
						AddItemProperty(node, property);
					}
				}

				// begin a browse.
				TsCDaBrowsePosition position = null;
				TsCDaBrowseElement[] elements = server.Browse(itemID, m_filters, out position);

				// add children.
				if (elements != null)
				{
					foreach (TsCDaBrowseElement element in elements)
					{
						AddBrowseElement(node, element);
					}
					
					node.Expand();
				}

				// loop until all elements have been fetched.
				while (position != null)
				{
					DialogResult result = MessageBox.Show(
						"More items meeting search criteria exist. Continue browse?", 
						"Browse Items", 
						MessageBoxButtons.YesNo);
					
					if (result == DialogResult.No)
					{
						break;
					}

					// fetch next batch of elements,.
					elements = server.BrowseNext(ref position);
				
					// add children.
					if (elements != null)
					{
						foreach (TsCDaBrowseElement element in elements)
						{
							AddBrowseElement(node, element);
						}

						node.Expand();
					}
				}

				// send notification that property list changed.
				if (ElementSelected != null)
				{
					if (node.Tag.GetType() == typeof(TsCDaBrowseElement))
					{
						ElementSelected((TsCDaBrowseElement)node.Tag);
					}
					else
					{
						ElementSelected(null);
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// Browses for children of the element at the current node.
		/// </summary>
		private void GetProperties(TreeNode node)
		{
			try
			{
				// get the server for the current node.
				TsCDaServer server = FindServer(node);

				// get the current element to use for a get properties.
				TsCDaBrowseElement element = null;

				if (node.Tag != null && node.Tag.GetType() == typeof(TsCDaBrowseElement))
				{
					element = (TsCDaBrowseElement)node.Tag;
				}

				// can only get properties for an item.
				if (!element.IsItem)
				{
					return;
				}
				
				// clear the node children.
				node.Nodes.Clear();

				// begin a browse.			
				OpcItem itemID = new OpcItem(element.ItemPath, element.ItemName);

				TsCDaItemPropertyCollection[] propertyLists = server.GetProperties(
					new OpcItem[] { itemID },
					m_filters.PropertyIDs,
					m_filters.ReturnPropertyValues);

				if (propertyLists != null)
				{
					foreach (TsCDaItemPropertyCollection propertyList in propertyLists)
					{
						foreach (TsCDaItemProperty property in propertyList)
						{
							AddItemProperty(node, property);
						}

						// update element properties.
						element.Properties = (TsCDaItemProperty[])propertyList.ToArray(typeof(TsCDaItemProperty));
					}
				}

				node.Expand();

				// send notification that property list changed.
				if (ElementSelected != null)
				{
					ElementSelected(element);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}


		/// <summary>
		/// Checks if the current node is a server node.
		/// </summary>
		private bool IsServerNode(TreeNode node)
		{
			if (node == null ||node.Tag == null) return false;
			return typeof(TsCDaServer).IsInstanceOfType(node.Tag);
		}

		/// <summary>
		/// Checks if the current node is a browse element node.
		/// </summary>
		private bool IsBrowseElementNode(TreeNode node)
		{
			if (node == null || node.Tag == null) return false;
			return (node.Tag.GetType() == typeof(TsCDaBrowseElement));
		}

		/// <summary>
		/// Checks if the current node is an item property node.
		/// </summary>
		private bool IsItemPropertyNode(TreeNode node)
		{
			if (node == null || node.Tag == null) return false;
			return (node.Tag.GetType() == typeof(TsCDaItemProperty));
		}

		/// <summary>
		/// Finds the server for the specified node.
		/// </summary>
		private TsCDaServer FindServer(TreeNode node)
		{
			if (node != null)
			{
				if (IsServerNode(node))
				{
					return (TsCDaServer)node.Tag;
				}

				return FindServer(node.Parent);
			}

			return null;
		}

		/// <summary>
		/// Sends a server or item pciked depending on the node.
		/// </summary>
		private void PickNode(TreeNode node)
		{
			if (IsServerNode(node))
			{
				if (ServerPicked != null)
				{
					ServerPicked((TsCDaServer)node.Tag);
				}
			}

			else if (IsBrowseElementNode(node))
			{
				TsCDaBrowseElement element = (TsCDaBrowseElement)node.Tag;

				if (element.IsItem && ItemPicked != null)
				{
					ItemPicked(new OpcItem(element.ItemPath, element.ItemName));
				}
			}

			else if (IsItemPropertyNode(node))
			{
				TsCDaItemProperty property = (TsCDaItemProperty)node.Tag;

				if (property.ItemName != null && ItemPicked != null)
				{
					ItemPicked(new OpcItem(property.ItemPath, property.ItemName));
				}
			}
		}

		/// <summary>
		/// Displays a dialog with the complex type information.
		/// </summary>
		private void ViewComplexType(TreeNode node)
		{
			/*if (!IsBrowseElementNode(node))
			{
				return;
			}
			try
			{
				TsCCpxComplexItem complexItem = TsCCpxComplexTypeCache.GetComplexItem((TsCDaBrowseElement)node.Tag);

				if (complexItem != null)
				{
					new EditComplexValueDlg().ShowDialog(complexItem, null, true, true);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}*/
        }

		/// <summary>
		/// Adds the specified browse element into the tree.
		/// </summary>
		private void AddBrowseElement(TreeNode parent, TsCDaBrowseElement element)
		{
			// create the new node.
			TreeNode node = new TreeNode(element.Name);
			
			// select the icon.
			if (element.IsItem)
			{
                node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_ITEM;
			}
			else
			{
                node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_CLOSED_FOLDER;
			}
	
			node.Tag = element;

			// add a dummy node to force display of '+' symbol.
			if (element.HasChildren)
			{
				node.Nodes.Add(new TreeNode());
			}

			// add properties
			if (element.Properties != null)
			{
				foreach (TsCDaItemProperty property in element.Properties)
				{
					AddItemProperty(node, property);
				}
			}

			// add to parent.
			parent.Nodes.Add(node);
		}

		/// <summary>
		/// Adds the specified item property into the tree.
		/// </summary>
		private void AddItemProperty(TreeNode parent, TsCDaItemProperty property)
		{
            if (property != null && property.Result.Succeeded())
			{
				// create the new node.
				TreeNode node = new TreeNode(property.Description);
			
				// select the icon.
				if (property.ItemName != null && property.ItemName != "")
				{
                    node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_ITEM;
				}
				else
				{
                    node.ImageIndex = node.SelectedImageIndex = Resources.IMAGE_PROPERTY;
				}

				node.Tag = property;

				if (property.Value != null)
				{
					TreeNode child = new TreeNode(OpcClientSdk.OpcConvert.ToString(property.Value));
                    child.ImageIndex = child.SelectedImageIndex = Resources.IMAGE_PROPERTY_VALUE;
					child.Tag = property.Value;
					node.Nodes.Add(child);

					if (property.Value.GetType().IsArray)
					{
						foreach (object element in (Array)property.Value)
						{
							TreeNode arrayChild = new TreeNode(OpcClientSdk.OpcConvert.ToString(element));
                            arrayChild.ImageIndex = arrayChild.SelectedImageIndex = Resources.IMAGE_PROPERTY_VALUE;
							arrayChild.Tag = element;
							child.Nodes.Add(arrayChild);
						}
					}
				}
	
				// add to parent.
				parent.Nodes.Add(node);
			}
		}

		/// <summary>
		/// Removes all nodes and releases all resources.
		/// </summary>
		public void Clear()
		{		
			// recursively searches the tree and free objects.
			foreach (TreeNode child in BrowseTV.Nodes)
			{
				Clear(child);
			}

			// clear the tree.
			BrowseTV.Nodes.Clear();

			m_singleServer = null;
		}

		/// <summary>
		/// Recursively searches the tree and free objects.
		/// </summary>
		private void Clear(TreeNode parent)
		{		
			// search children.
			foreach (TreeNode child in parent.Nodes)
			{
				Clear(child);
			}

			// disconnect servers.
			if (IsServerNode(parent))
			{
				TsCDaServer server = (TsCDaServer)parent.Tag;
				if (server.IsConnected) server.Disconnect();
			}
		}

		/// <summary>
		/// Called before a node is about to expand.
		/// </summary>
		private void BrowseTV_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;

			if (IsBrowseElementNode(node))
			{
				// browse for children if not already fetched.
				if (node.Nodes.Count >= 1 && node.Nodes[0].Text == "")
				{
					Browse(node);
				}

				return;
			}
		}

		/// <summary>
		/// Updates the state of context menus based on the current selection.
		/// </summary>
		private void BrowseTV_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// ignore left button actions.
			if (e.Button != MouseButtons.Right)	return;

			// selects the item that was right clicked on.
			TreeNode clickedNode = BrowseTV.GetNodeAt(e.X, e.Y);

			// no item clicked on - do nothing.
			if (clickedNode == null) return;

			// force selection to clicked node.
			BrowseTV.SelectedNode = clickedNode;

		}	
		
		/// <summary>
		/// Called when the browse filters have changed.
		/// </summary>
		private void OnBrowseFiltersChanged(TsCDaBrowseFilters filters)
		{
			m_filters = filters;

			if (IsBrowseElementNode(BrowseTV.SelectedNode))
			{
				TsCDaBrowseElement element = (TsCDaBrowseElement)BrowseTV.SelectedNode.Tag;

				if (!element.HasChildren)
				{
					GetProperties(BrowseTV.SelectedNode);
					return;
				}
			}

			Browse(BrowseTV.SelectedNode);
		}
		
	

		/// <summary>
		/// Sends a server or item selected event.
		/// </summary>
		private void PickMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = BrowseTV.SelectedNode;

			if (node != null)
			{
				PickNode(node);
			}
		}

		/// <summary>
		/// Sends a server or item selected event for all node children.
		/// </summary>
		private void PickChildrenMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = BrowseTV.SelectedNode;
			
			if (node != null)
			{
				foreach (TreeNode child in node.Nodes)
				{
					PickNode(child);
				}
			}
		}

		/// <summary>
		/// Called when a tree node is selected.
		/// </summary>
		private void BrowseTV_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = BrowseTV.SelectedNode;

			if (ElementSelected != null)
			{
				if (IsBrowseElementNode(node))
				{
					ElementSelected((TsCDaBrowseElement)node.Tag);
				}
				else
				{
					ElementSelected(null);
				}
			}
		}

		/// <summary>
		/// Called to view complex type information for an item.
		/// </summary>
		private void ViewComplexTypeMI_Click(object sender, System.EventArgs e)
		{
			TreeNode node = BrowseTV.SelectedNode;

			if (node != null)
			{
				ViewComplexType(node);
			}
		}
	}
}

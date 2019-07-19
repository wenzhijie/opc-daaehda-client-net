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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
#endregion

namespace DaBrowser
{
	/// <summary>
	/// A class that defines resource constants such as image indexes.
	/// </summary>
	public class Resources : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ImageList ToolBarImageList;
		public System.Windows.Forms.ImageList ImageList;

		private System.ComponentModel.IContainer components;

		public Resources()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.ToolBarImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Teal;
            this.ImageList.Images.SetKeyName(0, "");
            this.ImageList.Images.SetKeyName(1, "");
            this.ImageList.Images.SetKeyName(2, "");
            this.ImageList.Images.SetKeyName(3, "");
            this.ImageList.Images.SetKeyName(4, "prepare_property_icon.png");
            this.ImageList.Images.SetKeyName(5, "prepare_icon.png");
            // 
            // ToolBarImageList
            // 
            this.ToolBarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ToolBarImageList.ImageStream")));
            this.ToolBarImageList.TransparentColor = System.Drawing.Color.Teal;
            this.ToolBarImageList.Images.SetKeyName(0, "");
            this.ToolBarImageList.Images.SetKeyName(1, "");
            this.ToolBarImageList.Images.SetKeyName(2, "");
            this.ToolBarImageList.Images.SetKeyName(3, "");
            this.ToolBarImageList.Images.SetKeyName(4, "");
            this.ToolBarImageList.Images.SetKeyName(5, "");
            this.ToolBarImageList.Images.SetKeyName(6, "");
            this.ToolBarImageList.Images.SetKeyName(7, "");
            this.ToolBarImageList.Images.SetKeyName(8, "");
            this.ToolBarImageList.Images.SetKeyName(9, "");
            this.ToolBarImageList.Images.SetKeyName(10, "");
            // 
            // Resources
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "Resources";
            this.Text = "Resources";
            this.ResumeLayout(false);

		}
		#endregion

		// global constants
        public static readonly int IMAGE_LOCAL_SERVER          = 0;
        public static readonly int IMAGE_CLOSED_FOLDER         = 1;
		public static readonly int IMAGE_OPEN_FOLDER           = 2;
		public static readonly int IMAGE_ITEM                  = 3;
		public static readonly int IMAGE_PROPERTY              = 4;
        public static readonly int IMAGE_PROPERTY_VALUE        = 5;


		public static Resources Instance = new Resources();
	}
}

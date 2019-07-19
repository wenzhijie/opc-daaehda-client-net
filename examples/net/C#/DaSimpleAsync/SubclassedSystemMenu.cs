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
#endregion

/// -----------------------------------------------------------------
///	Class Name:  SubclassedSystemMenu
///	Description: Object that allows a modified system menu 
///              to be implemented and interacted with. The 
///              object is designed to add a new seperator and 
///              "About..." menu item to a windows system menu
/// -----------------------------------------------------------------
namespace DaSimpleAsync
{
    public class SubclassedSystemMenu : System.Windows.Forms.NativeWindow, IDisposable
    {
        /// -----------------------------------------------------------------
        ///  Win32 API Declares
        /// -----------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int GetSystemMenu(int hwnd, bool bRevert);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "AppendMenuA")]
        public static extern int AppendMenu(int hMenu, int wFlags, int wIDNewItem, string lpNewItem);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int DeleteMenu(int hMenu, int nPosition, int wFlags);


        /// -----------------------------------------------------------------
        ///  Constants
        /// -----------------------------------------------------------------
        private const int MF_BYPOSITION = 1024;			/// Position for remove Item 

        private const Int32 MF_STRING = 0;				/// Menu string format
        private const Int32 MF_SEPARATOR = 2048;		/// Menu separator
        private const Int32 WM_SYSCOMMAND = 274;		/// System menu 
        private const Int32 ID_ABOUT = 1000;			/// Our ID for the new menu item


        /// -----------------------------------------------------------------
        ///  Member Variables
        /// -----------------------------------------------------------------
        private Int32 mintSystemMenu;
        private Int32 mintHandle;
        private string mstrMenuItemText = string.Empty;


        /// -----------------------------------------------------------------
        ///  Events
        /// -----------------------------------------------------------------
        public EventHandler LaunchDialog;


        /// - Constructor ---------------------------------------------------
        ///
        /// Method Name:        New
        ///	Description:	    Constructor. Creates menu items and assigns subclass
        ///
        /// Inputs:             intWindowHandle : Parent window handle for message 
        ///                                       subclass and adding new menu items 
        ///                                       to parent system menu
        ///
        ///	Return Value:       None
        ///
        /// -----------------------------------------------------------------
        public SubclassedSystemMenu(Int32 intWindowHandle, string strMenuItemText)
        {
            AssignHandle(new IntPtr(intWindowHandle));
            mintHandle = intWindowHandle;
            mstrMenuItemText = strMenuItemText;
            mintSystemMenu = GetSystemMenu(mintHandle, false);
            if (AddNewSystemMenuItem() == false)
            {
                throw new Exception("Unable to add new system menu items");
            }
        }

        /// - Methods -------------------------------------------------------
        ///
        /// Method Name:        WndProc
        ///	Description:	    Subclassed window message delegate
        ///
        ///	Inputs:             m : Window Message 
        ///
        /// Return Value:       None
        ///
        /// -----------------------------------------------------------------
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                base.WndProc(ref m);
                if (m.WParam.ToInt32() == ID_ABOUT)
                {
                    if (mintSystemMenu != 0)
                    {
                        if (LaunchDialog != null)
                        {
                            EventArgs eArgs = new EventArgs();
                            LaunchDialog(this, eArgs);
                        }
                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }


        /// -----------------------------------------------------------------
        ///
        /// Method Name:        Dispose
        /// Description:	    IDispose interface implementation
        ///
        /// Inputs:             None 
        ///
        /// Return Value:       None
        ///
        /// -----------------------------------------------------------------
        public void Dispose()
        {
            if (!Handle.Equals(IntPtr.Zero))
            {
                ReleaseHandle();
            }
        }



        /// -----------------------------------------------------------------
        ///
        /// Method Name:        AddNewSystemMenuItem
        ///	Description:	    Adds system menu items
        ///
        /// Inputs:             None 
        ///
        /// Return Value:       True if successful, False else
        ///
        /// -----------------------------------------------------------------
        private bool AddNewSystemMenuItem()
        {
            try
            {
                return AppendToSystemMenu(mintSystemMenu, mstrMenuItemText);
            }
            catch
            {
                return false;
            }
        }


        /// -----------------------------------------------------------------
        ///
        /// Method Name:        AppendToSystemMenu
        ///	Description:	    Adds system menu items (Separator & About...?)
        ///
        /// Inputs:             intHandle : System Menu handle 
        ///                       strText : Text for new menu item
        ///
        /// Return Value:       True if successful, False else
        ///
        /// -----------------------------------------------------------------
        private bool AppendToSystemMenu(Int32 intHandle, string strText)
        {
            try
            {
                Int32 intRet = AppendMenu(intHandle, MF_SEPARATOR, 0, string.Empty);
                intRet = AppendMenu(intHandle, MF_STRING, ID_ABOUT, strText);
                if (intRet == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void RemoveMenus(System.Windows.Forms.Form frm, bool remove_restore, bool remove_move, bool remove_size, bool remove_minimize, bool remove_maximize, bool remove_seperator, bool remove_close)
        {
            int hMenu = GetSystemMenu(Handle.ToInt32(), false);
            if (remove_close)
            {
                DeleteMenu(hMenu, 6, MF_BYPOSITION);
            }
            if (remove_seperator)
            {
                DeleteMenu(hMenu, 5, MF_BYPOSITION);
            }
            if (remove_maximize)
            {
                DeleteMenu(hMenu, 4, MF_BYPOSITION);
            }
            if (remove_minimize)
            {
                DeleteMenu(hMenu, 3, MF_BYPOSITION);
            }
            if (remove_size)
            {
                DeleteMenu(hMenu, 2, MF_BYPOSITION);
            }
            if (remove_move)
            {
                DeleteMenu(hMenu, 1, MF_BYPOSITION);
            }
            if (remove_restore)
            {
                DeleteMenu(hMenu, 0, MF_BYPOSITION);
            }
        }
    }
}

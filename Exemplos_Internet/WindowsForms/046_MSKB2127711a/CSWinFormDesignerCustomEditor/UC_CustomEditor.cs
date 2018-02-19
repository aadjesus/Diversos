/************************************* Module Header **************************************\
* Module Name:	UC_CustomEditor.cs
* Project:		CSWinFormDesignerCustomEditor
* Copyright (c) Microsoft Corporation.
* 
* 
* The CSWinFormDesignerCustomEditor sample demonstrates how to use a custom editor for a 
* specific property at design time.
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE 
\******************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace CSWinFormDesignerCustomEditor
{
    [Serializable()]
    public partial class UC_CustomEditor : UserControl
    {
        private SubClass cls;

        public UC_CustomEditor()
        {
            InitializeComponent();
            cls = new SubClass("Name", System.DateTime.Now);
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(MyEditor), typeof(UITypeEditor))]
        public SubClass Cls
        {
            get
            {
                this.lblName.Text = this.cls.Name;
                this.lblDateTime.Text = this.cls.Date.ToString();
                return this.cls;
            }
            set 
            {
                this.lblName.Text = ((SubClass)value).Name;
                this.lblDateTime.Text = ((SubClass)value).Date.ToString();
                this.cls = value;
            }
        }
    }

    public class SubClass
    {
        public SubClass()
        {
        }

        public SubClass(string name, DateTime time)
        {
            this.date = time;
            this.name = name;
        }

        private string name;
        private DateTime date = DateTime.Now;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; }
        }
    }

    #region UITypeEditor

    // This class demonstrates the use of a custom UITypeEditor. 
    // It allows the UC_CustomEditor control's Cls property
    // to be changed at design time using a customized UI element
    // that is invoked by the Properties window. The UI is provided
    // by the EditorForm class.
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class MyEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService = null;

        public override UITypeEditorEditStyle GetEditStyle(
        System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object value)
        {
            if (provider != null)
            {
                editorService =
                    provider.GetService(typeof(IWindowsFormsEditorService))
                    as IWindowsFormsEditorService;
            }

            if (editorService != null)
            {
                EditorForm editorForm = new EditorForm((SubClass)value);
                if (editorService.ShowDialog(editorForm) == DialogResult.OK)
                {
                    value = editorForm.SubCls;
                }
            }
            return value;
        }

        public override bool GetPaintValueSupported(
            ITypeDescriptorContext context)
        {
            return true;
        }


        public override void PaintValue(PaintValueEventArgs e)
        {
            
        }
    }
    #endregion
}

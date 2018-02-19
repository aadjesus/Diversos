/******************************************************************************* 
 *  Copyright 2009 http://www.SoapPanda.com.
 *  
 *  Author: Anantjot Anand
 *
 * 
 * */

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GenericForm
{
    public partial class ComplexTypeArray : UserControl
    {
        public int OFFSET = 10;
        public int BORDER_OFFSET = 20;
        public int CONTROL_HEIGHT = 25;
        public int LABEL_WIDTH = 200;
        public int TEXTBOX_WIDTH = 400;
        public int COMPLEX_BUTTON_WIDTH = 100;
        public int GET_SET_BUTTON_WIDTH = 50;
        public int LOCK_WIDTH = 16;
        static public int ARRAY_MAX_SIZE = 100;
        public ComplexTypeArray()
        {
            InitializeComponent();
        }

        private Assembly clientAssembly; 

        public Control parent;

        private ArrayList instance;

        public ArrayList Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        private Type arrayType;

        public Type ArrayType
        {
            get { return arrayType; }
            set { arrayType = value; }
        }


        private bool isProperty = false;
        public bool IsProperty
        {
            get { return isProperty; }
            set { isProperty = value; }
        }


        private ComplexType complexType;

        public ComplexTypeArray(Assembly ass, ArrayList instance, Type arrayType, Control parent, bool bProperty)
        {
            this.clientAssembly = ass;
            this.parent = parent;
            this.instance = instance;
            this.arrayType = arrayType;
            this.isProperty = bProperty;

            InitializeComponent();

            if (isProperty)
                AddPropertyControls(this.instance.GetType(), this.panel1);
            else
                AddFieldControls(this.instance.GetType(), this.panel1);


            this.btnAdd.Click+=new EventHandler(btnAdd_Click);
            this.btnRemove.Click += new EventHandler(btnRemove_Click);
            this.btnClear.Click += new EventHandler(btnClear_Click);
        }

        public void AddPropertyControls(Type moduleType, Panel panl)
        {
            panl.Location = new System.Drawing.Point(0, 0);

            this.listView1.View = View.Details;
            // Allow the user to rearrange columns.
            this.listView1.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            this.listView1.FullRowSelect = true;
            // Display grid lines.
            this.listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            this.listView1.Sorting = SortOrder.Ascending;

            int offset = OFFSET;
            int x = OFFSET, y = this.btnAdd.Location.Y + this.btnAdd.Height + OFFSET;
            int lLabelWidth = LABEL_WIDTH, lHeight = CONTROL_HEIGHT;
            int lTextBoxWidth = TEXTBOX_WIDTH;
            int lBtnWidth = GET_SET_BUTTON_WIDTH;

            this.complexType = new ComplexType(this.clientAssembly, this.instance, this.parent, this.isProperty);

            this.complexType.Location = new System.Drawing.Point(x,y);

            panl.Controls.Add(this.complexType);

            PropertyInfo[] properties = this.arrayType.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                this.listView1.Columns.Add(propertyInfo.Name, 100);
            }


            panl.Width = complexType.Width;
            panl.Height = y+complexType.Height+offset;

            this.Width = panl.Width;
            this.Height = panl.Height;
            
            
        }

        public void AddFieldControls(Type moduleType, Panel panl)
        {

            panl.Location = new System.Drawing.Point(0, 0);

            this.listView1.View = View.Details;
            // Allow the user to rearrange columns.
            this.listView1.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            this.listView1.FullRowSelect = true;
            // Display grid lines.
            this.listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            this.listView1.Sorting = SortOrder.Ascending;

            int offset = OFFSET;
            int x = OFFSET, y = this.btnAdd.Location.Y + this.btnAdd.Height + OFFSET;
            int lLabelWidth = LABEL_WIDTH, lHeight = CONTROL_HEIGHT;
            int lTextBoxWidth = TEXTBOX_WIDTH;
            int lBtnWidth = GET_SET_BUTTON_WIDTH;

            
            this.complexType = new ComplexType(this.clientAssembly, this.arrayType, this, this.isProperty);

            this.complexType.Location = new System.Drawing.Point(x, y);

            panl.Controls.Add(this.complexType);

            if (ComplexType.IsBasicType(this.arrayType))
            {
                this.listView1.Columns.Add(this.arrayType.Name, 100);

            }
            else
            {

                FieldInfo[] fields = this.arrayType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                foreach (FieldInfo fieldInfo in fields)
                {
                    this.listView1.Columns.Add(fieldInfo.Name, 100);
                }

            }    

            panl.Width = complexType.Width;
            panl.Height = y + complexType.Height + offset;

            this.Width = panl.Width;
            this.Height = panl.Height;


        }

        public void ResizeParent(Control newCtl, bool bExpand)
        {
            Control parent = this.parent;
            Control child = this;
            while (parent != null)
            {
                if (parent is ComplexType)
                {

                    ((ComplexType)parent).ReLayoutChild(child, newCtl, bExpand);
                    child = parent;
                    parent = ((ComplexType)parent).parent;

                }
                else if (parent is ComplexTypeArray)
                {
                    ((ComplexTypeArray)parent).ReLayoutChild(child,newCtl, bExpand);
                    child = parent;
                    parent = ((ComplexTypeArray)parent).parent;
                }
                else if (parent is ObjectPanel)
                {
                    ((ObjectPanel)parent).ReLayoutChild(child, newCtl, bExpand);
                    break;
                }
                else
                {
                    MessageBox.Show(string.Format("Unknown Parent type {0}", parent.GetType().FullName), "Soap Panda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        public void ReLayoutChild(Control child, Control newCtl, bool bExpand)
        {
            int maxWidth = 0;
            for (int i = 0; i < this.panel1.Controls.Count; i++)
            {
                if (child != this.panel1.Controls[i] && this.panel1.Controls[i].Top > child.Top)
                    if (bExpand)
                        this.panel1.Controls[i].Top += newCtl.Height;
                    else
                        this.panel1.Controls[i].Top -= newCtl.Height;

                if ((this.panel1.Controls[i].Location.X + this.panel1.Controls[i].Width) > maxWidth)
                    maxWidth = this.panel1.Controls[i].Location.X + this.panel1.Controls[i].Width;
            }
            if (bExpand)
                this.ClientSize = new Size(maxWidth + OFFSET, this.Height + newCtl.Height);
            else
                this.ClientSize = new Size(maxWidth + OFFSET, this.Height - newCtl.Height);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Object myInstance =null;
                string[] subItems = new string[this.listView1.Columns.Count];

                if (ComplexType.IsBasicType(this.arrayType) && this.complexType.Panel1.Controls.Count > 1)
                {
                    if (this.complexType.Panel1.Controls[1] is CheckBox)
                    {
                        myInstance = ((CheckBox)this.complexType.Panel1.Controls[1]).Checked;
                        subItems[1] = myInstance.ToString();
                        ((CheckBox)this.complexType.Panel1.Controls[1]).Checked = false;
                    }
                    else if (this.complexType.Panel1.Controls[1] is TextBox)
                    {
                        myInstance = ComplexType.ConvertTextToParameterType(this.complexType.Panel1.Controls[1].Text, this.arrayType);
                        subItems[0] = this.complexType.Panel1.Controls[1].Text;
                        this.complexType.Panel1.Controls[1].Text = "";
                    }
                    else if (this.complexType.Panel1.Controls[1] is ComboBox)
                    {
                        myInstance = Enum.Parse(this.arrayType, this.complexType.Panel1.Controls[1].Text);
                        subItems[0] = this.complexType.Panel1.Controls[1].Text;
                    }

                }
                else
                {
                    myInstance = Assembly.GetAssembly(this.arrayType).CreateInstance(this.arrayType.FullName);

                    if (myInstance == null)
                    {
                        MessageBox.Show(String.Format("Fail to create instance of type {0}", this.instance.GetType().GetElementType().FullName));
                        return;
                    }

                    FieldInfo[] fields = this.arrayType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                    int i = 0;

                    foreach (FieldInfo fieldInfo in fields)
                    {

                        string subItemText = string.Empty;
                        int indx = -1;

                        if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_cmb", fieldInfo.Name))) == -1)
                        {
                            if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_chk", fieldInfo.Name))) == -1)
                            {
                                if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_txt", fieldInfo.Name))) == -1)
                                {
                                    if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_btn", fieldInfo.Name))) == -1)
                                    {
                                        subItems[i++] = "Failed";
                                        continue;
                                    }
                                }
                            }
                        }
                        Object paramObject = null;
                        if (this.complexType.Panel1.Controls[indx] is ComboBox)
                        {
                            paramObject = Enum.Parse(fieldInfo.FieldType, this.complexType.Panel1.Controls[indx].Text);
                            subItemText = this.complexType.Panel1.Controls[indx].Text;

                        }else if (this.complexType.Panel1.Controls[indx] is CheckBox)
                        {
                            paramObject = ((CheckBox)this.complexType.Panel1.Controls[indx]).Checked;
                            subItemText = paramObject.ToString();
                            ((CheckBox)this.complexType.Panel1.Controls[indx]).Checked = false;

                        }
                        else if (this.complexType.Panel1.Controls[indx] is TextBox)
                        {
                            paramObject = ComplexType.ConvertTextToParameterType(this.complexType.Panel1.Controls[indx].Text, fieldInfo.FieldType);
                            subItemText = this.complexType.Panel1.Controls[indx].Text;
                            this.complexType.Panel1.Controls[indx].Text = "";
                        }
                        else if (this.complexType.Panel1.Controls[indx] is ComplexTypeButton)
                        {
                            ComplexTypeButton btn = (ComplexTypeButton)this.complexType.Panel1.Controls[indx];
                            paramObject = btn.Tag;
                            subItemText = string.Format("Object of type '{0}'", btn.Tag.GetType().FullName);

                            btn.Tag = Assembly.GetAssembly(fieldInfo.FieldType).CreateInstance(fieldInfo.FieldType.FullName);

                            if (btn.UserControl != null)
                            {
                                btn.UserControl.Instance = btn.Tag;
                                btn.UserControl.GetAll(btn.Tag);
                            }

                        }
                        else if (this.complexType.Panel1.Controls[indx] is ComplexTypeArrayButton)
                        {
                            ComplexTypeArrayButton btn = (ComplexTypeArrayButton)this.complexType.Panel1.Controls[indx];
                            paramObject = ((ArrayList)btn.Tag).ToArray(btn.ArrayType);
                            subItemText = string.Format("Array of type '{0}'", btn.ArrayType.FullName);

                            this.complexType.Panel1.Controls[indx].Tag = new ArrayList();

                            if (btn.UserControl != null)
                            {
                                btn.UserControl.Instance = (ArrayList)btn.Tag;
                                btn.UserControl.GetAll();
                            }

                        }


                        if (paramObject == null)
                        {
                            subItemText = "Failed";
                        }
                        else
                        {
                            try
                            {
                                fieldInfo.SetValue(myInstance, paramObject);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                subItemText = "Failed";
                            }
                        }

                        subItems[i++] = subItemText;

                    }

                }

                this.instance.Add(myInstance);

                this.listView1.Items.Add(new ListViewItem(subItems)).Tag = myInstance;
            }
            catch (Exception ex)
            {

            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem myLV = null;
                foreach (ListViewItem lv in this.listView1.Items)
                {
                    if (lv.Tag == this.complexType.Instance)
                        myLV = lv;
                }


                if (myLV != null && myLV.Tag != null)
                {
                    if (ComplexType.IsBasicType(this.arrayType))
                    {
                        MessageBox.Show("Update is not allowed. Please use add and remove to update this array");
                    }
                    else
                    {
                        FieldInfo[] fields = this.arrayType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        int i = 0;
                        foreach (FieldInfo fieldInfo in fields)
                        {

                            string subItemText = string.Empty;
                            int indx = -1;
                            if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_cmb", fieldInfo.Name))) == -1)
                            {
                                if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_chk", fieldInfo.Name))) == -1)
                                {
                                    if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_txt", fieldInfo.Name))) == -1)
                                    {
                                        if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_btn", fieldInfo.Name))) == -1)
                                        {
                                            myLV.SubItems[i++].Text = "Failed";
                                            continue;
                                        }
                                    }
                                }
                            }
                            Object paramObject = null;
                            if (this.complexType.Panel1.Controls[indx] is ComboBox)
                            {
                                paramObject = Enum.Parse(fieldInfo.FieldType,((ComboBox)this.complexType.Panel1.Controls[indx]).Text);
                                subItemText = paramObject.ToString();
                            }
                            if (this.complexType.Panel1.Controls[indx] is CheckBox)
                            {
                                paramObject = ((CheckBox)this.complexType.Panel1.Controls[indx]).Checked;
                                subItemText = paramObject.ToString();
                            }
                            else if (this.complexType.Panel1.Controls[indx] is TextBox)
                            {
                                paramObject = ComplexType.ConvertTextToParameterType(this.complexType.Panel1.Controls[indx].Text, fieldInfo.FieldType);
                                subItemText = this.complexType.Panel1.Controls[indx].Text;
                            }
                            else if (this.complexType.Panel1.Controls[indx] is ComplexTypeButton)
                            {
                                ComplexTypeButton btn = (ComplexTypeButton)this.complexType.Panel1.Controls[indx];
                                paramObject = btn.Tag;
                                subItemText = string.Format("Object of type '{0}'", btn.Tag.GetType().FullName);
                            }
                            else if (this.complexType.Panel1.Controls[indx] is ComplexTypeArrayButton)
                            {
                                ComplexTypeArrayButton btn = (ComplexTypeArrayButton)this.complexType.Panel1.Controls[indx];
                                paramObject = ((ArrayList)btn.Tag).ToArray(btn.ArrayType);
                                subItemText = string.Format("Array of type '{0}'", btn.ArrayType.FullName);
                            }


                            if (paramObject == null)
                            {
                                subItemText = "Failed";
                            }
                            else
                            {
                                try
                                {
                                    fieldInfo.SetValue(myLV.Tag, paramObject);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    subItemText = "Failed";
                                }
                            }

                            myLV.SubItems[i++].Text = subItemText;

                        }

                        //
                        //reset fields to new objects for add
                        //remember new for loop is used because other events 
                        //for detecting textchange resets Availibility  specified
                        foreach (FieldInfo fieldInfo in fields)
                        {
                            int indx = -1;
                            if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_chk", fieldInfo.Name))) == -1)
                                if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_txt", fieldInfo.Name))) == -1)
                                    if ((indx = this.complexType.Panel1.Controls.IndexOfKey(string.Format("{0}_btn", fieldInfo.Name))) == -1)
                                        continue;
                            Object paramObject = null;
                            if (this.complexType.Panel1.Controls[indx] is ComboBox)
                            {
                                //drop down doesn't need to reset
                            }else if (this.complexType.Panel1.Controls[indx] is CheckBox)
                            {
                                ((CheckBox)this.complexType.Panel1.Controls[indx]).Checked = false;
                            }
                            else if (this.complexType.Panel1.Controls[indx] is TextBox)
                            {
                                this.complexType.Panel1.Controls[indx].Text = "";
                            }
                            else if (this.complexType.Panel1.Controls[indx] is ComplexTypeButton)
                            {
                                ComplexTypeButton btn = (ComplexTypeButton)this.complexType.Panel1.Controls[indx];
                                btn.Tag = Assembly.GetAssembly(fieldInfo.FieldType).CreateInstance(fieldInfo.FieldType.FullName);
                                if (btn.UserControl != null)
                                {
                                    btn.UserControl.Instance = btn.Tag;
                                    btn.UserControl.GetAll(btn.Tag);
                                }
                            }
                            else if (this.complexType.Panel1.Controls[indx] is ComplexTypeArrayButton)
                            {
                                ComplexTypeArrayButton btn = (ComplexTypeArrayButton)this.complexType.Panel1.Controls[indx];
                                btn.Tag = new ArrayList();
                                if (btn.UserControl != null)
                                {
                                    btn.UserControl.Instance = (ArrayList)btn.Tag;
                                    btn.UserControl.GetAll();
                                }

                            }
                        }
                    }
                }

            
            
            }
            catch (Exception ex)
            {

            }


            this.btnUpdate.Enabled = false;
            this.btnAdd.Enabled = true;
            this.complexType.Instance = null;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;

            this.instance.Remove(this.listView1.SelectedItems[0].Tag);
            this.listView1.SelectedItems[0].Remove();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to remove all the items from the list?", "Soap Panda", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.listView1.Items.Clear();
                this.instance.Clear();
            }

        }

        public void GetAll()
        {
            AddListValues();
        }

        public void SetAll()
        {
        }

        public void AddListValues()
        {
            FieldInfo[] fields = arrayType.GetFields(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public);

            this.listView1.Items.Clear();

            IEnumerator ienum = this.instance.GetEnumerator();

            while (ienum.MoveNext())
            {
                Object obj = ienum.Current;

                if (obj == null) continue;

                string[] subItems = new string[this.listView1.Columns.Count];

                if (ComplexType.IsBasicType(this.arrayType))
                {
                     subItems[0] = obj.ToString();
                }
                else
                {
                    int i = 0;
                    foreach (FieldInfo fieldInfo in fields)
                    {
                        try
                        {
                            Object paramObject = null;
                            paramObject = fieldInfo.GetValue(obj);
                            if (paramObject == null)
                            {
                                MessageBox.Show(string.Format("Type '{0}' not supported. ", fieldInfo.FieldType.Name));
                                subItems[i++] = "Failed";
                                continue;
                            }
                            subItems[i++] = paramObject.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            subItems[i++] = "Failed";
                        }
                    }
                }

                this.listView1.Items.Add(new ListViewItem(subItems)).Tag = obj;

            }



        }

        public void SoapArray_Load(object sender, EventArgs e)
        {
            GetAll();
        }

        public void SoapArray_Leave(object sender, EventArgs e)
        {
            SetAll();
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lv = listView1.SelectedItems[0];
                this.complexType.GetAll(lv.Tag);
                this.complexType.Instance = lv.Tag;
                Object obj;
                if (!ComplexType.IsBasicType(this.arrayType))
                {
                    this.btnUpdate.Enabled = true;
                    this.btnAdd.Enabled = false;
                }
            }
        }
    }

    public class ComplexTypeArrayButton : Button
    {
        private ComplexTypeArray userControl = null;

        public ComplexTypeArray UserControl
        {
            get { return userControl; }
            set { userControl = value; }
        }

        private Type arrayType;

        public Type ArrayType
        {
            get { return arrayType; }
            set { arrayType = value; }
        }
    }
}

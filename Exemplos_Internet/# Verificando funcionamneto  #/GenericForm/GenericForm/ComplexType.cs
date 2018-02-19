/******************************************************************************* 
 *  Copyright 2009 http://www.SoapPanda.com.
 *  
 *  Author: Anantjot Anand
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Collections;


namespace GenericForm
{
    public partial class ComplexType : UserControl
    {
        public int OFFSET = 10;
        public int BORDER_OFFSET = 20;
        public int CONTROL_HEIGHT = 25;
        public int LABEL_WIDTH = 200;
        public int TEXTBOX_WIDTH = 400;
        public int COMPLEX_BUTTON_WIDTH = 100;
        public int GET_SET_BUTTON_WIDTH = 50;
        public int LOCK_WIDTH = 16;
        
        public Control parent;

        private Object instance;

        public Object Instance
        {
            get { return instance; }
            set { instance = value; }
        }


        private bool isProperty = false;

        public bool IsProperty
        {
            get { return isProperty; }
            set { isProperty = value; }
        }


        private Assembly clientAssembly;

        public ComplexType(Assembly ass, Object instance, Control parent, bool bProperty)
        {
            this.parent = parent;
            this.instance = instance;
            this.clientAssembly = ass;
            
            this.isProperty = bProperty;

            InitializeComponent();

            if(isProperty)
                AddPropertyControls(this.instance.GetType(), this.panel1);
            else
                AddFieldControls(this.instance.GetType(), this.panel1);
        }

        public ComplexType(Assembly ass, Type cmplxType, Control parent, bool bProperty)
        {
            this.parent = parent;
            this.instance = null;
            this.clientAssembly = ass;
            this.isProperty = bProperty;

            InitializeComponent();

            if (isProperty)
                AddPropertyControls(cmplxType, this.panel1);
            else
                AddFieldControls(cmplxType, this.panel1);
        }
        /*
         * 1)Check field type 
         *      if type is an Array create the instance and assign it to the field value
         *      if type's name space doesn't start with System and is of type class then 
         *          create instance and assign it to the value of enclosing type
         *      
         */


        public void AddPropertyControls(Type moduleType, Panel panl)
        {
            PropertyInfo[] fields = moduleType.GetProperties();

            int offset = OFFSET;
            int x = OFFSET, y = OFFSET;
            int lLabelWidth = LABEL_WIDTH, lHeight = CONTROL_HEIGHT;
            int lTextBoxWidth = TEXTBOX_WIDTH;
            int lBtnWidth = COMPLEX_BUTTON_WIDTH;

            panl.Location = new System.Drawing.Point(0, 0);

            foreach (PropertyInfo propertyInfo in fields)
            {

                Label lbl = new Label();

                lbl.Location = new System.Drawing.Point(x, y);
                lbl.Name = string.Format("{0}_lbl", propertyInfo.Name);
                lbl.Size = new System.Drawing.Size(lLabelWidth, lHeight);
                lbl.Text = string.Format("{0}({1})", propertyInfo.Name, propertyInfo.PropertyType.Name);
                panl.Controls.Add(lbl);

                if (propertyInfo.PropertyType.IsArray)
                {
                    ComplexTypeArrayButton btn = new ComplexTypeArrayButton();
                    btn.Tag = new ArrayList();
                    btn.ArrayType = propertyInfo.PropertyType.GetElementType();

                    try
                    {
                        if (this.instance != null)
                        {
                            Object ret = moduleType.InvokeMember(propertyInfo.Name,
                                                BindingFlags.Public | BindingFlags.NonPublic |
                                                BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField,
                                                null, this.instance, null);
                            if (ret != null && ret is Array)
                            {
                                Array arr = (Array)ret;
                                for (int i = 0; i < arr.Length; i++)
                                    ((ArrayList)btn.Tag).Add(arr.GetValue(i));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    btn.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    btn.Name = string.Format("{0}_btn", propertyInfo.Name);
                    btn.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);
                    btn.Text = string.Format("Add or Remove {0} Instance Array Objects", propertyInfo.PropertyType.Name);
                    btn.Click += new EventHandler(arr_btn_Click);

                    y += lHeight + offset;
                    panl.Controls.Add(btn);
                }
                else if (!string.IsNullOrEmpty(propertyInfo.PropertyType.Namespace) && propertyInfo.PropertyType.Namespace.StartsWith("System") == false && propertyInfo.PropertyType.IsClass)
                {
                    Button btn = new ComplexTypeButton();
                    try
                    {
                        if (this.instance != null)
                        {
                            btn.Tag = moduleType.InvokeMember(propertyInfo.Name,
                                                BindingFlags.Public | BindingFlags.NonPublic |
                                                BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField,
                                                null, this.instance, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        btn.Tag = null;
                    }
                    try
                    {
                        if (btn.Tag == null)
                        {
                            btn.Tag = Assembly.GetAssembly(propertyInfo.PropertyType).CreateInstance(propertyInfo.PropertyType.FullName);
                            if (btn.Tag != null && this.instance!=null)
                            {
                                moduleType.InvokeMember(propertyInfo.Name,
                                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetField |
                                BindingFlags.Instance | BindingFlags.SetProperty, null, this.instance, new Object[] { btn.Tag });

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        continue;
                    }

                    btn.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    btn.Name = string.Format("{0}_btn", propertyInfo.Name);
                    btn.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);
                    btn.Text = string.Format("Set {0} Instance Field Members", propertyInfo.PropertyType.Name);
                    btn.Click += new EventHandler(cmplx_btn_Click);

                    y += lHeight + offset;
                    panl.Controls.Add(btn);
                }
                else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(Boolean))
                {
                    CheckBox chk = new CheckBox();

                    chk.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    chk.Name = string.Format("{0}_chk", propertyInfo.Name);
                    chk.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                    y += lHeight + offset;
                    panl.Controls.Add(chk);
                }

                else
                {
                    TextBox txt = new TextBox();
                    try
                    {
                        if (this.instance != null)
                        {
                            txt.Tag = this.instance.GetType().InvokeMember(propertyInfo.Name,
                                            BindingFlags.Public | BindingFlags.NonPublic |
                                            BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField,
                                            null, this.instance, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        txt.Tag = null;
                    }
                    try
                    {

                        if (txt.Tag == null)
                        {
                            txt.Tag = Assembly.GetAssembly(propertyInfo.PropertyType).CreateInstance(propertyInfo.PropertyType.FullName);
                            if (txt.Tag != null && this.instance != null)
                            {
                                this.instance.GetType().InvokeMember(propertyInfo.Name,
                                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetField |
                                BindingFlags.Instance | BindingFlags.SetProperty, null, this.instance, new Object[] { txt.Tag });

                            }
                        }
                        else
                        {
                            txt.Text = txt.Tag.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    txt.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    txt.Name = string.Format("{0}_txt", propertyInfo.Name);
                    txt.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                    y += lHeight + offset;

                    panl.Controls.Add(txt);


                }
            }

            panl.Width = x + lLabelWidth + lTextBoxWidth + LOCK_WIDTH + offset;
            panl.Height = y;

            this.Width = panl.Width;
            this.Height = panl.Height;


        }

        public void AddFieldControls(Type moduleType, Panel panl)
        {
            FieldInfo[] fields = moduleType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            int offset = OFFSET;
            int x = OFFSET, y = OFFSET;
            int lLabelWidth = LABEL_WIDTH, lHeight = CONTROL_HEIGHT;
            int lTextBoxWidth = TEXTBOX_WIDTH;
            int lBtnWidth = COMPLEX_BUTTON_WIDTH;

            panl.Location = new System.Drawing.Point(0, 0);

            object obj;
            if (IsBasicType(moduleType))
            {
                Label lbl = new Label();

                lbl.Location = new System.Drawing.Point(x, y);
                lbl.Name = string.Format("{0}_lbl", moduleType.Name);
                lbl.Size = new System.Drawing.Size(lLabelWidth, lHeight);
                lbl.Text = string.Format("{0}({1})", moduleType.Name, moduleType.Name);
                panl.Controls.Add(lbl);

                if (moduleType.IsEnum)
                {
                    ComboBox cmb = new ComboBox();

                    cmb.DataSource = Enum.GetNames(moduleType);
                    cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmb.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    cmb.Name = string.Format("{0}_cmb", moduleType.Name);
                    cmb.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                    y += lHeight + offset;
                    panl.Controls.Add(cmb);
                }else if (moduleType == typeof(bool) || moduleType == typeof(Boolean))
                {
                    CheckBox chk = new CheckBox();

                    chk.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    chk.Name = string.Format("{0}_chk", moduleType.Name);
                    chk.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                    y += lHeight + offset;
                    panl.Controls.Add(chk);
                }
                else
                {
                    TextBox txt = new TextBox();
                    try
                    {
                        if (this.instance != null)
                        {
                            txt.Tag = this.instance;
                        }
                    }
                    catch (Exception ex)
                    {
                        txt.Tag = null;
                    }
                    try
                    {

                        if (txt.Tag == null)
                        {
                            txt.Tag = this.instance;
                        }
                        else
                        {
                            txt.Text = txt.Tag.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    txt.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                    txt.Name = string.Format("{0}_txt", moduleType.Name);
                    txt.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                    y += lHeight + offset;

                    panl.Controls.Add(txt);


                } 
            }
            else
            {
                foreach (FieldInfo fieldInfo in fields)
                {

                    Label lbl = new Label();

                    lbl.Location = new System.Drawing.Point(x, y);
                    lbl.Name = string.Format("{0}_lbl", fieldInfo.Name);
                    lbl.Size = new System.Drawing.Size(lLabelWidth, lHeight);
                    lbl.Text = string.Format("{0}({1})", fieldInfo.Name, fieldInfo.FieldType.Name);
                    panl.Controls.Add(lbl);

                    if (fieldInfo.FieldType.IsArray)
                    {
                        ComplexTypeArrayButton btn = new ComplexTypeArrayButton();
                        btn.Tag = new ArrayList();
                        btn.ArrayType = fieldInfo.FieldType.GetElementType();

                        try
                        {
                            if (this.instance != null)
                            {
                                Object ret = this.instance.GetType().InvokeMember(fieldInfo.Name,
                                                    BindingFlags.Public | BindingFlags.NonPublic |
                                                    BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField,
                                                    null, this.instance, null);
                                if (ret != null && ret is Array)
                                {
                                    Array arr = (Array)ret;
                                    for (int i = 0; i < arr.Length; i++)
                                        ((ArrayList)btn.Tag).Add(arr.GetValue(i));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }


                        btn.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                        btn.Name = string.Format("{0}_btn", fieldInfo.Name);
                        btn.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);
                        btn.Text = string.Format("Add or Remove {0} Instance Array Objects", fieldInfo.FieldType.Name);
                        btn.Click += new EventHandler(arr_btn_Click);

                        y += lHeight + offset;
                        panl.Controls.Add(btn);
                    }
                    else if (fieldInfo.FieldType.Namespace.StartsWith("System") == false && fieldInfo.FieldType.IsClass)
                    {
                        Button btn = new ComplexTypeButton();
                        try
                        {
                            if (this.instance != null)
                            {
                                btn.Tag = this.instance.GetType().InvokeMember(fieldInfo.Name,
                                                    BindingFlags.Public | BindingFlags.NonPublic |
                                                    BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField,
                                                    null, this.instance, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            btn.Tag = null;
                        }
                        try
                        {
                            if (btn.Tag == null)
                            {
                                btn.Tag = Assembly.GetAssembly(fieldInfo.FieldType).CreateInstance(fieldInfo.FieldType.FullName);
                                if (btn.Tag != null && this.instance != null)
                                {
                                    this.instance.GetType().InvokeMember(fieldInfo.Name,
                                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetField |
                                    BindingFlags.Instance | BindingFlags.SetProperty, null, this.instance, new Object[] { btn.Tag });

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            continue;
                        }

                        btn.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                        btn.Name = string.Format("{0}_btn", fieldInfo.Name);
                        btn.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);
                        btn.Text = string.Format("Set {0} Instance Field Members", fieldInfo.FieldType.Name);
                        btn.Click += new EventHandler(cmplx_btn_Click);

                        y += lHeight + offset;
                        panl.Controls.Add(btn);
                    }
                    else if (fieldInfo.FieldType == typeof(bool) || fieldInfo.FieldType == typeof(Boolean))
                    {
                        CheckBox chk = new CheckBox();
                        if (this.instance != null)
                        {
                            chk.Tag = fieldInfo.GetValue(this.instance);
                            if (chk.Tag != null)
                            {
                                chk.Checked = (bool)chk.Tag;
                            }
                        }
                        chk.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                        chk.Name = string.Format("{0}_chk", fieldInfo.Name);
                        chk.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                        y += lHeight + offset;
                        panl.Controls.Add(chk);
                    }else if(fieldInfo.FieldType.IsEnum)
                    {
                        ComboBox cmb = new ComboBox();

                        cmb.DataSource = Enum.GetNames(fieldInfo.FieldType);
                        if (this.instance != null)
                        {
                            cmb.Tag = fieldInfo.GetValue(this.instance);
                            if (cmb.Tag != null)
                            {
                                cmb.Text = cmb.Tag.ToString();
                            }
                        }

                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                        cmb.Name = string.Format("{0}_cmb", fieldInfo.Name);
                        cmb.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);


                        y += lHeight + offset;
                        panl.Controls.Add(cmb);
                    }
                    else
                    {
                        TextBox txt = new TextBox();
                        try
                        {
                            if (this.instance != null)
                            {
                                txt.Tag = this.instance.GetType().InvokeMember(fieldInfo.Name,
                                                    BindingFlags.Public | BindingFlags.NonPublic |
                                                    BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.GetField,
                                                    null, this.instance, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            txt.Tag = null;
                        }
                        try
                        {

                            if (txt.Tag == null)
                            {
                                txt.Tag = Assembly.GetAssembly(fieldInfo.FieldType).CreateInstance(fieldInfo.FieldType.FullName);
                                if (txt.Tag != null && this.instance != null)
                                {
                                    this.instance.GetType().InvokeMember(fieldInfo.Name,
                                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetField |
                                    BindingFlags.Instance | BindingFlags.SetProperty, null, this.instance, new Object[] { txt.Tag });

                                }
                            }
                            else
                            {
                                txt.Text = txt.Tag.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        txt.Location = new System.Drawing.Point(x + lLabelWidth + offset, y);
                        txt.Name = string.Format("{0}_txt", fieldInfo.Name);
                        txt.Size = new System.Drawing.Size(lTextBoxWidth, lHeight);

                        y += lHeight + offset;

                        panl.Controls.Add(txt);


                    }
                }
            }

            panl.Width = x + lLabelWidth + lTextBoxWidth + LOCK_WIDTH + offset;
            panl.Height = y;

            this.Width = panl.Width;
            this.Height = panl.Height; 
            

        }

        

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
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
                    ((ComplexTypeArray)parent).ReLayoutChild(child, newCtl, bExpand);
                    child = parent;
                    parent = ((ComplexTypeArray)parent).parent;
                }
                else if (parent is ObjectPanel)
                {
                    ((ObjectPanel)parent).ReLayoutChild(child, newCtl, bExpand);
                    break;
                }
            }


        }
        void cmplx_btn_Click(object sender, EventArgs e)
        {
            ComplexTypeButton btn = (ComplexTypeButton)sender;

            int nChildIndex = this.panel1.Controls.GetChildIndex(btn);
            if (btn.FlatStyle == FlatStyle.Standard)
            {
                if (btn.UserControl == null)
                {
                    ComplexType cmplx = new ComplexType(this.clientAssembly, btn.Tag, this,isProperty);
                    cmplx.Name = string.Format("{0}_cmplx", btn.Tag.GetType().Name);
                    btn.UserControl = cmplx;
                }

                btn.UserControl.Location = new System.Drawing.Point(OFFSET, btn.Top + CONTROL_HEIGHT);
                this.panel1.Controls.Add(btn.UserControl);
                this.panel1.Controls.SetChildIndex(btn.UserControl, nChildIndex + 1);
                btn.FlatStyle = FlatStyle.Popup;
                btn.Text = string.Format("Click here to collapse");
                
                //this will relayout the child controls that were below the the complex btn control
                //as the child control has either expanded or collapsed by the user
                int maxWidth = 0;
                for (int i = 0; i < this.panel1.Controls.Count; i++)
                {
                    if (btn.UserControl != null && btn.UserControl != this.panel1.Controls[i] && this.panel1.Controls[i].Top > btn.Top)
                        this.panel1.Controls[i].Top += btn.UserControl.Height;

                    if ((this.panel1.Controls[i].Location.X + this.panel1.Controls[i].Width) > maxWidth)
                        maxWidth = this.panel1.Controls[i].Left + this.panel1.Controls[i].Width;
                }

                this.ClientSize = new Size(maxWidth + OFFSET, this.Height + btn.UserControl.Height);

                ResizeParent(btn.UserControl, true);
                btn.UserControl.GetAll(btn.Tag);

            }
            else
            {
                this.panel1.Controls.RemoveByKey(string.Format("{0}_cmplx", btn.Tag.GetType().Name));
                btn.FlatStyle = FlatStyle.Standard;
                btn.Text = string.Format("Set {0} Instance Field Members", btn.Tag.GetType().Name);
                int maxWidth = 0;
                for (int i = 0; i < this.panel1.Controls.Count; i++)
                {
                    if (this.panel1.Controls[i].Top > btn.Top)
                        this.panel1.Controls[i].Top -= btn.UserControl.Height;

                    if ((this.panel1.Controls[i].Location.X + this.panel1.Controls[i].Width) > maxWidth)
                        maxWidth = this.panel1.Controls[i].Left + this.panel1.Controls[i].Width;
                }
                this.ClientSize = new Size(maxWidth + OFFSET, this.Height - btn.UserControl.Height);

                ResizeParent(btn.UserControl, false);

                btn.UserControl.SetAll(btn.Tag);
            }
        }

        public void ReLayoutChild(Control child, Control newCtl, bool bExpand)
        {
            int maxHeight = 0, maxWidth = 0;
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
                this.ClientSize = new Size(maxWidth + OFFSET, this.Height+newCtl.Height);
            else
                this.ClientSize = new Size(maxWidth + OFFSET, this.Height-newCtl.Height);
            
        }

        void arr_btn_Click(object sender, EventArgs e)
        {
            ComplexTypeArrayButton btn = (ComplexTypeArrayButton)sender;
            int nChildIndex = this.panel1.Controls.GetChildIndex(btn);

            if (btn.FlatStyle == FlatStyle.Standard)
            {
                if (btn.UserControl == null)
                {
                    ComplexTypeArray arr = new ComplexTypeArray(this.clientAssembly, (ArrayList)btn.Tag, btn.ArrayType, this, isProperty);
                    arr.Name = string.Format("{0}_cmplx", btn.ArrayType.Name);
                    btn.UserControl = arr;
                }
                btn.UserControl.Location = new System.Drawing.Point(OFFSET, btn.Top + CONTROL_HEIGHT);
                this.panel1.Controls.Add(btn.UserControl);
                this.panel1.Controls.SetChildIndex(btn.UserControl, nChildIndex + 1);
                btn.FlatStyle = FlatStyle.Popup;
                btn.Text = string.Format("Click here to collapse");

                //this will relayout the child controls that were below the the complex btn control
                //as the child control has either expanded or collapsed by the user
                int maxWidth = 0;
                for (int i = 0; i < this.panel1.Controls.Count; i++)
                {
                    if (btn.UserControl != this.panel1.Controls[i] && this.panel1.Controls[i].Top > btn.Top)
                        this.panel1.Controls[i].Top += btn.UserControl.Height;

                    if ((this.panel1.Controls[i].Location.X + this.panel1.Controls[i].Width) > maxWidth)
                        maxWidth = this.panel1.Controls[i].Left + this.panel1.Controls[i].Width;
                }

                this.ClientSize = new Size(maxWidth + OFFSET, this.Height + btn.UserControl.Height);

                ResizeParent(btn.UserControl, true);

                btn.UserControl.GetAll();

            }
            else
            {
                this.panel1.Controls.RemoveByKey(string.Format("{0}_cmplx", btn.ArrayType.Name));
                btn.FlatStyle = FlatStyle.Standard;
                btn.Text = string.Format("Add or Remove {0} Instance Array Objects", btn.ArrayType.Name);
                
                int maxWidth = 0;
                for (int i = 0; i < this.panel1.Controls.Count; i++)
                {
                    if (this.panel1.Controls[i].Top > btn.Top)
                        this.panel1.Controls[i].Top -= btn.UserControl.Height;

                    if ((this.panel1.Controls[i].Location.X + this.panel1.Controls[i].Width) > maxWidth)
                        maxWidth = this.panel1.Controls[i].Left + this.panel1.Controls[i].Width;
                }
                this.ClientSize = new Size(maxWidth + OFFSET, this.Height - btn.UserControl.Height);

                ResizeParent(btn.UserControl, false);

                btn.UserControl.SetAll();               
            }


        }

        public void GetAll(Object instantiated)
        {
            object obj=null;
            if (IsBasicType(instantiated.GetType())&&this.panel1.Controls.Count>1)
            {
                if (this.panel1.Controls[1] is CheckBox)
                    ((CheckBox)this.panel1.Controls[1]).Checked = (bool)instantiated;
                else if (this.panel1.Controls[1] is TextBox)
                    this.panel1.Controls[1].Text = instantiated.ToString();
                else if (this.panel1.Controls[1] is ComboBox)
                    this.panel1.Controls[1].Text = instantiated.ToString();

            }
            else
            {
                FieldInfo[] fields = (FieldInfo[])instantiated.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (FieldInfo fInfo in fields)
                {
                    object ret = string.Empty;
                    if (fInfo.IsStatic) continue;

                    try
                    {
                        ret = fInfo.GetValue(instantiated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        continue;
                    }
                    if (ret == null) continue;

                    int indx = -1;
                    if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_cmb", fInfo.Name))) == -1)
                        if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_chk", fInfo.Name))) == -1)
                        if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_txt", fInfo.Name))) == -1)
                            if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_btn", fInfo.Name))) == -1)
                                continue;
                    if (this.panel1.Controls[indx] is ComboBox)
                        ((ComboBox)this.panel1.Controls[indx]).Text = ret.ToString();
                    else if (this.panel1.Controls[indx] is CheckBox)
                        ((CheckBox)this.panel1.Controls[indx]).Checked = (bool)ret;
                    else if (this.panel1.Controls[indx] is TextBox)
                        this.panel1.Controls[indx].Text = ret.ToString();
                    else if (this.panel1.Controls[indx] is ComplexTypeButton)
                    {
                        ComplexTypeButton cmplxBtn = (ComplexTypeButton)this.panel1.Controls[indx];
                        this.panel1.Controls[indx].Tag = ret;
                        if (cmplxBtn.UserControl != null)
                            cmplxBtn.UserControl.GetAll(ret);
                    }
                    else if (this.panel1.Controls[indx] is ComplexTypeArrayButton)
                    {
                        ComplexTypeArrayButton arrBtn = (ComplexTypeArrayButton)this.panel1.Controls[indx];
                        Array arr = (Array)ret;
                        ((ArrayList)this.panel1.Controls[indx].Tag).Clear();
                        for (int i = 0; i < arr.Length; i++)
                            ((ArrayList)arrBtn.Tag).Add(arr.GetValue(i));

                        if (arrBtn.UserControl != null)
                            arrBtn.UserControl.GetAll();

                    }

                }
            }
        }

        public void SetAll(Object instantiated)
        {
           if (IsBasicType(instantiated.GetType()) && this.panel1.Controls.Count > 1)
           {
               if (this.panel1.Controls[1] is CheckBox && instantiated is bool)
                   instantiated = (Object)((CheckBox)this.panel1.Controls[1]).Checked; 
               else if (this.panel1.Controls[1] is TextBox)
                   instantiated = ConvertTextToParameterType(this.panel1.Controls[1].Text, instantiated.GetType());
               else if (this.panel1.Controls[1] is ComboBox)
                   instantiated = Enum.Parse(instantiated.GetType(), this.panel1.Controls[1].Text);
           }
           else
           {
               FieldInfo[] fields = instantiated.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
               foreach (FieldInfo fInfo in fields)
               {
                   int indx = -1;
                   if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_cmb", fInfo.Name))) == -1)
                       if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_chk", fInfo.Name))) == -1)
                       if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_txt", fInfo.Name))) == -1)
                           if ((indx = this.panel1.Controls.IndexOfKey(string.Format("{0}_btn", fInfo.Name))) == -1)
                               continue;

                   Object paramObject = null;
                   if (this.panel1.Controls[indx] is ComboBox)
                   {
                       paramObject = Enum.Parse(fInfo.FieldType, this.panel1.Controls[indx].Text);
                   }else if (this.panel1.Controls[indx] is CheckBox)
                   {
                       paramObject = (Object)((CheckBox)this.panel1.Controls[indx]).Checked;
                   }else if (this.panel1.Controls[indx] is TextBox)
                   {
                       paramObject = ConvertTextToParameterType(this.panel1.Controls[indx].Text, fInfo.FieldType);
                   }else if (this.panel1.Controls[indx] is ComplexTypeButton)
                   {
                       ComplexTypeButton cmplxBtn = (ComplexTypeButton)this.panel1.Controls[indx];
                       if (cmplxBtn.UserControl != null && cmplxBtn.Tag != null)
                       {
                           cmplxBtn.UserControl.SetAll(cmplxBtn.Tag);
                       }

                       paramObject = this.panel1.Controls[indx].Tag;
                   }else if (this.panel1.Controls[indx] is ComplexTypeArrayButton)
                   {
                       ArrayList arrList = (ArrayList)this.panel1.Controls[indx].Tag;

                       paramObject = arrList.ToArray(((ComplexTypeArrayButton)this.panel1.Controls[indx]).ArrayType);

                   }


                   if (paramObject == null)
                   {
                       MessageBox.Show(string.Format("Type '{0}' not supported. ", fInfo.FieldType.Name));
                       continue;
                   }
                   try
                   {

                       fInfo.SetValue(instantiated, paramObject);
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }

               }
            }
        }

        public void SoapComplexType_Load(object sender, EventArgs e)
        {
            if(this.instance!=null)
                GetAll(this.instance);
        }

        public void SoapComplexType_Leave(object sender, EventArgs e)
        {
            if (this.instance != null)
                SetAll(this.instance);
        }

        public static bool IsBasicType(Type type)
        {
            if (type.IsEnum) return true;

            switch (type.Name)
            {
                case "string":
                case "String":
                case "bool":
                case "Boolean":
                case "int":
                case "Int32":
                case "Int16":
                case "Int64":
                case "double":
                    return true;

            }
            return false;
        }
        
        public static Object ConvertTextToParameterType(string value, Type paramType)
        {
            Object ret = null;
            try
            {
                TypeConverter tc = TypeDescriptor.GetConverter(paramType);
                if (tc != null)
                    ret = tc.ConvertFromString(value);
            }
            catch (Exception ex)
            {
            }

            return ret;
        }
    }
    public class ComplexTypeButton : Button
    {
        private ComplexType userControl = null;

        public ComplexType UserControl
        {
            get { return userControl; }
            set { userControl = value; }
        }


    }
}

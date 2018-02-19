using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;

namespace CustomControls
{
    /// <summary>
    /// (eraghi)
    /// Extended CheckedListBox with binding facilities (Value property)
    /// </summary>
    [ToolboxBitmap(typeof(CheckedListBox))]
    public class ExCheckedListBox : CheckedListBox
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExCheckedListBox()
        {
            this.CheckOnClick = true;
           
        }

       
        
        /// <summary>
        ///    Gets or sets the property to display for this CustomControls.CheckedListBox.
        ///
        /// Returns:
        ///     A System.String specifying the name of an object property that is contained
        ///     in the collection specified by the CustomControls.CheckedListBox.DataSource
        ///     property. The default is an empty string ("").
        /// </summary>
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        public new string DisplayMember
        {
            get
            {
                return base.DisplayMember;
            }
            set
            {
                base.DisplayMember = value;
                
            }
        }
       
        /// <summary>
        /// Gets or sets the data source for this CustomControls.CheckedListBox.
        /// Returns:
        ///    An object that implements the System.Collections.IList or System.ComponentModel.IListSource
        ///    interfaces, such as a System.Data.DataSet or an System.Array. The default
        ///    is null.
        ///
        ///Exceptions:
        ///  System.ArgumentException:
        ///    The assigned value does not implement the System.Collections.IList or System.ComponentModel.IListSource
        ///    interfaces.
        /// </summary>
        [DefaultValue("")]
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public new object DataSource { 
            get 
            {
                return base.DataSource;
            }
            set 
            {
                base.DataSource = value;
                
            }
        }

        private int value;

        /// <summary>
        /// Gets and sets an integer value based on checked items(bitwise)
        /// </summary>
        [Bindable(true), Browsable(true)]
        public int Value
        {
            get
            {
                ///Gets checked items in decimal mode from binary mode
                
                try
                {
                    //each item in list has a number that is binary number in decimal mode
                    //this number represents that number
                    int poweredNumber = 1;
                    //loop in all items of list
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        //if item checked and the value doesn't contains poweredNumber then
                        //add poweredNumber to the value
                        if((this.GetItemChecked(i)))
                            this.value |= poweredNumber;
                        //else if poweredNumber exists in the value remove from it
                        else if ((this.value & poweredNumber) != 0)
                            this.value -= poweredNumber;

                        //raise to the power
                        poweredNumber *= 2;
                    }
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                return this.value;
            }
            set
            {
                ///sets checked items from binary mode converted from decimal value

                this.value = value;
                try
                {
                    //each item in list has a number that is binary number in decimal mode
                    //this number represents that number
                    int poweredNumber = 1;
                    //loop in all items of list
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        //if poweredNumber exists in the value set checked on item
                        if ((this.value & poweredNumber) != 0)
                            this.SetItemCheckState(i, CheckState.Checked);
                        //else remove checked from item
                        else
                            this.SetItemCheckState(i, CheckState.Unchecked);

                        //raise to the power
                        poweredNumber *= 2;
                    }
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }

        

     

    }
}

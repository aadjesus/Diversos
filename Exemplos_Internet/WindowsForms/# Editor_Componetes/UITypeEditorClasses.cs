/*------------------------------------------------------------------
Classes BaseDropDownListTypeEditor and BaseControlTypeEditor, using C#
Written by Peter L. Blum (www.PeterBlum.com)
Free to distribute and reuse. Release: June 22, 2002
1.0.1 Bug fix: When nested in a naming container like a user control or panel,
      the IDs shown are not the ones set by the user.

--- OVERVIEW -------------------------------------------------------
BaseDropDownListTypeEditor is an implementation of the UITypeEditor that
can be used to develop Drop Down List style UITypeEditors for the Visual Studio.Net
property editor. It sets up the interface with a window form List Box. You
must subclass and override the method FillInList.

BaseControlTypeEditor is an implementation of the BaseDropDownListTypeEditor that provides
a drop down list of controls found on the current WebForm.
It is very similar to the one found on BaseValidator.ControlToValidate in that
it lists and assigns controls by their ID.
However, you use this class to limit the list to certain types.
You must inherit from BaseControlTypeEditor and override DefineSupportedTypes()
to select the types supported. When a type is supported, so are all of the subclasses
of that type. So if you want to list every control, call AddSupportedType(typeof(Control)).
This UITypeEditor expects the property containing a control ID to be a string type.

Use this as:
* For your own controls that have properties for associating another control's ID
* A sample for implementing a drop down style UITypeEditor
NOTE: It would be great for any custom BaseValidator classes you write that associate
BaseValidator.ControlToValidate with a very specific control class list. However,
dotNet 1.0 doesn't permit overriding ControlToValidate. Thus this property cannot be assigned. Bummer.

--- USING THIS CODE ----------------------------------------------------------
1. Add this file to a web project that has a custom control which needs it.

2. In the file with that custom control (as a suggestion), create a subclass of the desired base
   class.
   
FOR BaseDropDownListTypeEditor, override the FillInList method. Here is an example:

   public class SampleControlTypeEditor : BaseDropDownListTypeEditor
   {
      protected override void FillInList(ITypeDescriptorContext pContext, IServiceProvider pProvider, ListBox pListBox)
      {
         pListBox.Items.Add("String 1");
         pListBox.Items.Add("String 2");\
         // you can also customize the attributes of pListBox here. If you don't have strings,
         // the Windows Forms ListBox supports alternative drawing methods to handle whatever
         // objects you add.
      }  // DefineSupportedTypes()
   }  // class SampleControlTypeEditor

FOR BaseControlTypeEditor, override the DefineSupportedTypes method. Here is an example
   that limits to the ListBox and DropDownList web controls.
   
   public class SampleControlTypeEditor : BaseControlTypeEditor
   {
      protected override void DefineSupportedTypes()
      {
         AddSupportedType(typeof(System.Web.UI.WebControls.ListBox));
         AddSupportedType(typeof(System.Web.UI.WebControls.DropDownList));
      }  // DefineSupportedTypes()
   }  // class SampleControlTypeEditor

3. Associate the TypeEditor to your property.

class MyControl ...
{
   [Editor(typeof(MyControlTypeEditor), typeof(UITypeEditor))]
   public string MyOtherControlID { get {...;} set {...;} }
...
}

------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using System.Collections;

namespace PeterBlum.UITypeEditorClasses
{
// ----- CLASS BaseDropDownListTypeEditor ----------------------------------------
/// <remarks>
/// BaseDropDownListTypeEditor can be subclassed to build UITypeEditors that show
/// a drop down list in the Visual Studio.Net property editor.
/// You only need to override the FillInList method.
/// </remarks>
   public abstract class BaseDropDownListTypeEditor : UITypeEditor
   {
// fEdSrc is created and nulled in EditValue. It is here only to allow the value
// to be shared with the List_Click event handler
      private IWindowsFormsEditorService fEdSvc = null;
//------------------------------------------------------------------------
/// <summary>
/// GetEditStyle must be overridden for any UITypeEditor.
/// In this case, we are using a DropDown style.
/// </summary>
      public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext pContext)
      {
         if (pContext != null && pContext.Instance != null) 
         {
            return UITypeEditorEditStyle.DropDown;
         }
         return base.GetEditStyle(pContext);
      }  // GetEditStyle()

//------------------------------------------------------------------------
/// <summary>
/// EditValue must be overridden from UITypeEditor to install a control that appears in the drop down.
/// For UITypeEditorEditStyle.DropDown, here is the general procedure:
/// 1. Get the Edit Service from pProvider.GetService(typeof(IWindowsFormsEditorService)).
///    It contains methods to run the DropDown and Forms interfaces.
/// 2. Get a Window control instance that reflects the UI you want. In this case, its a ListBox.
///    If you wanted multiple controls, consider something like the Panel class and add controls to its Controls list.
/// 3. We want the ListBox to close on a Click event much like boolean and enum dropdowns do.
///    So we set up a Click event handler that calls CloseDropDown on the Edit Service.
/// 4. Add data to the ListBox.
/// 5. Set the initial value of the list. pValue contains that value.
/// 6. Let the Edit Service open and manage the DropDown interface.
/// 7. Return the value from the list.
/// </summary>
      public override object EditValue(ITypeDescriptorContext pContext, IServiceProvider pProvider, object pValue)
      {
         if (pContext != null
            && pContext.Instance != null
            && pProvider != null) 
            try
            {
               // get the editor service
               fEdSvc = (IWindowsFormsEditorService)
                  pProvider.GetService(typeof(IWindowsFormsEditorService));

               // create the control(s) we want for the UI
               ListBox vListBox = new ListBox();
               vListBox.Click += new EventHandler(List_Click);
            
               // modify the list's properties including the Item list
               FillInList(pContext, pProvider, vListBox);

               // initialize the selection on the list
               vListBox.SelectedItem = pValue;
         
               // let the editor service place the list on screen and manage its events
               fEdSvc.DropDownControl(vListBox);
   
               // return the updated value;
               return vListBox.SelectedItem;
            }  // try
            finally
            {
               fEdSvc = null;
            }
         else
            return pValue;

      }  // EditValue

/// <summary>
/// List_Click is a click event handler for the ListBox. We want the have the list
/// close when the user clicks, just like the Enum and Bool types do in their UITypeEditors.
/// </summary>
      protected void List_Click(object pSender, EventArgs pArgs)
      {
         fEdSvc.CloseDropDown();
      }  // List_Click

/// <summary>
/// FillInList must be overriden to supply properties to the ListBox.
/// It is essential to assign objects to the ListBox.Items collection.
/// You can also customize other properties.
/// Your data does not need to be strings. Window forms list boxes offer
/// customizable drawing rules which you can use to represent any kind of object. 
/// </summary>
      protected abstract void FillInList(ITypeDescriptorContext pContext, IServiceProvider pProvider, ListBox pListBox);

   } //  class BaseDropDownListTypeEditor

// ----- CLASS BaseControlTypeEditor ------------------------------------------
/// <remarks>
/// BaseControlTypeEditor is an abstract implementation of BaseDropDownListTypeEditor
/// to show a list of controls found on the page. It must be associated with a string type
/// property that hosts a control's ID. The list shows controls by their ID.
/// Subclass and override DefineSupportedTypes to determine which class types are shown in the list.
/// </remarks>
   public abstract class BaseControlTypeEditor : BaseDropDownListTypeEditor
   {
/// <summary>
/// xTypesSupported is a list of Types supported by the drop list.
/// The drop list will look through the page's component list and select any
/// object whose classtype matches or is inherited from the types in this list.
/// For example, to include all WebControls, you can simply add the WebControl type here.
/// To add to this list, you must override DefineSupportedTypes and
/// call AddSupportedType (which validates your entry.)
/// </summary>
      protected ArrayList xTypesSupported
      {
         get { return fTypesSupported; }
      }
      private ArrayList fTypesSupported = new ArrayList();

//----------------------------------------------------------------------------
/// <summary>
/// AddSupportedType adds a type into the xTypesSupported list. Call it from
/// DefineSupportedTypes. All subclasses of any type added are automatically supported.
/// </summary>
      protected void AddSupportedType(object pType)
      {
         if (pType is Type)
            fTypesSupported.Add(pType);
         else
            throw new ArgumentException("AddSupportedType requires that you pass a class type. For example, typeof(MyClass).");
      }  // AddSupportedType()

//----------------------------------------------------------------------------
/// <summary>
/// DefineSupportedTypes must be overridden. Use it to call AddSupportedType with Control type that you support.
/// Remember that all subclasses of any type added are automatically supported.
/// For example, if you want to include all HtmlControls, call AddSupportedTypes(typeof(HtmlControl)).
/// </summary>
      protected abstract void DefineSupportedTypes();

//----------------------------------------------------------------------------
/// <summary>
/// FillInList box gathers the controls found on the page and after filtering out those
/// not in xTypesSupported, adds their ClientID to the list box.
/// The ITypeDescriptorContext, pContext, contains the components list of the page.
/// </summary>
      protected override void FillInList(ITypeDescriptorContext pContext, IServiceProvider pProvider, ListBox pListBox)
      {
         // fill it in with the components from the page
         DefineSupportedTypes();
         ComponentCollection vComponents = pContext.Container.Components;
         FillInListWithControls(vComponents, pListBox);
      }  // FillInList()

//----------------------------------------------------------------------------
/// <summary>
/// FillInListWithControls does most of the work of FillInList.
/// </summary>
      protected void FillInListWithControls(ICollection pControls, ListBox pListBox)
      {
         foreach (object vControl in pControls)
            if (vControl is System.Web.UI.Control)
            {
               System.Web.UI.Control vWebControl = (System.Web.UI.Control)vControl;
               bool vSupportedB = false;
               for (int vI = 0; !vSupportedB && (vI < fTypesSupported.Count); vI++)
               {
                  if (vWebControl.GetType().IsAssignableFrom((Type)fTypesSupported[vI]))
                     vSupportedB = true;
               }  // for vI

               if (vSupportedB)
                  if (vWebControl.ID != "")  // !bf 7/25 supply the actual user ID
                     pListBox.Items.Add(vWebControl.ID);
                  else if (vWebControl.ClientID != "")
                     pListBox.Items.Add(vWebControl.ClientID);
               /*
               if (vWebControl.Controls.Count > 0)
                  FillInListWithControls(vWebControl.Controls, pListBox);  // !! RECURSION !!
               */
            }  // if/foreach
      }  // FillInListWithControls
       
   }  // class BaseControlTypeEditor

}  // namespace PeterBlum.UITypeEditorClasses

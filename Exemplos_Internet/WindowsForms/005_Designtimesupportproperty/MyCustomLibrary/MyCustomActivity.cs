using System.ComponentModel;
using System;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Drawing.Design;
using MyCustomLibrary;

namespace CustomActivityTester
{
    public partial class MyCustomActivity : System.Workflow.ComponentModel.Activity
    {
        public MyCustomActivity()
        {
            InitializeComponent();
        }
        
        public static DependencyProperty FilePathProperty = System.Workflow.ComponentModel.DependencyProperty.Register(
        "FilePath", typeof(string), typeof(MyCustomActivity));

        [Description("FilePath")]
        [Category("MyCustomActivity")]
        [Browsable(true)]
        [DefaultValueAttribute("False")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(DriveEditor), typeof(UITypeEditor))]
        public string FilePath
        {
            get
            {
                return ((string)base.GetValue(MyCustomActivity.FilePathProperty));
            }
            set
            {

                base.SetValue(MyCustomActivity.FilePathProperty, value);

            }
        }
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine(string.Concat("The File path is: ", this.FilePath));
            return ActivityExecutionStatus.Closed;
        }
    }

    public class DriveEditor : UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle
        GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // We will use a window for property editing. 
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(
        System.ComponentModel.ITypeDescriptorContext context,
        System.IServiceProvider provider, object value)
        {
            FilePathDesignSupport frm = new FilePathDesignSupport();

            //Show the UI to set up the property
            frm.ShowDialog();

            // Return the the file name of the file selected. 
            return frm.FileName;
        }

        public override bool GetPaintValueSupported(
        System.ComponentModel.ITypeDescriptorContext context)
        {
            // No special thumbnail will be shown for the grid. 
            return false;
        }

    }


}

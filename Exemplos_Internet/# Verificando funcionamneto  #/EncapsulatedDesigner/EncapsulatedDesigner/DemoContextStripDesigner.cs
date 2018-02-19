using System.ComponentModel;
using System.ComponentModel.Design;

namespace EncapsulatedDesigner
{
    internal class DemoContextStripDesigner : ToolStripDropDownDesigner
    {
        private bool myVar;

        public bool MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            PropertyDescriptor pd = TypeDescriptor.CreateProperty(GetType(), "MyProperty", typeof(bool), 
                new DescriptionAttribute("Designer Property"), new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));

            properties.Add(pd.Name, pd);
        }


        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // remove all standard entries except 'Edit Items'
                DesignerActionListCollection actionLists = base.ActionLists;
                actionLists.RemoveAt(0);
                return actionLists;
            }
        }
    }
}

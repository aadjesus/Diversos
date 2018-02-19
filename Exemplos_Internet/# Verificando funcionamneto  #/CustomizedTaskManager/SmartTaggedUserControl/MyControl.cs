using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace WinTips
{
    [Designer(typeof(myControlDesigner))]
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();
        }
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        public string LabelText
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            IEnumerable<string> filtered =
                           from a in cities
                           where a.StartsWith(label1.Text)
                           orderby a
                           select a; 
            if (filtered.Count() > 0)
                listBox1.Items.AddRange(filtered.ToArray());
        }
        string[] cities;
        private void MyControl_Load(object sender, EventArgs e)
        {
            cities = new string[] { "NewYork", "Mumbai", "Belgrade", "Los Angeles", "Chennai", "New Delhi", "Madrid", "Chicago",
                                  "Las Vegas", "Bangalore", "Sydney", "Melbourne", "Beijing", "Calcutta", "Milan", "Austin", "Brisbane" };

        }
    }
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class myControlDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new myControlActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
    public class myControlActionList : System.ComponentModel.Design.DesignerActionList
    {
        private MyControl  colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public myControlActionList(IComponent component)
            : base(component)
        {
            this.colUserControl = component as MyControl;

            // Cache a reference to DesignerActionUIService, so the DesigneractionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        // Properties that are targets of DesignerActionPropertyItem entries.
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }
        public string LabelText
        {
            get
            {
                return colUserControl.LabelText;
            }
            set
            {
                GetPropertyByName("LabelText").SetValue(colUserControl, value);
            }
        }

        // Implementation of this abstract method creates smart tag  items, associates their targets, and collects into list.
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));
            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));
            items.Add(new DesignerActionPropertyItem("LabelText",
                                 "Label Text", "Appearance",
                                 "Type few characters to filter Cities."));
            return items;
        }
    }
}

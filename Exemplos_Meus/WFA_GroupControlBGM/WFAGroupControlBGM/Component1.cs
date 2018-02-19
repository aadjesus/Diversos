using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel.Design;

namespace WFAGroupControlBGM
{
    #region 1
    
    public partial class Component1 : ContainerControl
    {
        public Component1()
        {            
            InitializeComponent();
            _panel1 = new SplitterPanelx(this);
            _panel1.BackColor = Color.Black;
            this.Controls.Add(_panel1);
        }

        public Component1(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        SplitterPanelx _panel1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SplitterPanelx Panel1
        {
            get { return _panel1; }
        }

        public sealed class SplitterPanelx : Panel
        {
            public SplitterPanelx(Component1 owner)
            {
                owner.Controls.Add(this);
            }
        }

    }

    #endregion

    // ADICIONA propriedade em tempo de desainer
    public class ExampleControlDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        private bool mouseover = false;
        private Color lineColor = Color.White;

        public Color OutlineColor
        {
            get
            {
                return lineColor;
            }
            set
            {
                lineColor = value;
            }
        }

        Int16 xxxxx;

        public Int16 Xxxxx
        {
            get { return xxxxx; }
            set { xxxxx = value; }
        }

        public ExampleControlDesigner()
        {
        }

        protected override void OnMouseEnter()
        {
            this.mouseover = true;
            this.Control.Refresh();
        }

        // Sets a value and refreshes the control's display when the 
        // mouse position enters the area of the control.        
        protected override void OnMouseLeave()
        {
            this.mouseover = false;
            this.Control.Refresh();
        }

        // Draws an outline around the control when the mouse is 
        // over the control.    
        protected override void OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
        {
            if (this.mouseover)
                pe.Graphics.DrawRectangle(new Pen(new SolidBrush(this.lineColor), 6), 0, 0, this.Control.Size.Width, this.Control.Size.Height);
        }

        // Adds a property to this designer's control at design time 
        // that indicates the outline color to use.
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            properties.Add("OutlineColor", TypeDescriptor.CreateProperty(typeof(ExampleControlDesigner), "OutlineColor", typeof(System.Drawing.Color), null));
            properties.Add("Xxxxx", TypeDescriptor.CreateProperty(typeof(ExampleControlDesigner), "Xxxxx", typeof(Int16), null));
        }
    }

}

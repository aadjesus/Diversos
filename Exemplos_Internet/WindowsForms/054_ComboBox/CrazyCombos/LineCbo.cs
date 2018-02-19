using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    class LineCbo : ComboBox 
    {
        private string[] DashStyles = Enum.GetNames(typeof(DashStyle)); //Get Current System.Drawing.Drawing2D.DashStyles

        private int i = 0; //Counter

        /// <summary>
        /// Constructor Setting Startup Settings
        /// </summary>
        public LineCbo()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed; //DrawMode

            this.DropDownStyle = ComboBoxStyle.DropDownList; //Style Of ComboBox

            LCFillLineTypes(); //Call FillLineTypes Method
        } 

        /// <summary>
        /// Adds All DashStyles To List
        /// </summary>
        private void LCFillLineTypes() 
        { 
            this.Items.Clear(); //Clear All Items

            for (int i = 0; i <= DashStyles.Length - 1; i++) //Loop Through All DashStyles
            { 
                
                this.Items.Add(DashStyles[i]); //Add Each DashStyle

            }    
        } 

        /// <summary>
        /// Override OnDrawItem, To Draw The Lines
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e) 
        { 
            base.OnDrawItem(e); 

            if (e.Index >= 0) //If Valid List
            { 
                Rectangle LCCurrItemRect = e.Bounds; //Create New rectangle For New Item

                Pen LCDrawPen = new Pen(Color.Black, 2); //Set Drawing Pen

                if (i <= DashStyles.Length - 1) //If Valid DashStyle
                {
                    LCDrawPen.DashStyle = (DashStyle)System.Enum.Parse(typeof(DashStyle), DashStyles[i]); //Set Pen's DashStyle To Current DashStyle

                    //Draw The Current Line With Appropriate DashStyle
                    e.Graphics.DrawLine(LCDrawPen, Convert.ToSingle(LCCurrItemRect.X), Convert.ToSingle(LCCurrItemRect.Y + LCCurrItemRect.Height / 2), Convert.ToSingle(LCCurrItemRect.X + LCCurrItemRect.Width), Convert.ToSingle(LCCurrItemRect.Y + LCCurrItemRect.Height / 2));

                    i++; //Increment Counter
                }
                if (i >= DashStyles.Length - 1) //If Out of Range
                    i = 0; //Reset to 0, To Loop the Drawing
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdvancedControls
{
    /// <summary>
    /// This Control Used To Handle Form Move If The Form Control Box
    /// Is Set To False
    /// Or If Form BorderStyle Is Set To None
    /// By Another Way If We Need To Handle Moving Form By Code
    /// </summary>
    public partial class HandelFormMove : Control
    { 
        
        private Point mouseOffset;
        private bool isMouseDown = false;
        System.Windows.Forms.Form fr;
        public HandelFormMove()
        {
            InitializeComponent();  
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
           // base.OnPaint(pe);
        }
       

        /// <summary>
        /// Initialize Form Moving by initialize the from postion
        /// And Set The Flage (isMouseDown) = true 
        /// to Enable Moving Form With Mouse Moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;
           // MessageBox.Show("click");
            if (e.Button == MouseButtons.Left)
            {
                // Assign coordinates to mouseOffset variable based on 
                // current position of the mouse pointer.
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                    SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }    
        }

        /// <summary>
        /// check the flage (isMouseDown) if it is true
        /// change form postion acording to mouse postion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_MouseMove(object sender, MouseEventArgs e)
        {
            // If the left mouse is held down.
            if (isMouseDown)
            {
                // Set the form's location property to the new position.
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
               fr.Location = mousePos;
            }

        }

        /// <summary>
        /// finalize Form Moving 
        /// by Seting The Flage (isMouseDown) = false 
        /// to disable Moving Form With Mouse Moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_MouseUp(object sender, MouseEventArgs e)
        {
            // Changes the isMouseDown field so that the form does
            // not move unless the user is pressing the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnCreateControl()
        {
            //base.OnCreateControl();
            fr = (System.Windows.Forms.Form)this.Parent;
            fr.MouseDown += new MouseEventHandler(Parent_MouseDown);
            fr.MouseMove += new MouseEventHandler(Parent_MouseMove);
            fr.MouseUp += new MouseEventHandler(Parent_MouseUp);
            this.Visible = false;
        }
    }
}

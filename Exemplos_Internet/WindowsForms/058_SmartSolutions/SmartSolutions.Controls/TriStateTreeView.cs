using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SmartSolutions.Controls
{
    /// <summary>
    /// Tri-State implementation of the common TreeView control in .NET 1.1
    /// Some of this code was translated from a VB sample about CustomDraw treeviews
    /// on CodeProject. Author: Carlos J. Quintero
    /// http://www.codeproject.com/KB/cpp/CustomDrawTreeview.aspx
    /// </summary>
    public class TriStateTreeView : TreeView
    {
        private int indexUnchecked;
        private int indexChecked;
        private int indexIndeterminate;
        private bool useCustomImages;

        public TriStateTreeView() : base() { }

        [Category("CheckState")]
        [DefaultValue(false)]
        public bool UseCustomImages
        {
            get { return this.useCustomImages; }
            set { this.useCustomImages = value; }
        }

        [Category("CheckState")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue(0)]
        public int CheckedImageIndex
        {
            get
            {
                if (base.ImageList == null)
                    return -1;
                if (this.indexChecked >= this.ImageList.Images.Count)
                    return Math.Max(0, this.ImageList.Images.Count - 1);
                return this.indexChecked;
            }
            set
            {
                if (value == -1)
                    value = 0;
                if (value < 0)
                    throw new ArgumentException(string.Format("Index out of bounds! ({0}) index must be equal to or greater then {1}.", value.ToString(), "0"));
                if (this.indexChecked != value)
                {
                    this.indexChecked = value;
                    if (base.IsHandleCreated)
                        base.RecreateHandle();
                }
            }
        }

        [Category("CheckState")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue(0)]
        public int UncheckedImageIndex
        {
            get
            {
                if (base.ImageList == null)
                    return -1;
                if (this.indexUnchecked >= this.ImageList.Images.Count)
                    return Math.Max(0, this.ImageList.Images.Count - 1);
                return this.indexUnchecked;
            }
            set
            {
                if (value == -1)
                    value = 0;
                if (value < 0)
                    throw new ArgumentException(string.Format("Index out of bounds! ({0}) index must be equal to or greater then {1}.", value.ToString(), "0"));
                if (this.indexUnchecked != value)
                {
                    this.indexUnchecked = value;
                    if (base.IsHandleCreated)
                        base.RecreateHandle();
                }
            }
        }

        [Category("CheckState")]
        [TypeConverter(typeof(TreeViewImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue(0)]
        public int IndeterminateImageIndex
        {
            get
            {
                if (base.ImageList == null)
                    return -1;
                if (this.indexIndeterminate >= this.ImageList.Images.Count)
                    return Math.Max(0, this.ImageList.Images.Count - 1);
                return this.indexIndeterminate;
            }
            set
            {
                if (value == -1)
                    value = 0;
                if (value < 0)
                    throw new ArgumentException(string.Format("Index out of bounds! ({0}) index must be equal to or greater then {1}.", value.ToString(), "0"));
                if (this.indexIndeterminate != value)
                {
                    this.indexIndeterminate = value;
                    if (base.IsHandleCreated)
                        base.RecreateHandle();
                }
            }
        }

        private struct RECT
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;
        }

        private struct NMHDR
        {
            internal IntPtr hwndFrom;
            internal IntPtr idFrom;
            internal int code;
        }

        private struct NMCUSTOMDRAW
        {
            internal NMHDR hdr;
            internal int dwDrawStage;
            internal IntPtr hdc;
            internal RECT rc;
            internal IntPtr dwItemSpec;
            internal int uItemState;
            internal IntPtr lItemlParam;
        }

        private struct NMTVCUSTOMDRAW
        {
            internal NMCUSTOMDRAW nmcd;
            internal int clrText;
            internal int clrTextBk;
            internal int iLevel;
        }

        private int HandleNotify(Message msg)
        {
            const int NM_FIRST = 0;
            const int NM_CUSTOMDRAW = NM_FIRST - 12;

            // Drawstage
            const int CDDS_PREPAINT = 0x1;
            const int CDDS_POSTPAINT = 0x2;

            const int CDDS_ITEM = 0x10000;
            const int CDDS_ITEMPREPAINT = (CDDS_ITEM | CDDS_PREPAINT);
            const int CDDS_ITEMPOSTPAINT = (CDDS_ITEM | CDDS_POSTPAINT);

            // Custom draw return flags
            const int CDRF_DODEFAULT = 0x0;
            const int CDRF_NOTIFYPOSTPAINT = 0x10;
            const int CDRF_NOTIFYITEMDRAW = 0x20;

            NMHDR tNMHDR;
            NMTVCUSTOMDRAW tNMTVCUSTOMDRAW;
            int iResult = 0;
            object obj;
            TreeNode node;
            TriStateTreeNode tsNode;

            try
            {
                if (!msg.LParam.Equals(IntPtr.Zero))
                {
                    obj = msg.GetLParam(typeof(NMHDR));
                    if (obj is NMHDR)
                    {
                        tNMHDR = (NMHDR)obj;
                        if (tNMHDR.code == NM_CUSTOMDRAW)
                        {
                            obj = msg.GetLParam(typeof(NMTVCUSTOMDRAW));
                            if (obj is NMTVCUSTOMDRAW)
                            {
                                tNMTVCUSTOMDRAW = (NMTVCUSTOMDRAW)obj;
                                switch (tNMTVCUSTOMDRAW.nmcd.dwDrawStage)
                                {
                                    case CDDS_PREPAINT:
                                        iResult = CDRF_NOTIFYITEMDRAW;
                                        break;
                                    case CDDS_ITEMPREPAINT:
                                        iResult = CDRF_NOTIFYPOSTPAINT;
                                        break;
                                    case CDDS_ITEMPOSTPAINT:
                                        node = TreeNode.FromHandle(this, tNMTVCUSTOMDRAW.nmcd.dwItemSpec);
                                        tsNode = node as TriStateTreeNode;
                                        if (tsNode != null)
                                        {
                                            Graphics graph = Graphics.FromHdc(tNMTVCUSTOMDRAW.nmcd.hdc);
                                            PaintTreeNode(tsNode, graph);
                                            graph.Dispose();
                                        }
                                        iResult = CDRF_DODEFAULT;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return iResult;
        }

        /// <summary>
        /// Paints the node specified.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="gx"></param>
        private void PaintTreeNode(TriStateTreeNode node, Graphics gx)
        {
            if (this.CheckBoxes)
            {
                // calculate boundaries
                Rectangle ncRect = new Rectangle(node.Bounds.X - 35, node.Bounds.Y, 15, 15);

                // make sure the default checkboxes are gone
                ClearCheckbox(ncRect, gx);

                // draw lines, if we are supposed to
                if (this.ShowLines)
                {
                    DrawNodeLines(node, ncRect, gx);
                }

                if (node.CheckboxVisible)
                {
                    // now draw the checkboxes
                    switch (node.CheckState)
                    {
                        case CheckState.Unchecked:			// Normal
                            DrawCheckbox(ncRect, gx, ButtonState.Normal | ButtonState.Flat);
                            break;
                        case CheckState.Checked:			// Checked
                            DrawCheckbox(ncRect, gx, ButtonState.Checked | ButtonState.Flat);
                            break;
                        case CheckState.Indeterminate:		// Pushed
                            DrawCheckbox(ncRect, gx, ButtonState.Pushed | ButtonState.Flat);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the default checkboxes which are drawn when the treeview.Checkboxes property
        /// is set tot true.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="gx"></param>
        private void ClearCheckbox(Rectangle bounds, Graphics gx)
        {
            // make sure the default checkboxes are gone.
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                gx.FillRectangle(brush, bounds);
            }
        }

        /// <summary>
        /// Draws thee node lines before the checkboxes are drawn
        /// </summary>
        /// <param name="gx">Graphics context</param>
        private void DrawNodeLines(TriStateTreeNode node, Rectangle bounds, Graphics gx)
        {
            // determine type of line to draw
            NodeLineType lineType = node.NodeLineType;
            if (lineType == NodeLineType.None) { return; }

            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                pen.DashStyle = DashStyle.Dot;

                gx.DrawLine(pen, new Point(bounds.X, bounds.Y + 8), new Point(bounds.X + 15, bounds.Y + 8));
                if (lineType == NodeLineType.WithChildren && node.IsExpanded)
                {
                    gx.DrawLine(pen, new Point(bounds.X + 8, bounds.Y + 8), new Point(bounds.X + 8, bounds.Y + 16));
                }
            }
        }

        /// <summary>
        /// Draws a checkbox in the desired state and style
        /// </summary>
        /// <param name="bounds">boundaries of the checkbox</param>
        /// <param name="gx">graphics context object</param>
        /// <param name="buttonState">state to draw the checkbox in</param>
        private void DrawCheckbox(Rectangle bounds, Graphics gx, ButtonState buttonState)
        {
            // if we don't have custom images, or no imagelist, draw default images
            if (!this.useCustomImages || (this.useCustomImages && null == this.ImageList))
            {
                ControlPaint.DrawMixedCheckBox(gx, bounds, buttonState);
                return;
            }

            // get the right image index
            int imageIndex = -1;
            if ((buttonState & ButtonState.Normal) == ButtonState.Normal)
                imageIndex = this.indexUnchecked;
            if ((buttonState & ButtonState.Checked) == ButtonState.Checked)
                imageIndex = this.indexChecked;
            if ((buttonState & ButtonState.Pushed) == ButtonState.Pushed)
                imageIndex = this.indexIndeterminate;

            if (imageIndex > -1 && imageIndex < this.ImageList.Images.Count)
            {
                // index is valid so draw the image
                this.ImageList.Draw(gx, bounds.X, bounds.Y, bounds.Width + 1, bounds.Height + 1, imageIndex);
            }
            else
            {
                // index is not valid so draw default image
                ControlPaint.DrawMixedCheckBox(gx, bounds, buttonState);
            }
        }

        /// <summary>
        /// Ovveride the WindowProcedure in order to intercept the itemdraw event
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_NOTIFY = 0x4E;

            int iResult = 0;
            bool bHandled = false;

            if (m.Msg == (0x2000 | WM_NOTIFY))
            {
                if (m.WParam.Equals(this.Handle))
                {
                    iResult = HandleNotify(m);
                    m.Result = new IntPtr(iResult);
                    bHandled = (iResult != 0);
                }
            }

            if (!bHandled)
                base.WndProc(ref m);
        }

        /// <summary>
        /// override the aftercheck event in order to get the node beeing checked/unchecked
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);

            TreeNode node = e.Node;
            if (node != null)
            {
                TriStateTreeNode clickedNode = node as TriStateTreeNode;
                if (clickedNode != null)
                    if (clickedNode.CheckboxVisible)
                    {
                        ToggleNodeState(clickedNode);
                    }
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
        }


        /// <summary>
        /// Toggles node state between checked & unchecked
        /// </summary>
        /// <param name="node"></param>
        private void ToggleNodeState(TriStateTreeNode node)
        {
            // no need to toggle state for non-existing node ( or non-tristatetreenode! )
            if (null == node) return;

            // toggle state
            CheckState nextState;
            switch (node.CheckState)
            {
                case CheckState.Unchecked:
                    nextState = CheckState.Checked;
                    break;
                default:
                    nextState = CheckState.Unchecked;
                    break;
            }

            // notify the treeview that an update is about to take place
            BeginUpdate();

            // update the node state, and dependend nodes
            node.SetCheckedState(nextState);

            // force a redraw
            EndUpdate();
        }
    }
}

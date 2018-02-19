using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DragNDrop
{
    public partial class DragNDrop : Form
    {
        #region Member Variables
        string dragItemTempFileName = string.Empty;
        private bool itemDragStart = false;
        #endregion

        #region Contructor
        public DragNDrop()
        {
            InitializeComponent();
        }
        #endregion

        #region DragMethods
        private void ClearDragData()
        {
            try
            {
                if (File.Exists(dragItemTempFileName))
                    File.Delete(dragItemTempFileName);
                Program.objDragItem = null;
                dragItemTempFileName = string.Empty;
                itemDragStart = false;
                Program.ClearFileWatchers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ItemsListView Events
        private void ItemsListView_MouseDown(object sender, MouseEventArgs e)
        {
            //Cears the Drag Data
            ClearDragData();
            if (e.Button == MouseButtons.Left && ItemsListView.SelectedItems.Count > 0)
            {
                Program.objDragItem = ItemsListView.SelectedItems[0].Text;
                itemDragStart = true;
            }    
        }

        private void ItemsListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
                return;
            if (itemDragStart && Program.objDragItem != null)
            {
                dragItemTempFileName = string.Format("{0}{1}{2}.tmp", Path.GetTempPath(), Program.DRAG_SOURCE_PREFIX, ItemsListView.SelectedItems[0].Text);
                try
                {
                    Util.CreateDragItemTempFile(dragItemTempFileName);

                    string[] fileList = new string[] { dragItemTempFileName };
                    DataObject fileDragData = new DataObject(DataFormats.FileDrop, fileList);
                    DoDragDrop(fileDragData, DragDropEffects.Move);

                    ClearDragData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
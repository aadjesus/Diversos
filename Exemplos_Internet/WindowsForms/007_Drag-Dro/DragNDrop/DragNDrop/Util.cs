using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DragNDrop
{
    internal static class Util
    {
        internal static void CreateDragItemTempFile(string dragItemTempFileName)
        {
            FileStream fsDropFile = null;

            try
            {
                fsDropFile = new FileStream(dragItemTempFileName, FileMode.Create);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "DragNDrop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fsDropFile != null)
                {
                    fsDropFile.Flush();
                    fsDropFile.Close();
                    fsDropFile.Dispose();
                }
            }
        }
    }
}

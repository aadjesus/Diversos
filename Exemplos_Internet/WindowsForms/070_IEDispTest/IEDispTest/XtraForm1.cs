using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking;
using DevExpress.Utils;

namespace IEDispTest
{
    public partial class XtraForm1 : XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            dockManager.LayoutVersion = Guid.NewGuid().ToString();
            dockManager.LayoutUpgrade += OnDockManagerLayoutUpgrade;
        }

        void OnDockManagerLayoutUpgrade(object sender, LayoutUpgadeEventArgs e)
        {
            dockManager.BeginUpdate();
            List<DockPanel> toRemove = new List<DockPanel>();
            try
            {
                foreach (DockPanel panel in dockManager.Panels)
                {
                    if (LoadedPlugIns.ContainsKey(panel.ID))
                        LoadedPlugIns.Remove(panel.ID);
                    else if (panel.ID.ToString().StartsWith("00000000-1984-0000-0000-0000000000"))
                        toRemove.Add(panel);
                }
                foreach (DockPanel panel in toRemove)
                    dockManager.RemovePanel(panel);
                AddPlugins();
            }
            finally { dockManager.EndUpdate(); }
        }

        Dictionary<Guid, PlugIn> LoadedPlugIns = new Dictionary<Guid, PlugIn>();

        private void AddPlugins()
        {
            DockPanel dp_first = null;
            DockPanel dp_second = null;
            foreach (DockPanel panel in dockManager.Panels)
                if (panel.Dock == DockingStyle.Left)
                {
                    dp_first = panel;
                    break;
                }
            foreach (KeyValuePair<Guid, PlugIn> kvp in LoadedPlugIns)
            {
                if (dp_first == null)
                {
                    dp_first = dockManager.AddPanel(DockingStyle.Left);
                    dp_first.ID = kvp.Key;
                    dp_first.Text = kvp.Value.Name;
                    dp_first.ControlContainer.Controls.Add(kvp.Value.CreateControl());
                }
                else
                {
                    if (dp_second == null)
                    {
                        dp_second = dp_first.AddPanel();
                        dp_second.ID = kvp.Key;
                        dp_second.Text = kvp.Value.Name;
                        dp_second.ControlContainer.Controls.Add(kvp.Value.CreateControl());
                        dp_first.ParentPanel.Tabbed = true;
                    }
                    else
                    {
                        DockPanel dp_other = dp_first.ParentPanel.AddPanel();
                        dp_other.ID = kvp.Key;
                        dp_other.Text = kvp.Value.Name;
                        dp_other.ControlContainer.Controls.Add(kvp.Value.CreateControl());
                    }
                }
            }
        }
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            List<PlugIn> PlugIns = new List<PlugIn>();
            LoadedPlugIns.Add(new Guid("00000000-1984-0000-0000-000000000001"),
                new PlugIn("view as grid"));
            LoadedPlugIns.Add(new Guid("00000000-1984-0000-0000-000000000002"),
                new PlugIn("view as tree"));
            //PlugIns.Add(new PlugIn("view as graph"));
            //PlugIns.Add(new PlugIn("view as memo"));
            //PlugIns.Add(new PlugIn("this is a new plugin"));
            AddPlugins();
            try
            {
                dockManager.RestoreLayoutFromXml(@"c:\Users\alessandro.augusto\Downloads\IEDispTest\IEDispTest\bin\Debug\test_1.xml");
            }
            catch (Exception i_exc)
            {
                XtraMessageBox.Show(i_exc.Message);
            }
        }

        private void XtraForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockManager.SaveLayoutToXml("test_1.xml");
        }

        private void df_dockPanel2_Click(object sender, EventArgs e)
        {

        }
    }
    //
    public class PlugIn
    {
        public String Name;

        public PlugIn(String name)
        {
            Name = name;
        }

        public Control CreateControl()
        {
            Label lb = new Label();
            lb.Text = "Test " + Name;
            return lb;
        }
    }
}
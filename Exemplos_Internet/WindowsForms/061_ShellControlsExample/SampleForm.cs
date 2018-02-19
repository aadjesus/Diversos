using System;
using System.Windows.Forms;

namespace ShellControlsExample
{
  public partial class SampleForm : Form
  {
    public SampleForm()
    {
      InitializeComponent();
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void SampleForm_Load(object sender, EventArgs e)
    {
      this.FillTreeView(defaultTreeView);
      this.FillTreeView(shellTreeView);
      this.FillListView(defaultListView);
      this.FillListView(shellListView);

      shellTreeView.ShowLines = false;
    }

    private void FillListView(System.Windows.Forms.ListView listView)
    {
      int columnCount;
      int rowCount;

      columnCount = 10;
      rowCount = 50;

      for (int i = 0; i < columnCount; i++)
        listView.Columns.Add(string.Format("Column {0}", (char)(65 + i)));

      for (int row = 0; row < rowCount; row++)
      {
        ListViewItem rowItem;

        rowItem = new ListViewItem();
        rowItem.Text = string.Format("Cell {0}A", row + 1);

        for (int column = 1; column < columnCount; column++)
          rowItem.SubItems.Add(string.Format("Cell {0}{1}", row + 1, (char)(65 + column)));

        listView.Items.Add(rowItem);
      }

      listView.View = View.Details;
      listView.FullRowSelect = true;
      listView.HideSelection = false;
      listView.Items[0].Selected = true;
    }

    private void FillTreeView(System.Windows.Forms.TreeView treeView)
    {
      int childCount;

      childCount = 10;

      for (int i = 0; i < childCount; i++)
      {
        TreeNode treeNode;

        treeNode = new TreeNode(string.Format("Node {0}", (char)(65 + i)));

        for (int z = 0; z < childCount; z++)
          treeNode.Nodes.Add(string.Format("Child {0}", i + 1));

        treeView.Nodes.Add(treeNode);
      }

      treeView.HideSelection = false;
      treeView.ExpandAll();
      treeView.Nodes[0].EnsureVisible();
      treeView.SelectedNode = treeView.Nodes[0];
    }
  }
}

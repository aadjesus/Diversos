using System.Windows.Forms;

namespace ColorDialogExample
{
    public partial class Main : Form
    {
        private SelectableClass _Object = new SelectableClass();

        public Main()
        {
            InitializeComponent();

            pgMain.SelectedObject = _Object;
        }
    }
}

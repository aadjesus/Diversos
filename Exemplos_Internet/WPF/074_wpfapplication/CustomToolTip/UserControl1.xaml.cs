using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomToolTip
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public double UserControlToolTipX
        {
            get { return this.UserControlToolTipXY.X; }
            set { this.UserControlToolTipXY.X = value; }
        }

        public double UserControlToolTipY
        {
            get { return this.UserControlToolTipXY.Y; }
            set { this.UserControlToolTipXY.Y = value; }
        }

        public string UserControlTextBlockToolTip
        {
            get { return TextBlockToolTip.Text; }
            set { TextBlockToolTip.Text = value; }
        }

        public string UserControlToolTipTitle
        {
            get { return ToolTipTitle.Text; }
            set { ToolTipTitle.Text = value; }
        }
    }
}

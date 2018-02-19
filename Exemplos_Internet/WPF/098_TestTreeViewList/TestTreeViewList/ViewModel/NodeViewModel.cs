using System.Collections.Generic;
using System.ComponentModel;

namespace TestTreeViewList
{
    public class NodeViewModel : INotifyPropertyChanged
    {
        private bool isSelected;

        public string Name { get; set; }
        public List<NodeViewModel> Children { get; set; }

        public NodeViewModel()
        {
            this.Children = new List<NodeViewModel>();
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                if(this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfTreeViewTricks
{
    public class SomeDataObject
    {
        private int _id;
        private ObservableCollection<SomeDataObject> _children;

        public SomeDataObject(int id)
        {
            this._id = id;
            this._children = new ObservableCollection<SomeDataObject>();
        }

        public int Id
        {
            get
            {
                return this._id;
            }
        }

        public ObservableCollection<SomeDataObject> Children
        {
            get
            {
                return this._children;
            }
        }
    }
}

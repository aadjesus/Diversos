using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ScrollControl
{
    /// <summary>
    /// Represents a ObservableCollection of ItemHolder. There
    /// is a singleton Instance that should be used
    /// </summary>
    public class ItemList : ObservableCollection<ItemHolder>
    {
        #region Data
        private static ItemList instance;
        #endregion

        #region Ctor
        private ItemList()
        {

        }
        #endregion

        #region Public Methods / Properties
        /// <summary>
        /// Returns the singleton instance 
        /// </summary>
        public static ItemList Instance
        {
            get
            {

                if (instance == null)
                {
                    instance = new ItemList();
                }
                return instance;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Controls;


namespace NotesListBox
{
    /// <summary>
    /// Provides a OneWay converter that can provide a Top/ZIndex or Rotate
    /// value for a give ListBoxItem, based on the source ListBoxItem index
    /// </summary>
    public class ListIndexConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            ListBoxItem item = (ListBoxItem)value;
            ListBox listBox =
                ItemsControl.ItemsControlFromItemContainer(item) as ListBox;
            String paramValue = parameter.ToString();
            Int32 index = listBox.ItemContainerGenerator.IndexFromContainer(item);

            switch (paramValue)
            {
                case "Top":
                    return index * 80;
                case "ZIndex":
                    return listBox.Items.Count - index;
                case "Rotate":
                    return RotateAngle(index);
            }
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("can not convert back");
        }
        #endregion

        #region Private Methods
        private int RotateAngle(int index)
        {
            if (index == 0)
                return -5;
            if (index % 3 == 0)
                return -15;
            if (index % 2 == 0)
                return 10;
            if (index % 1 == 0)
                return 6;
            else
                return 3;

        }


        #endregion
    }
}

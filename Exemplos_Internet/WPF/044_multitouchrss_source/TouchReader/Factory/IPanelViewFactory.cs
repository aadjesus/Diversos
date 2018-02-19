using System.Windows;
using System.Windows.Controls;

namespace VirtualDreams.TouchReader.Factory
{
    public interface IPanelViewFactory
    {
        Panel Panel { get; set; }

        void CreateViewFromDataContext(object dataContext, Point position);
    }
}

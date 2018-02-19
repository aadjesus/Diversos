using System.Windows;
using System.Windows.Controls;
using Multitouch.Framework.WPF.Input;
using VirtualDreams.TouchReader.View;
using VirtualDreams.TouchReader.ViewModel;

namespace VirtualDreams.TouchReader.Factory
{
    class TouchablePanelRssViewFactory : IPanelViewFactory
    {
        public Panel Panel { get; set; }

        public void CreateViewFromDataContext(object dataContext, Point position)
        {
            var viewModel = dataContext as RssViewModelBase;

            if (viewModel == null) return;

            FrameworkElement newView;

            if (viewModel is FeedItemViewModel)
                newView = new FeedItemView();
            else
                newView = new FolderAndFeedView();
            
            newView.DataContext = viewModel;
            newView.SetValue(Canvas.LeftProperty, position.X);
            newView.SetValue(Canvas.TopProperty, position.Y);
            MultitouchScreen.AddContactEnterHandler(newView, ElementCaptureHandler);
            Panel.Children.Add(newView);
        }

        private static void ElementCaptureHandler(object sender, ContactEventArgs e)
        {
            var element = sender as IInputElement;
            if (element != null)
                e.Contact.Capture(element);
        }

    }
}

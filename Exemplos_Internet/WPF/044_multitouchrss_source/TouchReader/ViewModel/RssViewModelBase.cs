using MvvmFoundation.Wpf;

namespace VirtualDreams.TouchReader.ViewModel
{
    internal abstract class RssViewModelBase : ObservableObject
    {
        public string Title
        {
            get { return GetTitle(); }
        }

        protected abstract string GetTitle();
    }
}

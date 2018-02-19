using System;
using System.Windows.Input;
using Galador.Applications;

namespace DemoApp
{
	/// <summary>
	/// This ViewModelBase subclass requests to be removed 
	/// from the UI when its CloseCommand executes.
	/// This class is abstract.
	/// </summary>
	public abstract class WorkspaceViewModel : ViewModelEx
	{
		protected WorkspaceViewModel()
		{
		}

		#region CloseCommand

		public ICommand CloseCommand
		{
			get { return mCloseCommand; }
			set
			{
				if (value == mCloseCommand)
					return;
				mCloseCommand = value;
				OnPropertyChanged(() => CloseCommand);
			}
		}
		ICommand mCloseCommand;

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
using Galador.Applications;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace DemoApp.ViewModel
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class MainWindowViewModel : WorkspaceViewModel, IDesignTimeAware
	{
		readonly ICustomerRepository customerRepository;

		[ImportingConstructor]
		public MainWindowViewModel(ICustomerRepository repository)
		{
			DisplayName = Strings.MainWindowViewModel_DisplayName;
			Workspaces = new ObservableCollection<WorkspaceViewModel>();

			this.customerRepository = repository;
			this.EditCommand = new DelegateCommand<CustomerViewModel>(cvm => ShowCustomer(cvm));
		}

		[Export("EditCustomerCommand", typeof(ICommand))]
		public ICommand EditCommand { get; set; }

		public ObservableCollection<WorkspaceViewModel> Workspaces { get; private set; }

		#region Commands

		/// <summary>
		/// Returns a read-only list of commands 
		/// that the UI can display and execute.
		/// </summary>
		public ReadOnlyCollection<CommandViewModel> Commands
		{
			get
			{
				if (_commands == null)
				{
					List<CommandViewModel> cmds = this.CreateCommands();
					_commands = new ReadOnlyCollection<CommandViewModel>(cmds);
				}
				return _commands;
			}
		}
		ReadOnlyCollection<CommandViewModel> _commands;

		List<CommandViewModel> CreateCommands()
		{
			return new List<CommandViewModel>
			{
				new CommandViewModel(
					Strings.MainWindowViewModel_Command_ViewAllCustomers,
					new DelegateCommand(ShowAllCustomers)),

				new CommandViewModel(
					Strings.MainWindowViewModel_Command_CreateNewCustomer,
					new DelegateCommand(CreateNewCustomer))
			};
		}

		#endregion // Workspaces

		#region private: ShowCustomer(), CreateNewCustomer(), ShowAllCustomers(), SetActiveWorkspace(workspace), CreateCloseCommand()

		[Export("ShowCustomer", typeof(Action<CustomerViewModel>))]
		void ShowCustomer(CustomerViewModel cvm)
		{
			var workspace = (
				from w in Workspaces
				let cm = w as CustomerViewModel
				where cm != null && cm.Customer == cvm.Customer
				select cm
			)
			.FirstOrDefault();
			if (workspace == null)
			{
				workspace = cvm;
				this.Workspaces.Add(cvm);
			}
			workspace.CloseCommand = CreateCloseCommand(workspace);
			this.SetActiveWorkspace(workspace);
		}

		void CreateNewCustomer()
		{
			var newCustomer = new Customer();
			var workspace = new CustomerViewModel(newCustomer, customerRepository);
			workspace.CloseCommand = CreateCloseCommand(workspace);
			this.Workspaces.Add(workspace);
			this.SetActiveWorkspace(workspace);
		}

		ICommand CreateCloseCommand(WorkspaceViewModel model)
		{
			return new DelegateCommand<WorkspaceViewModel>(wvm => Workspaces.Remove(wvm));
		}

		void ShowAllCustomers()
		{
			var workspace = Workspaces.FirstOrDefault(vm => vm is AllCustomersViewModel) as AllCustomersViewModel;
			if (workspace == null)
			{
				workspace = new AllCustomersViewModel(customerRepository);
				workspace.CloseCommand = CreateCloseCommand(workspace);
				this.Workspaces.Add(workspace);
			}
			this.SetActiveWorkspace(workspace);
		}

		void SetActiveWorkspace(WorkspaceViewModel workspace)
		{
			ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
			if (collectionView != null)
				collectionView.MoveCurrentTo(workspace);
		}

		#endregion

		void IDesignTimeAware.DesignTimeInitialization()
		{
			CreateNewCustomer();
			ShowAllCustomers();
		}
	}
}
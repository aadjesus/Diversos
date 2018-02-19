//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using StockTraderRI.Modules.Position.Interfaces;
using System.Collections.ObjectModel;
using Galador.Applications;
using System.Windows.Input;
using System.ComponentModel.Composition;
using System.Linq;

namespace StockTraderRI.Modules.Position.Orders
{
	[Export(typeof(IOrdersPresentationModel))]
	public partial class OrdersPresentationModel : ViewModelBase, IOrdersPresentationModel
	{
		public OrdersPresentationModel()
		{
			OrderItems = new ObservableCollection<IOrderCompositePresentationModel>();
			SubmitAllCommand = new ForeachCommand<IOrderCompositePresentationModel>(m => m.SubmitCommand, OrderItems);
			CancelAllCommand = new ForeachCommand<IOrderCompositePresentationModel>(m => m.CancelCommand, OrderItems);
		}

		public string HeaderInfo { get { return "BUY & SELL"; } }
		public ICommand SubmitAllCommand { get; private set; }
		public ICommand CancelAllCommand { get; private set; }
		public ObservableCollection<IOrderCompositePresentationModel> OrderItems { get; private set; }
	}
}

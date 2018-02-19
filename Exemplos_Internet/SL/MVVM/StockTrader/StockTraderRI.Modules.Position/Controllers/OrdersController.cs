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
using System;
using System.Collections.Generic;
using System.Globalization;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Position.Interfaces;
using StockTraderRI.Modules.Position.Models;
using StockTraderRI.Modules.Position.Orders;
using StockTraderRI.Modules.Position.Properties;
using System.Linq;
using Galador.Applications;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Position.Controllers
{
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IOrdersController))]
	public class OrdersController : IOrdersController
	{
		IShellView shell;
		IOrdersPresentationModel orders;
		IAccountPositionService accountPositionService;

		[ImportingConstructor]
		public OrdersController(IShellView shell, IOrdersPresentationModel orders, IAccountPositionService accountPositionService)
		{
			this.shell = shell;
			this.orders = orders;
			this.accountPositionService = accountPositionService;

			BuyCommand = new DelegateCommand<string>(s => StartOrder(s, TransactionType.Buy));
			SellCommand = new DelegateCommand<string>(s => StartOrder(s, TransactionType.Sell));
		}

		protected void StartOrder(string tickerSymbol, TransactionType transactionType)
		{
			if (String.IsNullOrEmpty(tickerSymbol))
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.StringCannotBeNullOrEmpty, "tickerSymbol"));

			this.ShowOrdersView();

			var orderCompositePresentationModel = Composition.GetInstance<IOrderCompositePresentationModel>();
			orderCompositePresentationModel.TransactionInfo = new TransactionInfo(tickerSymbol, transactionType);
			orderCompositePresentationModel.CloseViewRequested += delegate
			{
				orders.OrderItems.Remove(orderCompositePresentationModel);
				if (orders.OrderItems.Count == 0)
					this.RemoveOrdersView();
			};
			orders.OrderItems.Add(orderCompositePresentationModel);
		}

		private void RemoveOrdersView()
		{
			shell.Show(ShellRegion.Action, null);
		}

		private void ShowOrdersView()
		{
			shell.Show(ShellRegion.Action, orders);
		}

		public ICommand<string> BuyCommand { get; private set; }
		public ICommand<string> SellCommand { get; private set; }
		public DelegateCommand<OrdersController> SubmitAllVoteOnlyCommand { get; private set; }
	}
}

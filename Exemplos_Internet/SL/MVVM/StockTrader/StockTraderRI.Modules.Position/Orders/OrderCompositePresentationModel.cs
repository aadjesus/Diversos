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
using System.Windows;
using System.Windows.Input;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Position.Interfaces;
using StockTraderRI.Modules.Position.Models;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Position.Orders
{
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IOrderCompositePresentationModel))]
	public partial class OrderCompositePresentationModel : DependencyObject, IOrderCompositePresentationModel, IHeaderInfoProvider<string>, IPartImportsSatisfiedNotification
	{
#pragma warning disable 649
		[Import]
		private IOrderDetailsPresentationModel orderDetailsPresentationModel;
#pragma warning restore 649

		public static readonly DependencyProperty HeaderInfoProperty = DependencyProperty.Register("HeaderInfo", typeof(string), typeof(OrderCompositePresentationModel), null);

		public OrderCompositePresentationModel()
		{
		}
		public void OnImportsSatisfied()
		{
			this.orderDetailsPresentationModel.CloseViewRequested += _orderPresenter_CloseViewRequested;
		}

		void _orderPresenter_CloseViewRequested(object sender, EventArgs e)
		{
			OnCloseViewRequested(sender, e);
		}

		partial void SetTransactionInfo(TransactionInfo transactionInfo);

		private void OnCloseViewRequested(object sender, EventArgs e)
		{
			CloseViewRequested(sender, e);
		}

		public event EventHandler CloseViewRequested = delegate { };

		public TransactionInfo TransactionInfo
		{
			get { return this.orderDetailsPresentationModel.TransactionInfo; }
			set { SetTransactionInfo(value); }

		}

		public ICommand SubmitCommand
		{
			get { return this.orderDetailsPresentationModel.SubmitCommand; }
		}

		public ICommand CancelCommand
		{
			get { return this.orderDetailsPresentationModel.CancelCommand; }
		}

		public int Shares
		{
			get { return this.orderDetailsPresentationModel.Shares ?? 0; }
		}

		public IOrderDetailsPresentationModel OrderDetail { get { return orderDetailsPresentationModel; } }
	}
}
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
using System.ComponentModel;
using System.Globalization;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Position.Interfaces;
using StockTraderRI.Modules.Position.Models;
using StockTraderRI.Modules.Position.Properties;
using Galador.Applications;
using System.Windows.Input;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Position.Orders
{
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IOrderDetailsPresentationModel))]
	public class OrderDetailsPresentationModel : ViewModelBase, IOrderDetailsPresentationModel, IPartImportsSatisfiedNotification
	{
#pragma warning disable 649
		[Import]
		private IAccountPositionService accountPositionService;
		[Import]
		private IOrdersService ordersService;
#pragma warning restore 649

		private readonly List<string> errors = new List<string>();

		public OrderDetailsPresentationModel()
		{
			//use localizable enum descriptions
			this.AvailableOrderTypes = new List<ValueDescription<OrderType>>
										{
											new ValueDescription<OrderType>(OrderType.Limit, Resources.OrderType_Limit),
											new ValueDescription<OrderType>(OrderType.Market, Resources.OrderType_Market),
											new ValueDescription<OrderType>(OrderType.Stop, Resources.OrderType_Stop)
										};

			this.AvailableTimesInForce = new List<ValueDescription<TimeInForce>>
										  {
											  new ValueDescription<TimeInForce>(TimeInForce.EndOfDay, Resources.TimeInForce_EndOfDay),
											  new ValueDescription<TimeInForce>(TimeInForce.ThirtyDays, Resources.TimeInForce_ThirtyDays)
										  };

			this.SubmitCommand = new DelegateCommand(Submit, () => CanSubmit);
			this.CancelCommand = new DelegateCommand(Cancel);

		}

		void IPartImportsSatisfiedNotification.OnImportsSatisfied()
		{
			this.SetInitialValidState();
		}

		public event EventHandler CloseViewRequested = delegate { };

		public IList<ValueDescription<OrderType>> AvailableOrderTypes { get; private set; }

		public IList<ValueDescription<TimeInForce>> AvailableTimesInForce { get; private set; }

		#region TransactionInfo: TransactionType, TickerSymbol

		public TransactionInfo TransactionInfo
		{
			get { return mTransactionInfo; }
			set
			{
				if (value == mTransactionInfo)
					return;
				mTransactionInfo = value;
				OnPropertyChanged(() => TransactionInfo);
				OnPropertyChanged(() => TickerSymbol);
				OnPropertyChanged(() => TransactionType);
			}
		}
		TransactionInfo mTransactionInfo = new TransactionInfo();

		public TransactionType TransactionType
		{
			get { return this.TransactionInfo.TransactionType; }
			set
			{
				if (this.TransactionInfo.TransactionType == value)
					return;
				this.ValidateHasEnoughSharesToSell(this.Shares, value, false);
				this.TransactionInfo.TransactionType = value;
				OnPropertyChanged(() => TransactionType);
			}
		}

		public string TickerSymbol
		{
			get { return this.TransactionInfo.TickerSymbol; }
			set
			{
				if (this.TransactionInfo.TickerSymbol == value)
					return;
				this.TransactionInfo.TickerSymbol = value;
				OnPropertyChanged(() => TickerSymbol);
			}
		}

		#endregion

		#region Shares, ValidateHasEnoughSharesToSell(), ValidateShares(), HoldsEnoughShares()

		public int? Shares
		{
			get { return mShares; }
			set
			{
				if (value == mShares)
					return;
				
				this.ValidateShares(value, true);
				this.ValidateHasEnoughSharesToSell(value, this.TransactionType, true);

				mShares = value;
				OnPropertyChanged(() => Shares);
			}
		}
		int? mShares;

		private void ValidateHasEnoughSharesToSell(int? sharesToSell, TransactionType transactionType, bool throwException)
		{
			if (transactionType == TransactionType.Sell && !this.HoldsEnoughShares(this.TickerSymbol, sharesToSell))
			{
				this.AddError("NotEnoughSharesToSell");
				if (throwException)
				{
					throw new InputValidationException(String.Format(CultureInfo.InvariantCulture, Resources.NotEnoughSharesToSell, sharesToSell));
				}
			}
			else
			{
				this.RemoveError("NotEnoughSharesToSell");
			}
		}

		private void ValidateShares(int? newSharesValue, bool throwException)
		{
			if (!newSharesValue.HasValue || newSharesValue.Value <= 0)
			{
				this.AddError("InvalidSharesRange");
				if (throwException)
				{
					throw new InputValidationException(Resources.InvalidSharesRange);
				}
			}
			else
			{
				this.RemoveError("InvalidSharesRange");
			}
		}

		private bool HoldsEnoughShares(string symbol, int? sharesToSell)
		{
			if (!sharesToSell.HasValue)
			{
				return false;
			}

			foreach (AccountPosition accountPosition in this.accountPositionService.GetAccountPositions())
			{
				if (accountPosition.TickerSymbol.Equals(symbol, StringComparison.OrdinalIgnoreCase))
				{
					if (accountPosition.Shares >= sharesToSell)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return false;
		}

		#endregion

		#region OrderType

		public OrderType OrderType
		{
			get { return mOrderType; }
			set
			{
				if (value == mOrderType)
					return;
				mOrderType = value;
				OnPropertyChanged(() => OrderType);
			}
		}
		OrderType mOrderType = OrderType.Market;

		#endregion

		#region StopLimitPrice, ValidateStopLimitPrice()

		public decimal? StopLimitPrice
		{
			get { return mStopLimitPrice; }
			set
			{
				if (value == mStopLimitPrice)
					return;

				this.ValidateStopLimitPrice(value, true);

				mStopLimitPrice = value;
				OnPropertyChanged(() => StopLimitPrice);
			}
		}
		decimal? mStopLimitPrice;

		private void ValidateStopLimitPrice(decimal? price, bool throwException)
		{
			if (!price.HasValue || price.Value <= 0)
			{
				this.AddError("InvalidStopLimitPrice");
				if (throwException)
				{
					throw new InputValidationException(Resources.InvalidStopLimitPrice);
				}
			}
			else
			{
				this.RemoveError("InvalidStopLimitPrice");
			}
		}

		#endregion

		#region TimeInForce

		public TimeInForce TimeInForce
		{
			get { return mTimeInForce; }
			set
			{
				if (value == mTimeInForce)
					return;
				mTimeInForce = value;
				OnPropertyChanged(() => TimeInForce);
			}
		}
		TimeInForce mTimeInForce;

		#endregion

		public ICommand SubmitCommand { get; private set; }
		public ICommand CancelCommand { get; private set; }

		private void SetInitialValidState()
		{
			this.ValidateShares(this.Shares, false);
			this.ValidateStopLimitPrice(this.StopLimitPrice, false);
		}

		private void AddError(string ruleName)
		{
			if (!this.errors.Contains(ruleName))
			{
				this.errors.Add(ruleName);
				OnPropertyChanged(() => CanSubmit);
			}
		}

		private void RemoveError(string ruleName)
		{
			if (this.errors.Contains(ruleName))
			{
				this.errors.Remove(ruleName);
				if (this.errors.Count == 0)
					OnPropertyChanged(() => CanSubmit);
			}
		}

		private bool CanSubmit
		{
			get { return this.errors.Count == 0; }
		}

		private void Submit()
		{
			if (!this.CanSubmit)
				throw new InvalidOperationException();

			var order = new Order();
			order.TransactionType = this.TransactionType;
			order.OrderType = this.OrderType;
			order.Shares = this.Shares.Value;
			order.StopLimitPrice = this.StopLimitPrice.Value;
			order.TickerSymbol = this.TickerSymbol;
			order.TimeInForce = this.TimeInForce;

			ordersService.Submit(order);

			CloseViewRequested(this, EventArgs.Empty);
		}

		private void Cancel()
		{
			CloseViewRequested(this, EventArgs.Empty);
		}
	}
}

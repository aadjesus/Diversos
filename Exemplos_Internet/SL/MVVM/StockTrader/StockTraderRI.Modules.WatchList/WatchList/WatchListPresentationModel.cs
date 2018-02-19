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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Watch.Properties;
using StockTraderRI.Modules.Watch.Services;
using Galador.Applications;
using System.ComponentModel.Composition;
using System.Collections.Specialized;

namespace StockTraderRI.Modules.Watch.WatchList
{
	[Export(typeof(IWatchListPresentationModel))]
	public class WatchListPresentationModel : IWatchListPresentationModel, INotifyPropertyChanged, IHeaderInfoProvider<string>
	{
		private readonly IMarketFeedService marketFeedService;
		private readonly ObservableCollection<string> watchList;
		private ICommand removeWatchCommand;
		private ObservableCollection<WatchItem> watchListItems;
		private WatchItem currentWatchItem;
		readonly IShellView shell;

		[ImportingConstructor]
		public WatchListPresentationModel(IShellView shell, IWatchListService watchListService, IMarketFeedService marketFeedService)
		{
			this.shell = shell;

			this.HeaderInfo = Resources.WatchListTitle;
			this.WatchListItems = new ObservableCollection<WatchItem>();
			this.WatchListItems.CollectionChanged += (o, e) =>
			{
				if (e.Action == NotifyCollectionChangedAction.Add)
					shell.Show(ShellRegion.Main, this);
			};

			this.marketFeedService = marketFeedService;

			this.watchList = watchListService.RetrieveWatchList();
			this.watchList.CollectionChanged += delegate { this.PopulateWatchItemsList(this.watchList); };
			this.PopulateWatchItemsList(this.watchList);

			Notifications.Subscribe<WatchListPresentationModel, MarketPricesUpdatedEvent>(
				this,
				(t, e) => t.MarketPricesUpdated(e),
				ThreadOption.UIThread);
			this.RemoveWatchCommand = new DelegateCommand<string>(s => RemoveWatch(s));
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public ObservableCollection<WatchItem> WatchListItems
		{
			get
			{
				return this.watchListItems;
			}

			set
			{
				if (this.watchListItems != value)
				{
					this.watchListItems = value;
					this.OnPropertyChanged("WatchListItems");
				}
			}
		}

		public WatchItem CurrentWatchItem
		{
			get
			{
				return this.currentWatchItem;
			}

			set
			{
				if (value != null && this.currentWatchItem != value)
				{
					this.currentWatchItem = value;
					this.OnPropertyChanged("CurrentWatchItem");
					Notifications.Publish<TickerSymbolSelectedEvent>(new TickerSymbolSelectedEvent(this.currentWatchItem.TickerSymbol));
				}
			}
		}

		public string HeaderInfo { get; set; }

		public ICommand RemoveWatchCommand
		{
			get
			{
				return this.removeWatchCommand;
			}

			private set
			{
				this.removeWatchCommand = value;
				this.OnPropertyChanged("RemoveWatchCommand");
			}
		}

		public void MarketPricesUpdated(MarketPricesUpdatedEvent updatedPrices)
		{
			foreach (WatchItem watchItem in this.WatchListItems)
			{
				if (updatedPrices.Prices.ContainsKey(watchItem.TickerSymbol))
				{
					watchItem.CurrentPrice = updatedPrices.Prices[watchItem.TickerSymbol];
				}
			}
		}

		private void RemoveWatch(string tickerSymbol)
		{
			this.watchList.Remove(tickerSymbol);
		}

		private void PopulateWatchItemsList(IEnumerable<string> watchItemsList)
		{
			this.WatchListItems.Clear();
			foreach (string tickerSymbol in watchItemsList)
			{
				decimal? currentPrice;
				try
				{
					currentPrice = this.marketFeedService.GetPrice(tickerSymbol);
				}
				catch (ArgumentException)
				{
					currentPrice = null;
				}

				this.WatchListItems.Add(new WatchItem(tickerSymbol, currentPrice));
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

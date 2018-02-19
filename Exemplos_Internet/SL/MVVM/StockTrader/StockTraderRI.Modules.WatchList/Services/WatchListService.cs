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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using StockTraderRI.Infrastructure.Interfaces;
using Galador.Applications;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Watch.Services
{
	[Export(typeof(IWatchListService))]
	public class WatchListService : IWatchListService
	{
		private readonly IMarketFeedService marketFeedService;

		[ImportingConstructor]
		public WatchListService(IMarketFeedService marketFeedService)
		{
			this.marketFeedService = marketFeedService;
			WatchItems = new ObservableCollection<string>();
			AddWatchCommand = new DelegateCommand<string>(s => AddWatch(s));
		}

		public ObservableCollection<string> RetrieveWatchList() { return WatchItems; }

		public ObservableCollection<string> WatchItems { get; private set; }
		public ICommand AddWatchCommand { get; private set; }

		private void AddWatch(string tickerSymbol)
		{
			if (string.IsNullOrEmpty(tickerSymbol))
				return;

			string upperCasedTrimmedSymbol = tickerSymbol.ToUpper(CultureInfo.InvariantCulture).Trim();
			if (!WatchItems.Contains(upperCasedTrimmedSymbol)
				&& marketFeedService.SymbolExists(upperCasedTrimmedSymbol))
				WatchItems.Add(upperCasedTrimmedSymbol);
		}
	}
}
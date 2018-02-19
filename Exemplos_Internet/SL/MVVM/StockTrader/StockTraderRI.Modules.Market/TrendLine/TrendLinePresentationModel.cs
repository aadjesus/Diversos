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
using System.ComponentModel;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using Galador.Applications;
using System.ComponentModel.Composition;

namespace StockTraderRI.Modules.Market.TrendLine
{
	[Export(typeof(ITrendLinePresentationModel))]
    public class TrendLinePresentationModel : ViewModelBase, ITrendLinePresentationModel
    {
        private readonly IMarketHistoryService _marketHistoryService;

		[ImportingConstructor]
        public TrendLinePresentationModel(IMarketHistoryService marketHistoryService)
        {
            this._marketHistoryService = marketHistoryService;
            Notifications.Subscribe<TrendLinePresentationModel, TickerSymbolSelectedEvent>(
				this,
				(m, e) => m.TickerSymbolChanged(e),
				ThreadOption.UIThread);
        }

        public void TickerSymbolChanged(TickerSymbolSelectedEvent newSymbol)
        {
            MarketHistoryCollection newHistoryCollection = this._marketHistoryService.GetPriceHistory(newSymbol.Symbol);
			this.TickerSymbol = newSymbol.Symbol;
            this.HistoryCollection = newHistoryCollection;
        }

		#region TickerSymbol, HistoryCollection

		public string TickerSymbol
        {
            get { return this.tickerSymbol; }
            set
            {
				if (this.tickerSymbol == value)
					return;
                this.tickerSymbol = value;
				OnPropertyChanged(() => TickerSymbol);
			}
        }
        private string tickerSymbol;

        public MarketHistoryCollection HistoryCollection
        {
            get { return historyCollection; }
            private set
            {
				if (this.historyCollection == value)
					return;
                this.historyCollection = value;
				OnPropertyChanged(() => HistoryCollection);
            }
        }
        private MarketHistoryCollection historyCollection;

        #endregion
    }
}

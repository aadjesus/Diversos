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
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Market.Services;
using StockTraderRI.Modules.Market.TrendLine;
using System.ComponentModel.Composition;
using Galador.Applications;

namespace StockTraderRI.Modules.Market
{
	[Export(typeof(IShellModule))]
    public class MarketModule : IShellModule
    {
#pragma warning disable 649
		[Import(typeof(IShellView))]
		IShellView shell;
#pragma warning restore 649

		public MarketModule()
        {
        }

        public void Init()
        {
			shell.Show(ShellRegion.Research, Composition.GetInstance<ITrendLinePresentationModel>());
        }
    }
}

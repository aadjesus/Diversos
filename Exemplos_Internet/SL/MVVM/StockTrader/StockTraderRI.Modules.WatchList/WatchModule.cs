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
using StockTraderRI.Modules.Watch.AddWatch;
using StockTraderRI.Modules.Watch.Services;
using StockTraderRI.Modules.Watch.WatchList;
using System.ComponentModel.Composition;
using Galador.Applications;

namespace StockTraderRI.Modules.Watch
{
	[Export(typeof(IShellModule))]
	public class WatchModule : IShellModule
	{
		public void Init()
		{
			var shell = Composition.GetInstance<IShellView>();
			shell.MainItems.Add(Composition.GetInstance<IWatchListPresentationModel>());
			shell.Show(ShellRegion.MainToolBar, new AddWatchPresentationModel(Composition.GetInstance<IWatchListService>()));
		}

		void WatchListItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			throw new System.NotImplementedException();
		}
	}
}

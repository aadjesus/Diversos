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
using System.Windows;
using StockTraderRI.Modules.Market;
using StockTraderRI.Modules.News;
using StockTraderRI.Modules.Position;
using StockTraderRI.Modules.Watch;
using Galador.Applications;
using StockTraderRI.Infrastructure;
using System.ComponentModel.Composition;

namespace StockTraderRI
{
	public partial class StockTraderRIBootstrapper
	{
		public StockTraderRIBootstrapper()
		{
			Composition.Register(
				typeof(Shell).Assembly
				, typeof(IShellView).Assembly
				, typeof(MarketModule).Assembly
				, typeof(PositionModule).Assembly
				, typeof(WatchModule).Assembly
				, typeof(NewsModule).Assembly
				);
			Logger = new TraceLogger();
			Shell = new Shell();
		}

		[Export(typeof(IShellView))]
		public IShellView Shell { get; internal set; }

		[Export(typeof(ILoggerFacade))]
		public ILoggerFacade Logger { get; private set; }

		[ImportMany(typeof(IShellModule))]
		public IShellModule[] Modules { get; internal set; }

		public void Run()
		{
			Composition.Compose(this);
			Shell.ShowShell();

			// also init the Module after the window is shown, to avoid all sort of strange Xaml element finding error
			foreach (var m in Modules)
				m.Init();
		}
	}
}

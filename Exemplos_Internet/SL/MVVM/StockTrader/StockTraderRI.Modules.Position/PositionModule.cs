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
using StockTraderRI.Modules.Position.Interfaces;
using StockTraderRI.Modules.Position.PositionSummary;
using StockTraderRI.Modules.Position.Services;
using StockTraderRI.Modules.Position.Controllers;
using StockTraderRI.Modules.Position.Orders;
using System.ComponentModel.Composition;
using Galador.Applications;

namespace StockTraderRI.Modules.Position
{
	[Export(typeof(IShellModule))]
	public class PositionModule : IShellModule
	{
#pragma warning disable 649
		[Import(typeof(IShellView))]
		IShellView shell;
#pragma warning restore 649

		public PositionModule()
		{
		}

		public void Init()
		{
			shell.Show(ShellRegion.Research, Composition.GetInstance<IPositionPieChartPresentationModel>());
			shell.MainItems.Add(Composition.GetInstance<IPositionSummaryPresentationModel>());
		}
	}
}

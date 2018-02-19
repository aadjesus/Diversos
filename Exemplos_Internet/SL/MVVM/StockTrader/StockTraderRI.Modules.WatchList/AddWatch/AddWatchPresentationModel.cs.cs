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
using StockTraderRI.Modules.Watch.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace StockTraderRI.Modules.Watch.AddWatch
{
	public class AddWatchPresentationModel : INotifyPropertyChanged
	{
		public AddWatchPresentationModel(IWatchListService service)
		{
			AddWatchCommand = service.AddWatchCommand;
		}

		public ICommand AddWatchCommand { get; set; }

		public string StockSymbol
		{
			get { return stockSymbol; }
			set
			{
				stockSymbol = value;
				OnPropertyChanged("StockSymbol");
			}
		}
		private string stockSymbol;

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler Handler = PropertyChanged;
			if (Handler != null) Handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

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
using StockTraderRI.Infrastructure;
using Galador.Applications;
using System.ComponentModel.Composition;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

namespace StockTraderRI
{
	/// <summary>
	/// Interaction logic for Shell.xaml
	/// </summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IShellView))]
	public partial class Shell : Window, IShellView
	{
		public Shell()
		{
			MainItems = new ObservableCollection<object>();
			InitializeComponent();
			Loaded += new RoutedEventHandler(Shell_Loaded);
		}

		List<Action> afterLoaded = new List<Action>();
		void Shell_Loaded(object sender, RoutedEventArgs e)
		{
			foreach (var a in afterLoaded)
				a();
			afterLoaded.Clear();
		}

		public ObservableCollection<object> MainItems { get; private set; }

		public void ShowShell()
		{
			this.Show();
		}

		public void Show(ShellRegion region, object data)
		{
			if (!IsLoaded)
			{
				afterLoaded.Add(delegate { Show(region, data); });
				return;
			}

			if (region == ShellRegion.Main)
			{
				if (MainItems.Contains(data))
					PositionBuySellTab.SelectedItem = data;
			}

			var view = Composition.GetView(data);
			switch (region)
			{
				case ShellRegion.MainToolBar:
					{
						MainToolbar.Items.Add(view);
					}
					break;
				case ShellRegion.Secondary:
					{
						var w = new Window();
						w.Owner = this;
						w.Content = view;
						w.Style = (Style)FindResource("WindowRegionStyle");
						w.Show();
					}
					break;
				case ShellRegion.Action:
					{
						ActionContent.Content = view;
					}
					break;
				case ShellRegion.Research:
					{
						ResearchView.Items.Add(view);
					}
					break;
			}
		}
	}
}

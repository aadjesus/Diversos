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
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.News.Controllers;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Galador.Applications;

namespace StockTraderRI.Modules.News.Article
{
	[Export(typeof(IArticlePresentationModel))]
	public class ArticlePresentationModel : ViewModelBase, IArticlePresentationModel, INotifyPropertyChanged
	{
		private readonly INewsFeedService newsFeedService;

		[ImportingConstructor]
		public ArticlePresentationModel(INewsFeedService newsFeedService)
		{
			this.newsFeedService = newsFeedService;
		}

		public INewsController Controller { get; set; }
		public ICommand ShowNewsCommand 
		{
			get 
			{
				if(mShowNewsCommand == null)
					mShowNewsCommand = new DelegateCommand(Controller.ShowNewsReader, () => HasArticle);
				return mShowNewsCommand;
			}
		}
		ICommand mShowNewsCommand;

		public void SetTickerSymbol(string companySymbol)
		{
			this.Articles = newsFeedService.GetNews(companySymbol);
		}

		#region SelectedArticle, HasArticle

		public NewsArticle SelectedArticle
		{
			get { return this.selectedArticle; }
			set
			{
				if (this.selectedArticle == value)
					return;
				this.selectedArticle = value;

				OnPropertyChanged(() => SelectedArticle);
				OnPropertyChanged(() => HasArticle);
			}
		}
		private NewsArticle selectedArticle;

		public bool HasArticle { get { return SelectedArticle != null; } }

		#endregion

		#region Articles

		public IList<NewsArticle> Articles
		{
			get { return this.articles; }
			private set
			{
				if (this.articles == value)
					return;

				this.articles = value;
				OnPropertyChanged(() => Articles);
			}
		}
		private IList<NewsArticle> articles;

		#endregion
	}
}

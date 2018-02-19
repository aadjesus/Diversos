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
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.News.Article;
using System.ComponentModel.Composition;
using Galador.Applications;

namespace StockTraderRI.Modules.News.Controllers
{
	[Export(typeof(INewsController))]
    public class NewsController : INewsController
    {
		private readonly IArticlePresentationModel articlePresentationModel;
		private readonly IShellView shell;

		[ImportingConstructor]
		public NewsController(IArticlePresentationModel articlePresentationModel, IShellView shell)
        {
            this.articlePresentationModel = articlePresentationModel;
			this.articlePresentationModel.Controller = this;
			this.shell = shell;
        }

        public void Run()
        {
			shell.Show(ShellRegion.Research, articlePresentationModel);

			Notifications.Subscribe<NewsController, TickerSymbolSelectedEvent>(
				this,
				(nc, te) => nc.ShowNews(te.Symbol),
				ThreadOption.UIThread
			);
        }

        public void ShowNews(string companySymbol)
        {
            this.articlePresentationModel.SetTickerSymbol(companySymbol);
        }

        public void CurrentNewsArticleChanged(NewsArticle article)
        {
			articlePresentationModel.SelectedArticle = article;
        }

		public void ShowNewsReader()
        {
			this.shell.Show(ShellRegion.Secondary, articlePresentationModel.SelectedArticle);
        }
    }
}

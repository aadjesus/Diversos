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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Galador.Applications;

namespace StockTraderRI.Modules.News.Article
{
    /// <summary>
    /// Interaction logic for ArticleView.xaml
    /// </summary>
	[DataView(typeof(ArticlePresentationModel))]
    public partial class ArticleView : UserControl
    {
        public ArticleView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NewsList.SelectedItem != null)
            {
                var storyboard = (Storyboard)this.Resources["Details"];
                storyboard.Begin();
            }
            else
            {
                var storyboard = (Storyboard)this.Resources["List"];
                storyboard.Begin();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
			var m = this.DataContext as ArticlePresentationModel;
            m.SelectedArticle = null;
        }
    }
}

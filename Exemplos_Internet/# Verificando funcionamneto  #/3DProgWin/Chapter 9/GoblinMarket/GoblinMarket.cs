//---------------------------------------------
// GoblinMarket.cs (c) 2007 by Charles Petzold
//---------------------------------------------
using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Resources;
using System.Windows.Xps.Packaging;
using Petzold.BookViewer3D;

namespace Petzold.GoblinMarket
{
    public partial class GoblinMarket : Page
    {
        public GoblinMarket()
        {
            // Access the document stored as an application resource.
            StreamResourceInfo info = Application.GetResourceStream(
                        new Uri("pack://application:,,/GoblinMarket.xps"));

            // Open a package from the resources stream.
            Package pack = Package.Open(info.Stream, FileMode.Open, 
                                                     FileAccess.Read);

            // Add a URI to the package store.
            string strUri = "memorystream://GoblinMarket.xps";
            Uri uriPackage = new Uri(strUri);
            PackageStore.AddPackage(uriPackage, pack);

            // Get the XPS file.
            XpsDocument xps = new XpsDocument(pack, CompressionOption.Normal, 
                                              strUri);

            // Get a paginator for the fixed documents in the XPS file.
            FixedDocumentSequence seq = xps.GetFixedDocumentSequence();
            DocumentPaginator paginator = seq.DocumentPaginator;

            // Plan is to get a Visual for each page in the document.
            Visual[] visuals = new Visual[paginator.PageCount];

            for (int i = 0; i < paginator.PageCount; i++)
                visuals[i] = paginator.GetPage(i).Visual;

            // Close the file.
            xps.Close();

            // Create BookViewport to display book.
            BookViewport viewer = new BookViewport(visuals);
            Content = viewer;
        }
    }
}

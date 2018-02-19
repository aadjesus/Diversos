using System;
using System.Collections.Generic;
using System.Text;

namespace ImageTest
{



    public class RSSImageFeed
    {
        private const string FLICKR_API_KEY = "c705bfbf75e8d40f584c8a946cf0834c";
        private const string MOST_RECENT = "http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=" + FLICKR_API_KEY;
        private const string INTERESTING = "http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=" + FLICKR_API_KEY;
        private const string ENTER_TAG = "http://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=" + FLICKR_API_KEY + "&tags=";
        private string url = MOST_RECENT;
        private int pageIndex = 0;
        private int columns = 5;
        private int rows = 2;
        private bool prevAvail = false;
        private bool nextAvail = false;


        public RSSImageFeed()
        {

        }

        public bool IsPrevAvail
        {
            get { return prevAvail; }
        }
        public bool IsNextAvail
        {
            get { return nextAvail; }
        }

        public int PageIndex
        {
            set { pageIndex = value; }
            get { return pageIndex; }
        }

	

        public bool LoadPictures(string searchType, string searchWord)
        {


            switch (searchType)
            {
                case "Most Recent":
                    this.url=MOST_RECENT;
                    break;
                case "Interesting":
                    this.url=INTERESTING;
                    break;
                case "By Search Word":
                    this.url = ENTER_TAG + searchWord;
                    break;
                default :
                    this.url = MOST_RECENT;
                    break;
            }

            try
            {
                //var xraw = XElement.Load(url);
                //var xroot = XElement.Parse(xraw.Xml);
                ////select the RSS data from Flickr, and use standard LINQ projection
                ////to store it within a new PhotoInfo which is an object of my own making
                //var photos = (from photo in xroot.Element("photos").Elements("photo")
                //    select new PhotoInfo
                //    { 
                //        Id = (string)photo.Attribute("id"),
                //        Owner = (string)photo.Attribute("owner"),
                //        Title = (string)photo.Attribute("title"),
                //        Secret = (string)photo.Attribute("secret"),
                //        Server = (string)photo.Attribute("server"),
                //        Farm = (string)photo.Attribute("Farm"),
                //    }).Skip(pageIndex * columns * rows).Take(columns * rows);

                ////set the allowable next/prev states
                //int count = photos.Count();

                //if (pageIndex == 0)
                //{
                //    this.prevAvail = false;
                //    this.nextAvail = true;
                //}
                //else
                //{
                //    this.prevAvail = true;
                //}
                ////see if there are less photos than sum(Columns * Rows) if there are less
                ////cant allow next operation
                //if (count < columns * rows)
                //{
                //    this.nextAvail = false;
                //}
                ////then write results out to a file, which can then be used by XPF Application
                //XDocument photosDoc = new XDocument();
                //XElement photosElement = new XElement("photos", from pi in photos
                //        select new XElement("photo",
                //            new XElement("url", pi.PhotoUrl(false)),
                //            new XElement("title", pi.Title))
                //    );
                //photosDoc.Add(photosElement);
                photosDoc.Save(@"c:\photos.xml");
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }



    
    }
}

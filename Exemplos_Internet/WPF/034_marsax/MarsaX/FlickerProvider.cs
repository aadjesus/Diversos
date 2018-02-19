using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace MarsaX
{

    /// Uses XLINQ to create a List<see cref="PhotoInfo">PhotoInfo</see> using an Rss feed.
    /// 
    /// The following links are useful information regarding how the Flickr API works 
    /// 
    /// http://www.flickr.com/services/api/misc.urls.html
    /// http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=
    /// http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=
    /// http://www.flickr.com/services/api/flickr.photos.search.html
    /// </summary>
    public class FlickerProvider
    {
        #region Data
        private const string FLICKR_API_KEY = "2c9cae43031e99b6b5e62a2bb2a1edbf";
        private const string MOST_RECENT = "http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=" + FLICKR_API_KEY;
        private const string INTERESTING = "http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=" + FLICKR_API_KEY;
        private const string SEARCH = "http://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=" + FLICKR_API_KEY + "&text={0}";
        #endregion

        #region LoadPicturesFromFeed
        /// <summary>
        /// Returns a List<see cref="PhotoInfo">PhotoInfo</see> which represent
        /// the latest Flickr images
        /// </summary>
        public static List<PhotoInfo> LoadLatestPictures()
        {
            try
            {
                var xraw = XElement.Load(MOST_RECENT);
                var xroot = XElement.Parse(xraw.ToString());
                var photos = (from photo in xroot.Element("photos").
                Elements("photo")
                              select new PhotoInfo
                              {
                                  ImageUrl =
                                  string.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg",
                                                (string)photo.Attribute("farm"),
                                                (string)photo.Attribute("server"),
                                                (string)photo.Attribute("id"),
                                                (string)photo.Attribute("secret"))
                              }).Take(Constants.ROWS * Constants.COLUMNS);

                return photos.ToList<PhotoInfo>();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
            return null;
        }


        /// <summary>
        /// Returns a List<see cref="PhotoInfo">PhotoInfo</see> which represent
        /// the most interesting Flickr images
        /// </summary>
        public static List<PhotoInfo> LoadInterestingPictures()
        {
            try
            {
                var xraw = XElement.Load(INTERESTING);
                var xroot = XElement.Parse(xraw.ToString());
                var photos = (from photo in xroot.Element("photos").
                Elements("photo")
                              select new PhotoInfo
                              {
                                  ImageUrl =
                                  string.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg",
                                                (string)photo.Attribute("farm"),
                                                (string)photo.Attribute("server"),
                                                (string)photo.Attribute("id"),
                                                (string)photo.Attribute("secret"))
                              }).Take(Constants.ROWS * Constants.COLUMNS);

                return photos.ToList<PhotoInfo>();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
            return null;
        }


        /// <summary>
        /// Returns a List<see cref="PhotoInfo">PhotoInfo</see> which represent
        /// the Flickr images that matched the KeyWord input parameter
        /// </summary>
        public static List<PhotoInfo> LoadPicturesKey(string keyWord)
        {
            try
            {
                var xraw = XElement.Load(string.Format(SEARCH, keyWord));
                var xroot = XElement.Parse(xraw.ToString());
                var photos = (from photo in xroot.Element("photos").
                Elements("photo")
                              select new PhotoInfo
                              {
                                  ImageUrl =
                                  string.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg",
                                                (string)photo.Attribute("farm"),
                                                (string)photo.Attribute("server"),
                                                (string)photo.Attribute("id"),
                                                (string)photo.Attribute("secret"))
                              }).Take(Constants.ROWS * Constants.COLUMNS);

                return photos.ToList<PhotoInfo>();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
            return null;
        }
        #endregion
    }
}

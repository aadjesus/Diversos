using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace YouViewer
{

    /// Uses XLINQ to create a List<see cref="YouTubeInfo">YouTubeInfo</see> using an Rss feed.
    /// 
    /// The following links are useful information regarding how the YouTube API works 
    /// 
    /// http://www.youtube.com/rssls
    /// </summary>
    public class YouTubeProvider
    {
        #region Data
        private const string MOST_POPULAR = "http://youtube.com/rss/global/top_viewed.rss";
        private const string SEARCH = "http://www.youtube.com/rss/tag/{0}.rss";
        #endregion

        #region Load Videos From Feed
        /// <summary>
        /// Returns a List<see cref="YouTubeInfo">PhotoInfo</see> which represent
        /// the most popular YouTube videos
        /// </summary>
        public static List<YouTubeInfo> LoadMostPopularVideos()
        {
            try
            {
                var xraw = XElement.Load(MOST_POPULAR);
                var xroot = XElement.Parse(xraw.ToString());
                var links = (from item in xroot.Element("channel").Descendants("item")
                             select new YouTubeInfo
                             {
                                 LinkUrl = item.Element("link").Value,
                                 EmbedUrl = GetEmbedUrlFromLink(item.Element("link").Value),
                                 ThumnailUrl = 
                                    item.Elements().Where(
                                            child => child.Name.ToString().Contains("thumbnail")
                                        ).Single().Attribute("url").Value

                             }).Take(20);

                return links.ToList<YouTubeInfo>();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
            return null;
        }

        /// <summary>
        /// Returns a List<see cref="YouTubeInfo">YouTubeInfo</see> which represent
        /// the YouTube videos that matched the keyWord input parameter
        /// </summary>
        public static List<YouTubeInfo> LoadVideosKey(string keyWord)
        {
            try
            {
                var xraw = XElement.Load(string.Format(SEARCH,keyWord));
                var xroot = XElement.Parse(xraw.ToString());
                var links = (from item in xroot.Element("channel").Descendants("item")
                             select new YouTubeInfo
                             {
                                 LinkUrl = item.Element("link").Value,
                                 EmbedUrl = GetEmbedUrlFromLink(item.Element("link").Value),
                                 ThumnailUrl =
                                    item.Elements().Where(
                                            child => child.Name.ToString().Contains("thumbnail")
                                        ).Single().Attribute("url").Value

                             }).Take(20);

                return links.ToList<YouTubeInfo>();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
            return null;
        }

    
        #endregion

        #region Private Methods

        /// <summary>
        /// Simple helper methods that tunrs a link string into a embed string
        /// for a YouTube item. 
        /// turns 
        /// http://youtube.com/?v=FhZ-HsiS8aI
        /// into
        /// http://www.youtube.com/v/FhZ-HsiS8aI&hl=en
        /// </summary>
        private static string GetEmbedUrlFromLink(string link)
        {
            try
            {
                string embedUrl = "http://www.";
                string startPart = link.Substring(link.IndexOf("you"));
                embedUrl += startPart.Substring(0, startPart.LastIndexOf(@"/"));
                embedUrl += "/v/";
                embedUrl += startPart.Substring(startPart.LastIndexOf("=") + 1);
                embedUrl += "&hl=en";
                return embedUrl;
            }
            catch
            {
                return link;
            }
        }
        #endregion
    }
}

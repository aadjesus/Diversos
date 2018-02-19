using System;
using System.Collections.Generic;
using System.Text;

namespace ImageTest
{
    public class PhotoInfo
    {
        private const string FLICKR_SERVER_URL = "http://static.flickr.com/";
        private const string FLICKR_PHOTO_URL = "http://www.flickr.com/photos/";

        public string Id;
        public string Owner;
        public string Title;
        public string Secret;
        public string Server;
        public string Farm;
        public bool IsPublic;
        public bool IsFriend;
        public bool IsFamily;
        public string PhotoUrl(bool small)
        {
            return FLICKR_SERVER_URL + this.Server + '/' + this.Id + '_' + this.Secret + (small ? "_s.jpg" : "_m.jpg");
        }
        public string PhotoPageUrl
        {
            get { return FLICKR_PHOTO_URL + this.Owner + '/' + this.Id; }
        }
    }
}

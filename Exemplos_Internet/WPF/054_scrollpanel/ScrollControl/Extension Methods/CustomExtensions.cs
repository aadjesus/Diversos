using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ScrollControl
{
    public static class  CustomExtensions
    {
        #region IsImageFile extension Methods
        /// <summary>
        /// IsImageFile using a predicate, returns true if file
        /// matches the predicate
        /// </summary>
        /// <param name="file">file name to check</param>
        /// <param name="isMatch">The match predicate</param>
        public static bool IsImageFile(this string file, 
                                            Predicate<string> isMatch)
        {
            return isMatch(file);
        }

        /// <summary>
        /// IsImageFile using a predicate, returns true if file
        /// contains .jpg/.png/.bmp
        /// </summary>
        /// <param name="file">file name to check</param>
        public static bool IsImageFile(this string file)
        {
            return
            file.Contains(".jpg") || 
            file.Contains(".png") || 
            file.Contains(".bmp");
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CycleTracks
{
  static class ImageHelper
  {
    public static byte[] LoadImage(this string file)
    {
      FileInfo fi = new FileInfo(file);
      byte[] fileBytes = new byte[fi.Length];
      using (Stream sr = fi.OpenRead())
      {
        sr.Read(fileBytes, 0, fileBytes.Length);
      }
      return fileBytes;
    }
  }
}

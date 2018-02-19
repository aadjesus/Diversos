using System;
using System.Text;
using System.Collections;
using System.Web.UI;

namespace ConsoleApplication1
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Encoding ascii = Encoding.ASCII;

//			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
//				"F:\\webdev\\raw.images.pk\\saripaya\\lonely Tree - Paey.jpg"
//				);
//			Goheer.EXIF.EXIFextractor er = new Goheer.EXIF.EXIFextractor(ref bmp,"\n");
//			foreach(System.Web.UI.Pair s in er )
//			{
//				Console.Write(s.First+" : " + s.Second +"\n");
//			}
//			if (er["Image Title"] == null)
//				er.setTag(0x320, "http://www.beautifulpakistan.com");
//			if (er["Artist"] == null)
//				er.setTag(0x13B, "http://www.beautifulpakistan.com");
//			if (er["User Comment"] == null)
//				er.setTag(0x9286, "http://www.beautifulpakistan.com");
//			if (er["Copyright"] == null)
//				er.setTag(0x8298, "http://www.beautifulpakistan.com");

			Goheer.EXIF.EXIFextractor er2 = new Goheer.EXIF.EXIFextractor("F:\\webdev\\raw.images.pk\\saripaya\\lonely Tree - Paey.jpg","","");
		
			foreach(System.Web.UI.Pair s in er2 )
			{
				Console.WriteLine(s.First+" : " + s.Second);
			}
			Console.WriteLine( er2["User Comment"] );
		}
		
		
}
}

using System;
using System.Drawing;

namespace SampleProject
{
	/// <summary>
	/// Summary description for SampleImages.
	/// </summary>
	public class SampleImages
	{
		public readonly static Image FACE00;
		public readonly static Image FACE01;
		public readonly static Image FACE02;
		public readonly static Image FACE04;

		static SampleImages()
		{
			System.Reflection.Assembly l_ExecAs = System.Reflection.Assembly.GetExecutingAssembly();

			FACE00 = System.Drawing.Image.FromStream(l_ExecAs.GetManifestResourceStream("SampleProject.Samples.FACE00.ICO"));
			FACE01 = System.Drawing.Image.FromStream(l_ExecAs.GetManifestResourceStream("SampleProject.Samples.FACE01.ICO"));
			FACE02 = System.Drawing.Image.FromStream(l_ExecAs.GetManifestResourceStream("SampleProject.Samples.FACE02.ICO"));
			FACE04 = System.Drawing.Image.FromStream(l_ExecAs.GetManifestResourceStream("SampleProject.Samples.FACE04.ICO"));
		}
	}
}

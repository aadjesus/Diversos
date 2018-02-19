using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace System.IO
{
	public static class StreamEx
	{
		public static void CopyTo(this Stream input, Stream output)
		{
			var buf = new byte[1 << 12];
			int nRead;
			while ((nRead = input.Read(buf, 0, buf.Length)) > 0)
				output.Write(buf, 0, nRead);
		}
	}
}

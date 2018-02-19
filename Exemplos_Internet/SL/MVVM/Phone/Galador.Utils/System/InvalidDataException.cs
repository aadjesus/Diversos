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

namespace System
{
	public class InvalidDataException : Exception
	{
		public InvalidDataException()
		{

		}
		public InvalidDataException(string msg)
			: base(msg)
		{

		}
		public InvalidDataException(string msg, Exception e)
			: base(msg, e)
		{

		}
	}
}

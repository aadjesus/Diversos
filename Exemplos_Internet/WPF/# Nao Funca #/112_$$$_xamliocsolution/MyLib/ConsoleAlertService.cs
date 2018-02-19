using System;

namespace MyLib
{
	/// <summary>Dummy alert service outputting on the console.</summary>
	public class ConsoleAlertService : IAlertService
	{
		#region IAlertService Members
		void IAlertService.RaiseAlert(string userRole, string message)
		{
			Console.WriteLine("Alert sent to " + userRole + ":  " + message);
		}
		#endregion
	}
}
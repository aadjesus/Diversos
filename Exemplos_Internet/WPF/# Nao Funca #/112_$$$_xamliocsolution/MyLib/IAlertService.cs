using System;

namespace MyLib
{
	/// <summary>Represents an alert service.</summary>
	public interface IAlertService
	{
		/// <summary>Raises an alert to a given user role.</summary>
		/// <param name="userRole"></param>
		/// <param name="message"></param>
		void RaiseAlert(string userRole, string message);
	}
}
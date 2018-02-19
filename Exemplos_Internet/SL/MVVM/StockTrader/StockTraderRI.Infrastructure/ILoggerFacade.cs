using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel.Composition;

namespace StockTraderRI.Infrastructure
{
	/// <summary>
	/// Defines values for the categories used by <see cref="ILoggerFacade"/>.
	/// </summary>
	public enum Category
	{
		/// <summary>
		/// Debug category.
		/// </summary>
		Debug,

		/// <summary>
		/// Exception category.
		/// </summary>
		Exception,

		/// <summary>
		/// Informational category.
		/// </summary>
		Info,

		/// <summary>
		/// Warning category.
		/// </summary>
		Warn
	}

	/// <summary>
	/// Defines values for the priorities used by <see cref="ILoggerFacade"/>.
	/// </summary>
	public enum Priority
	{
		/// <summary>
		/// No priority specified.
		/// </summary>
		None = 0,

		/// <summary>
		/// High priority entry.
		/// </summary>
		High = 1,

		/// <summary>
		/// Medium priority entry.
		/// </summary>
		Medium,

		/// <summary>
		/// Low priority entry.
		/// </summary>
		Low
	}

	public interface ILoggerFacade
	{
		/// <summary>
		/// Write a new log entry with the specified category and priority.
		/// </summary>
		/// <param name="message">Message body to log.</param>
		/// <param name="category">Category of the entry.</param>
		/// <param name="priority">The priority of the entry.</param>
		void Log(string message, Category category, Priority priority);
	}

	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(ILoggerFacade))]
	public class TraceLogger : ILoggerFacade
	{
		public void Log(string message, Category category, Priority priority)
		{
			Trace.WriteLine("[" + priority + "] " + message, category.ToString());
		}
	}
}

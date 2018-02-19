using System;
using System.Data;
using System.Diagnostics;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Magi.OutlookConnector
{
	/// <summary>
	/// Delegate declaration for each time an item is processed from an Outlook folder.
	/// </summary>
	public delegate void OutlookItemProcessed();

	/// <summary>
	/// This disposable class acts a translator for information stored in the user's Outlook folders.
	/// </summary>
	public class OutlookConnector : IDisposable
	{
		private Outlook.Application objOutlook = null;
		private Outlook.NameSpace objNamespace = null;
		private Outlook.MAPIFolder objFolder = null;
		public event OutlookItemProcessed ItemProcessed;

		public OutlookConnector()
		{
			objOutlook = new Outlook.ApplicationClass();
			objNamespace = objOutlook.GetNamespace("MAPI");
		}

		/// <summary>
		/// Close the Outlook application when this instance is disposed.
		/// </summary>
		public void Dispose()
		{
			if (objOutlook != null) objOutlook.Quit();
		}

		/// <summary>
		/// Get the number of items within the specified default Outlook folder.
		/// </summary>
		/// <param name="folder">outlook folder enumerated value</param>
		/// <returns>total number of items</returns>
		public int getFolderCount(Outlook.OlDefaultFolders folder)
		{
			objFolder = objNamespace.GetDefaultFolder(folder);
			return objFolder.Items.Count;
		}

		/// <summary>
		/// Retrieve a list of all appointments listed in the Outlook calendar.
		/// </summary>
		/// <returns>Calendar Items DataSet</returns>
		public DataSet getCalendarDataSet()
		{
			Outlook.AppointmentItem item;
			DataSet rv = new DataSet();
			rv.DataSetName = "Calendar";
			rv.Tables.Add("Appointment");
			rv.Tables[0].Columns.Add("Subject");
			rv.Tables[0].Columns.Add("Location");
			rv.Tables[0].Columns.Add("Start");
			rv.Tables[0].Columns.Add("End");
			rv.Tables[0].Columns.Add("AllDayEvent");
			rv.Tables[0].Columns.Add("Duration");
			rv.Tables[0].Columns.Add("Organizer");
			rv.Tables[0].Columns.Add("Importance");
			rv.Tables[0].Columns.Add("Sensitivity");
			rv.Tables[0].Columns.Add("Body");

			try
			{
				objFolder = objNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);
				Debug.WriteLine(objFolder.Items.Count + " Appointments found.");
				foreach (System.Object _item in objFolder.Items) 
				{
					item = (Outlook.AppointmentItem) _item;
					rv.Tables[0].Rows.Add(new object[] {
						item.Subject,
						item.Location,
						item.Start,
						item.End,
						item.AllDayEvent,
						item.Duration,
						item.Organizer,
						item.Importance,
						item.Sensitivity,
						item.Body
					});
					this.ItemProcessed();
				}
				Debug.WriteLine(rv.Tables[0].Rows.Count + " Appointments exported.");
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e);
			}
			return rv;
		}

		/// <summary>
		/// Retrieves a list of all the Outlook Contacts.
		/// </summary>
		/// <returns>Contact Items DataSet</returns>
		public DataSet getContactDataSet()
		{
			Outlook.ContactItem item;
			DataSet rv = new DataSet();
			rv.DataSetName = "Contacts";
			rv.Tables.Add("Contact");
			rv.Tables[0].Columns.Add("FirstName");
			rv.Tables[0].Columns.Add("LastName");
			rv.Tables[0].Columns.Add("CompanyName");
			rv.Tables[0].Columns.Add("Email");
			rv.Tables[0].Columns.Add("HomePhone");
			rv.Tables[0].Columns.Add("WorkPhone");

			try
			{
				objFolder = objNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);
				Debug.WriteLine(objFolder.Items.Count + " Contacts found.");
				foreach (System.Object _item in objFolder.Items) 
				{
					item = (Outlook.ContactItem) _item;
					rv.Tables[0].Rows.Add(new object[] {
						item.FirstName,
						item.LastName,
						item.CompanyName,
						item.Email1Address,
						item.HomeTelephoneNumber,
						item.BusinessTelephoneNumber
					});
					this.ItemProcessed();
				}
				Debug.WriteLine(rv.Tables[0].Rows.Count + " Contacts exported.");
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e);
			}
			return rv;
		}

		/// <summary>
		/// Retrieves a list of all emails in the Outlook Inbox.
		/// </summary>
		/// <returns>Inbox E-Mail Items DataSet</returns>
		public DataSet getInboxDataSet()
		{
			Outlook.MailItem item;
			DataSet rv = new DataSet();
			rv.DataSetName = "InboxEmails";
			rv.Tables.Add("Email");
			rv.Tables[0].Columns.Add("From");
			rv.Tables[0].Columns.Add("To");
			rv.Tables[0].Columns.Add("Cc");
			rv.Tables[0].Columns.Add("Subject");
			rv.Tables[0].Columns.Add("Received");
			rv.Tables[0].Columns.Add("Message");

			try
			{
				objFolder = objNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
				Debug.WriteLine(objFolder.Items.Count + " E-Mails found.");
				foreach (System.Object _item in objFolder.Items) 
				{
					item = (Outlook.MailItem) _item;
					rv.Tables[0].Rows.Add(new object[] {
						item.SenderEmailAddress,
						item.To,
						item.CC,
						item.Subject,
						item.ReceivedTime,
						item.Body
					});
					this.ItemProcessed();
				}
				Debug.WriteLine(rv.Tables[0].Rows.Count + " E-Mails exported.");
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e);
			}
			return rv;
		}

		/// <summary>
		/// Retrieves a list of all Outlook Notes.
		/// </summary>
		/// <returns>Note Items DataSet</returns>
		public DataSet getNoteDataSet()
		{
			Outlook.NoteItem item;
			DataSet rv = new DataSet();
			rv.DataSetName = "Notes";
			rv.Tables.Add("Note");
			rv.Tables[0].Columns.Add("Subject");
			rv.Tables[0].Columns.Add("Categories");
			rv.Tables[0].Columns.Add("CreationTime");
			rv.Tables[0].Columns.Add("LastModificationTime");
			rv.Tables[0].Columns.Add("Contents");

			try
			{
				objFolder = objNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderNotes);
				Debug.WriteLine(objFolder.Items.Count + " Notes found.");
				foreach (System.Object _item in objFolder.Items) 
				{
					item = (Outlook.NoteItem) _item;
					rv.Tables[0].Rows.Add(new object[] {
						item.Subject,
						item.Categories,
						item.CreationTime,
						item.LastModificationTime,
						item.Body
					});
					this.ItemProcessed();
				}
				Debug.WriteLine(rv.Tables[0].Rows.Count + " Notes exported.");
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e);
			}
			return rv;
		}

		/// <summary>
		/// Retrieves a list of all Outlook Tasks.
		/// </summary>
		/// <returns>Task Items DataSet</returns>
		public DataSet getTaskDataSet()
		{
			Outlook.TaskItem item;
			DataSet rv = new DataSet();
			rv.DataSetName = "Tasks";
			rv.Tables.Add("Task");
			rv.Tables[0].Columns.Add("Subject");
			rv.Tables[0].Columns.Add("StartDate");
			rv.Tables[0].Columns.Add("DueDate");
			rv.Tables[0].Columns.Add("Status");
			rv.Tables[0].Columns.Add("Contents");

			try
			{
				objFolder = objNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderTasks);
				Debug.WriteLine(objFolder.Items.Count + " Tasks found.");
				foreach (System.Object _item in objFolder.Items) 
				{
					item = (Outlook.TaskItem) _item;
					rv.Tables[0].Rows.Add(new object[] {
						item.Subject,
						item.StartDate,
						item.DueDate,
						item.Status,
						item.Body
					});
					this.ItemProcessed();
				}
				Debug.WriteLine(rv.Tables[0].Rows.Count + " Tasks exported.");
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e);
			}
			return rv;
		}
	}
}

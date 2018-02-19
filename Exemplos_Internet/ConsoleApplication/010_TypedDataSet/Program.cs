using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypedDataSet
{
	class Program
	{
		static void Main(string[] args)
		{
			PersonTable table = new PersonTable();
			table.PersonRowChanged += new PersonRowChangedDlgt(OnPersonRowChanged);
			PersonRow row = table.GetNewRow();
			table.Add(row);
		}

		static void OnPersonRowChanged(PersonTable sender, PersonRowChangedEventArgs args)
		{
			// This is silly example only for the purposes of illustrating using typed events.
			// Do not do this in real applications, because you would never use this Changed event
			// to validate row fields!
			if (args.Row.LastName != String.Empty)
			{
				throw new ApplicationException("The row did not initialize to an empty string for the LastName field.");
			}
		}
	}
}

using System;
using System.Data;

namespace TypedDataSet
{
	public delegate void PersonRowChangedDlgt(PersonTable sender, PersonRowChangedEventArgs args);

	public class PersonRowChangedEventArgs
	{
		protected DataRowAction action;
		protected PersonRow row;

		public DataRowAction Action
		{
			get { return action; }
		}

		public PersonRow Row
		{
			get { return row; }
		}

		public PersonRowChangedEventArgs(DataRowAction action, PersonRow row)
		{
			this.action = action;
			this.row = row;
		}
	}

	public class PersonTable : DataTable
	{
		public event PersonRowChangedDlgt PersonRowChanged;

		public PersonRow this[int idx]
		{
			get { return (PersonRow)Rows[idx]; }
		}

		public PersonTable()
		{
			base.Columns.Add(new DataColumn("LastName", typeof(string)));
            base.Columns.Add(new DataColumn("FirstName", typeof(string)));
		}

		public void Add(PersonRow row)
		{
            base.Rows.Add(row);
		}

		public void Remove(PersonRow row)
		{
            base.Rows.Remove(row);
		}

		public PersonRow GetNewRow()
		{
            PersonRow row = base.NewRow() as PersonRow;

			return row;
		}

		protected override Type GetRowType()
		{
			return typeof(PersonRow);
		}

		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new PersonRow(builder);
		}

		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);
			PersonRowChangedEventArgs args = new PersonRowChangedEventArgs(e.Action, (PersonRow)e.Row);
			OnPersonRowChanged(args);
		}

		protected virtual void OnPersonRowChanged(PersonRowChangedEventArgs args)
		{
			if (PersonRowChanged != null)
			{
				PersonRowChanged(this, args);
			}
		}
	}

	public class PersonRow : DataRow
	{
		public string LastName
		{
			get { return (string)base["LastName"]; }
			set { base["LastName"] = value; }
		}

		public string FirstName
		{
			get { return (string)base["FirstName"]; }
			set { base["FirstName"] = value; }
		}

		internal PersonRow(DataRowBuilder builder)
			: base(builder)
		{
			LastName = String.Empty;
			FirstName = String.Empty;
		}
	}
}

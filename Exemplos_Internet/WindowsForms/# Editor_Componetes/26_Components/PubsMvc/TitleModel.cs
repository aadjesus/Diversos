using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using Mvc.Components.Model;

namespace PubsMvc
{
	/// <summary>
	/// Model representing a publisher in the Pubs sample database.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxItemFilter("Mvc.Components.Controller", ToolboxItemFilterType.Require)]
	public class TitleModel : BaseModel, Mvc.Components.IConnectable
	{
		#region IConnectable implementation

		/// <summary>
		/// Receives the connection to use when accessing the DB.
		/// </summary>
		[Browsable(false)]
		public string ConnectionString 
		{ 
			get { return _cnstring; } 
			set { _cnstring = value; }
		} string _cnstring = String.Empty;

		#endregion

		#region Methods

		/// <summary>
		/// Loads the current model with data from the database matching the current ID.
		/// </summary>
		public void Load()
		{
			SqlConnection cn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand("SELECT * FROM titles WHERE title_id = '" + this.ID + "'", cn);
			try
			{
				cn.Open();
				SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				if (reader.Read())
				{
					this.ID = reader["title_id"] as string;
					this.Title = reader["title"] as string;
					this.Type = reader["type"] as string;
					this.Price = (decimal) reader["price"];
					this.Advance = (decimal) reader["advance"];
					this.Royalty = (int) reader["royalty"];
					this.YearSales = (int) reader["ytd_sales"];
					this.Notes = reader["notes"] as string;
					this.PublicationDate = (DateTime) reader["pubdate"];
				}
				reader.Close();
			}
			finally
			{
				if (cn.State != ConnectionState.Closed)
					cn.Close();
			}
		}

		#endregion

		#region Properties

		[Browsable(false)]
		[Bindable(true)]
		public string ID
		{
			get { return _id; }
			set { _id = value; }
		} string _id;

		[Browsable(false)]
		[Bindable(true)]
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		} string _title;

		[Browsable(false)]
		[Bindable(true)]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		} string _type;

		[Browsable(false)]
		[Bindable(true)]
		public decimal Price
		{
			get { return _price; }
			set { _price = value; }
		} decimal _price;

		[Browsable(false)]
		[Bindable(true)]
		public decimal Advance
		{
			get { return _advance; }
			set { _advance = value; }
		} decimal _advance;

		[Browsable(false)]
		[Bindable(true)]
		public int Royalty
		{
			get { return _royalty; }
			set { _royalty = value; }
		} int _royalty;

		[Browsable(false)]
		[Bindable(true)]
		public int YearSales
		{
			get { return _yearsales; }
			set { _yearsales = value; }
		} int _yearsales;

		[Browsable(false)]
		[Bindable(true)]
		public string Notes
		{
			get { return _notes; }
			set { _notes = value; }
		} string _notes;

		[Browsable(false)]
		[Bindable(true)]
		public DateTime PublicationDate
		{
			get { return _pubdate; }
			set { _pubdate = value; }
		} DateTime _pubdate;

		bool ShouldSerializeID() { return false; }
		bool ShouldSerializeTitle() { return false; }
		bool ShouldSerializeType() { return false; }
		bool ShouldSerializePrice() { return false; }
		bool ShouldSerializeAdvance() { return false; }
		bool ShouldSerializeRoyalty() { return false; }
		bool ShouldSerializeYearSales() { return false; }
		bool ShouldSerializeNotes() { return false; }

		#endregion
		
		#region Ctor & Designer stuff

		public TitleModel(IContainer container)
		{
			container.Add(this);
			//Change default name for the model
			this.ModelName = "Title";
			InitializeComponent();
		}

		public TitleModel()
		{
			//Change default name for the model
			this.ModelName = "Title";
			InitializeComponent();
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		#endregion
	}
}

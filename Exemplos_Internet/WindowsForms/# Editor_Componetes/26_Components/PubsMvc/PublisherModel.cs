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
	public class PublisherModel : BaseModel, Mvc.Components.IConnectable
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
		/// Deletes the currently loaded model from the database.
		/// </summary>
		public void Delete()
		{
			SqlConnection cn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand("DELETE FROM publishers WHERE pub_id = '" + this.ID + "'", cn);
			try
			{
				cn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (cn.State != ConnectionState.Closed)
					cn.Close();
			}
		}

		/// <summary>
		/// Loads the current model with data from the database matching the current ID.
		/// </summary>
		public void Load()
		{
			SqlConnection cn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand("SELECT * FROM publishers WHERE pub_id = '" + this.ID + "'", cn);
			try
			{
				cn.Open();
				SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				if (reader.Read())
				{
					this.ID = reader["pub_id"] as string;
					this.Name = reader["pub_name"] as string;
					this.City = reader["city"] as string;
					this.State = reader["state"] as string;
					this.Country = reader["country"] as string;
				}
				reader.Close();
			}
			finally
			{
				if (cn.State != ConnectionState.Closed)
					cn.Close();
			}
		}

		/// <summary>
		/// Updates or inserts a publisher in the database.
		/// </summary>
		public void Save()
		{
			SqlConnection cn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand(String.Format(
				@"UPDATE publishers SET pub_name='{1}', city='{2}', state='{3}', country='{4}'
					WHERE pub_id='{0}'
				IF @@ROWCOUNT = 0
					INSERT INTO publishers 
					(pub_id, pub_name, city, state, country) VALUES
					('{0}', '{1}', '{2}', '{3}', '{4}')", 
				this.ID, this.Name , this.City, this.State, this.Country), cn);
			try
			{
				cn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (cn.State != ConnectionState.Closed)
					cn.Close();
			}
		}

		#endregion

		#region Properties

		//EXPLAIN: DesignerSerializationVisibilityAttribute vs ShouldSerializeXX methods.

		[Browsable(false)]
		[Bindable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ID
		{
			get { return _id; }
			set { _id = value; }
		} string _id;

		[Browsable(false)]
		[Bindable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		} string _name;

		[Browsable(false)]
		[Bindable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string City
		{
			get { return _city; }
			set { _city = value; }
		} string _city;

		[Browsable(false)]
		[Bindable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string State
		{
			get { return _state; }
			set { _state = value; }
		} string _state;

		[Browsable(false)]
		[Bindable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Country
		{
			get { return _country; }
			set { _country = value; }
		} string _country;

		bool ShouldSerializeID() { return false; }
		bool ShouldSerializeName() { return false; }
		bool ShouldSerializeCity() { return false; }
		bool ShouldSerializeState() { return false; }
		bool ShouldSerializeCountry() { return false; }

		#endregion
		
		#region Ctor & Designer stuff

		public PublisherModel(IContainer container)
		{
			container.Add(this);
			//Change default name for the model
			this.ModelName = "Publisher";
			InitializeComponent();
		}

		public PublisherModel()
		{
			//Change default name for the model
			this.ModelName = "Publisher";
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using CarApplication.Converters;
using CustomControls;
using CustomControls.ComboBox;
using CustomControls.Data;
using CustomControls.Rule;
using DataModel;
using DataModel.PersonModel;

namespace CarApplication.View
{
	[TypeConverter( typeof( ListExpandableConverter ) )]
	public class ViewPersonCollection : IDisplay
	{
		#region Data Members

		private readonly Person _person;
		private ViewPersonCollection _changeablePerson;
		private ViewPersonCollection _changeableParent;

		private ViewPersonCollection _parent;
		private readonly List<ViewPersonCollection> _children = new List<ViewPersonCollection>();

		private List<ViewPersonCollection> _list;

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		private ViewPersonCollection()
		{
			this._person = new Person();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="person"></param>
		public ViewPersonCollection( Person person )
		{
			this._person = person;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Parent
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Parent" ), DisplayName( "Parent" )]
		public ViewPersonCollection Parent
		{
			get { return ( _parent ); }
			set { _parent = value; }
		}

		/// <summary>
		/// Get/Set for Children
		/// </summary>
		[TypeConverter( typeof( ListConverter<ViewPersonCollection> ) )]
		[Category( "Person" ), DescriptionAttribute( "Description of Name" ), DisplayName( "Children" )]
		public List<ViewPersonCollection> Children
		{
			get { return ( _children ); }
		}

		/// <summary>
		/// Get/Set for Name
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Name" ), DisplayName( "\t\t\t\t\tName" )]
		public string Name
		{
			get { return ( _person.Name ); }
			set { _person.Name = value; }
		}

		/// <summary>
		/// Get/Set for Street
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Street" ), DisplayName( "\t\t\t\tStreet" )]
		public string Street
		{
			get { return ( _person.Street ); }
			set { _person.Street = value; }
		}

		/// <summary>
		/// Get/Set for City
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of City" ), DisplayName( "\t\t\tCity" )]
		[LengthRule( 4, 20 )]
		public string City
		{
			get { return ( _person.City ); }
			set { _person.City = value; }
		}

		/// <summary>
		/// Get/Set for State
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of State" ), DisplayName( "\t\tState" )]
		[PatternRule( @"^([A-Z]){2}$" )]
		public string State
		{
			get { return ( _person.State ); }
			set { _person.State = value; }
		}

		/// <summary>
		/// Get/Set for Zip
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Zip" ), DisplayName( "\tZip" )]
		[PatternRule( @"^(\d{5}-\d{4})|(\d{5})$" )]
		public string Zip
		{
			get { return ( _person.Zip ); }
			set { _person.Zip = value; }
		}

		/// <summary>
		/// Get/Set for Phone
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Phone" ), DisplayName( "Phone" )]
		[PatternRule( @"^\(\d{3}\)\s*\d{3}\-\d{4}$" )]
		public string Phone
		{
			get { return ( _person.Phone ); }
			set { _person.Phone = value; }
		}



		/// <summary>
		/// 
		/// </summary>
		[DefaultValue( "" )]
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "GetPeopleList", true, "OnAddedEventHandler" )]
		[Category( "Misc" ), Description( "Description of ChoosePerson" ), DisplayName( "Choose a Person" )]
		public ViewPersonCollection ChoosePerson
		{
			get { return ( _changeablePerson ); }
			set { _changeablePerson = value; }
		}

		/// <summary>
		/// Get/Set for ChangeableParent
		/// </summary>
		[DefaultValue( "" )]
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "GetPeopleList" )]
		[Category( "Misc" ), Description( "Description of ChooseParent" ), DisplayName( "Choose a Parent" )]
		public ViewPersonCollection ChooseParent
		{
			get { return ( _changeableParent ); }
			set { _changeableParent = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Person Person
		{
			get { return ( this._person ); }
		}

		#endregion

		#region Methods - Private

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private List<ViewPersonCollection> GetPeopleList()
		{
			if ( _list == null )
			{
				_list = new List<ViewPersonCollection>();
				GetChildren( _list, this );
			}

			return ( _list );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="list"></param>
		/// <param name="personCollection"></param>
		private static void GetChildren( List<ViewPersonCollection> list, ViewPersonCollection personCollection )
		{
			if ( personCollection != null )
			{
				list.Add( personCollection );

				if ( personCollection.Children != null )
				{
					foreach ( ViewPersonCollection child in personCollection.Children )
					{
						GetChildren( list, child );
					}
				}
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="arg"></param>
		private void OnAddedEventHandler( object sender, ObjectCreatedEventArgs arg )
		{
			if ( arg != null )
			{
				ViewPersonCollection collection = arg.DataValue as ViewPersonCollection;
				if ( collection != null )
				{
					collection.Name = "New Person #" + new Random().Next( 1, 100 );
					this.ChooseParent.Children.Add( collection );
					this._list.Add( collection );
				}
			}
		}

		#endregion

		#region IDisplay Members

		/// <summary>
		/// 
		/// </summary>
		public string Text
		{
			get { return ( _person.Name ); }
		}

		#endregion

		#region Overrides

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ( _person.Name );
		}

		#endregion
	}
}

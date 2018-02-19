using System.ComponentModel;
using CarApplication.Converters;
using CustomControls.ComboBox;
using CustomControls.Rule;
using DataModel;
using DataModel.PersonModel;

namespace CarApplication.View
{
	[TypeConverter( typeof( PersonConverter ) )]
	public class ViewPerson : IDisplay
	{
		#region Data Members

		private readonly Person _person;

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		private ViewPerson()
		{
			this._person = new Person();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="person"></param>
		public ViewPerson( Person person )
		{
			this._person = person;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Name
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Name" ), OrderedDisplayName( 1, 6, "Name" )]
		public string Name
		{
			get { return ( _person.Name ); }
			set { _person.Name = value; }
		}

		/// <summary>
		/// Get/Set for Street
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Street" ), OrderedDisplayName( 2, 6, "Street" )]
		public string Street
		{
			get { return ( _person.Street ); }
			set { _person.Street = value; }
		}

		/// <summary>
		/// Get/Set for City
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of City" ), OrderedDisplayName( 3, 6, "City" )]
		[LengthRule( 4, 20 )]
		public string City
		{
			get { return ( _person.City ); }
			set { _person.City = value; }
		}

		/// <summary>
		/// Get/Set for State
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of State" ), OrderedDisplayName( 4, 6, "State" )]
		[PatternRule( @"^([A-Z]){2}$" )]
		public string State
		{
			get { return ( _person.State ); }
			set { _person.State = value; }
		}

		/// <summary>
		/// Get/Set for Zip
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Zip" ), OrderedDisplayName( 5, 6, "Zip" )]
		[PatternRule( @"^(\d{5}-\d{4})|(\d{5})$" )]
		public string Zip
		{
			get { return ( _person.Zip ); }
			set { _person.Zip = value; }
		}

		/// <summary>
		/// Get/Set for Phone
		/// </summary>
		[Category( "Person" ), DescriptionAttribute( "Description of Phone" ), OrderedDisplayName( 6, 6, "Phone" )]
		[PatternRule( @"^\(\d{3}\)\s*\d{3}\-\d{4}$" )]
		public string Phone
		{
			get { return ( _person.Phone ); }
			set { _person.Phone = value; }
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
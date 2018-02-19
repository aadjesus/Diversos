using System.Windows.Forms;
using CarApplication.View;
using DataModel.CarModel.Cars;
using DataModel.CarModel.Wheels;
using DataModel.CarModel.Wheels.Falken;
using DataModel.PersonModel;

namespace CarApplication
{
	public partial class Form1 : Form
	{
		private readonly Car _car;
		private readonly Person _person;
		private readonly Wheel _wheel;

		private readonly ViewCar _viewCar;
		private readonly ViewPerson _viewPerson;
		private readonly ViewPerson2 _viewPerson2;
		private readonly ViewWheel _viewWheel;
		private ViewPersonCollection _viewAncestor;


		public Form1()
		{
			InitializeComponent();

			_car = new Car();
			_person = new Person();
			_wheel = new FalkenZE912();
			CreateAncestors();

			_person.Name = "Bob The Builder";

			_viewCar = new ViewCar( _car );
			_viewPerson = new ViewPerson( _person );
			_viewPerson2 = new ViewPerson2( _person );
			_viewWheel = new ViewWheel( _wheel );
			_viewWheel.Person = _viewPerson2;
		}

		private void CreateAncestors()
		{
			ViewPersonCollection view;

			_viewAncestor = AddChild( null, "Grandpa" );
			_viewAncestor.ChooseParent = _viewAncestor;

			view = AddChild( _viewAncestor, "Father" );
			AddChild( view, "Ann" );
			AddChild( view, "Bill" );
			AddChild( view, "Charley" );

			AddChild( _viewAncestor, "Uncle Albert" );
			AddChild( _viewAncestor, "Aunt Jemima" );
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		private static ViewPersonCollection AddChild( ViewPersonCollection parent, string name )
		{
			Person person;
			ViewPersonCollection view;

			person = new Person();
			person.Name = name;

			view = new ViewPersonCollection( person );

			if ( parent != null )
				parent.Children.Add( view );

			return ( view );
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
		{
			if ( e.Node.Name.Equals( "treeNode1" ) )
			{
				this.propertyGridControl1.SelectedObject = _viewCar;
			}
			else if ( e.Node.Name.Equals( "treeNode2" ) )
			{
				this.propertyGridControl1.SelectedObject = _viewWheel;
			}
			else if ( e.Node.Name.Equals( "treeNode3" ) )
			{
				this.propertyGridControl1.SelectedObject = _viewPerson;
			}
			else if ( e.Node.Name.Equals( "treeNode4" ) )
			{
				this.propertyGridControl1.SelectedObject = _viewAncestor;
			}
			
		}
	}
}
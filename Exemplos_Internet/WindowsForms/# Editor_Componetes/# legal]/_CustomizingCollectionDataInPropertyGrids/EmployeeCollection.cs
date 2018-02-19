using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PropertyGridSample
{
	/// <summary>
	/// Type safe collection class for Employee objects. Extends the base class 
	/// CollectionBase to inherit base collection functionality.
	/// Implementation of ICustomTypeDescvriptor to provide customized type description.
	/// </summary>
	public class EmployeeCollection : CollectionBase, ICustomTypeDescriptor
	{
		#region collection impl
		
		/// <summary>
		/// Adds an employee object to the collection
		/// </summary>
		/// <param name="emp"></param>
		public void Add( Employee emp )
		{
			this.List.Add( emp );
		}
		
		/// <summary>
		/// Removes an employee object from the collection
		/// </summary>
		/// <param name="emp"></param>
		public void Remove( Employee emp )
		{
			this.List.Remove( emp );
		}
		
		/// <summary>
		/// Returns an employee object at index position.
		/// </summary>
		public Employee this[ int index ] 
		{
			get
			{
				return (Employee)this.List[index];
			}
		}

		#endregion

		// Implementation of interface ICustomTypeDescriptor 
		#region ICustomTypeDescriptor impl 

		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}


		/// <summary>
		/// Called to get the properties of this type. Returns properties with certain
		/// attributes. this restriction is not implemented here.
		/// </summary>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		/// <summary>
		/// Called to get the properties of this type.
		/// </summary>
		/// <returns></returns>
		public PropertyDescriptorCollection GetProperties()
		{
			// Create a collection object to hold property descriptors
			PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

			// Iterate the list of employees
			for( int i=0; i<this.List.Count; i++ )
			{
				// Create a property descriptor for the employee item and add to the property descriptor collection
				EmployeeCollectionPropertyDescriptor pd = new EmployeeCollectionPropertyDescriptor(this,i);
				pds.Add(pd);
			}
			// return the property descriptor collection
			return pds;
		}

		#endregion
	}
}
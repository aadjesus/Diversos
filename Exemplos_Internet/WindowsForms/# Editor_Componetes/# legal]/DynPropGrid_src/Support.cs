using System;
using System.Collections.Specialized;

namespace Testproperty
{
	//Interface for the class to show in propertygrid
	interface ICustomClass
	{
		PropertyList PublicProperties
		{
			get;
			set;
		}
	}


	public class PropertyList : NameObjectCollectionBase
	{		
		public void Add(Object value)  
		{
			//The key for the object is taken from the object to insert
			this.BaseAdd(((myProperty)value).Name, value );
		}

		public void Remove(String key)  
		{
			this.BaseRemove( key );
		}

		public void Remove(int index)  
		{
			this.BaseRemoveAt( index );
		}		

		public void Clear()  
		{
			this.BaseClear();
		}

		public myProperty this[ String key ]  
		{
			get  
			{
				return (myProperty)(this.BaseGet(key));
			}
			set  
			{
				this.BaseSet(key,value);
			}
		}

		public myProperty this[ int indice ]  
		{
			get  
			{
				return (myProperty)(this.BaseGet(indice));
			}
			set  
			{
				this.BaseSet(indice,value);
			}
		}

	}


	public class myProperty
	{
		string name=string.Empty;
		string category=string.Empty;

		public myProperty()
		{
		}

		public myProperty(string pname,string pcat)
		{
			name=pname;
			category=pcat;
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name=value;
			}
		}

		public string Category
		{
			get
			{
				return category;
			}
			set
			{
				category=value;
			}
		}
	}

}

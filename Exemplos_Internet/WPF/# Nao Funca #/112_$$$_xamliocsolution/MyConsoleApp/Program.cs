using System;
using System.Collections.Generic;
using System.Windows;

using XamlIoc;

using MyLib;

namespace MyConsoleApp
{
	/// <summary>Entry point of the application.</summary>
	public static class Program
	{
		/// <summary>Lets validate some persons!</summary>
		public static void Main()
		{
			TestPersonValidation();
			TestGenerics();
			TestShared();
		}

		private static void TestPersonValidation()
		{
			Console.WriteLine();
			Console.WriteLine("TestPersonValidation");
			Console.WriteLine();

			PersonValidator validator =
				ObjectFactory.GetObject<PersonValidator>(typeof(PersonValidator).FullName);

			validator.ValidatePersons(42, 12, 79, 14);
		}

		private static void TestGenerics()
		{
			Console.WriteLine();
			Console.WriteLine("TestGenerics");
			Console.WriteLine();

			IList<int> emptyList = ObjectFactory.GetObject<IList<int>>("myListOfInt");
			IList<int> populatedList = ObjectFactory.GetObject<IList<int>>("myListOfIntPopulated");

			Console.WriteLine("emptyList Capacity:  " + ((List<int>)emptyList).Capacity);

			Console.WriteLine("populatedList Content:");
			foreach (int i in populatedList)
			{
				Console.WriteLine(i);
			}
		}

		private static void TestShared()
		{
			Console.WriteLine();
			Console.WriteLine("TestShared");
			Console.WriteLine();

			Person objectFactoryPerson1 = ObjectFactory.GetObject<Person>("objectFactoryPerson");
			Person objectFactoryPerson2 = ObjectFactory.GetObject<Person>("objectFactoryPerson");
			Person staticResourcePerson1 = ObjectFactory.GetObject<Person>("staticResourcePerson");
			Person staticResourcePerson2 = ObjectFactory.GetObject<Person>("staticResourcePerson");

			Console.WriteLine(
				"object factory persons equal:  "
				+ object.ReferenceEquals(objectFactoryPerson1, objectFactoryPerson2));
			Console.WriteLine(
				"static resource persons equal:  "
				+ object.ReferenceEquals(staticResourcePerson1, staticResourcePerson2));
			Console.WriteLine(
				"object factory addresses equal:  "
				+ object.ReferenceEquals(objectFactoryPerson1.Address, objectFactoryPerson2.Address));
			Console.WriteLine(
				"static resource addresses equal:  "
				+ object.ReferenceEquals(staticResourcePerson1.Address, staticResourcePerson2.Address));
		}
	}
}
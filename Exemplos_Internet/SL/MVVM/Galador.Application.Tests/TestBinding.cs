using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galador.Applications;

namespace Galador.Application.Tests
{
	[TestClass]
	public class TestPOCOSyn
	{
		[TestMethod]
		public void TestSync()
		{
			var p1 = new Person();
			var p2 = new Person();
			POCOBinding.Create<Person, Person, int>(p1, aP => aP.Boss.Age, p2, aP2 => aP2.Age);

			p1.Boss = new Person();
			p1.Boss.Age = 23;
			Assert.AreEqual(p1.Boss.Age, p2.Age);
		}

		[TestMethod]
		public void TestPathUpdate()
		{
			var p1 = new Person();
			var pp = PropertyPath.Create<string>(() => p1.Boss.Name);

			Assert.AreEqual(pp.Value, null);

			//.......
			p1.Boss = new Person();
			Assert.AreEqual(pp.Value, null);

			p1.Boss.Name = "Lloyd";
			Assert.AreEqual(pp.Value, "Lloyd");

			pp.Value = "Foo";
			Assert.AreEqual(p1.Boss.Name, "Foo");
		}

	}
}

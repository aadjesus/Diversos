using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galador.Applications;
using System.ComponentModel.DataAnnotations;
using Galador.Applications.Validation;
using System.ComponentModel;

namespace Galador.Application.Tests
{
	[TestClass]
	public class ViewModelTests
	{
		[TestMethod]
		public void IsPropertyChangedRaised()
		{
			var p = new Person();

			string lastP = null;
			p.PropertyChanged += (o, args) => { lastP = args.PropertyName; };

			p.Age = 23;
			Assert.AreEqual("Age", lastP);

			p.Name = "dhejkw";
			Assert.AreEqual("Name", lastP);
		}

		[TestMethod]
		public void Validation()
		{
			var p = new Person();
			var ei = (IDataErrorInfo)p;

			Assert.IsNotNull(ei.Error);

			p.Age = -12;
			Assert.IsNotNull(ei["Age"]);
			p.Age = 3;
			Assert.IsNull(ei["Age"]);

			p.Name = "cbhk";
			Assert.IsNull(ei["Name"]);

			p.LastName = "cbhk";
			Assert.IsNull(ei["LastName"]);

			Assert.IsNull(ei.Error);
		}
	}
}

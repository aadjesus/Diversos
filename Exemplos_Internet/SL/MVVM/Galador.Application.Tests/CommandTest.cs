using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galador.Applications;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Galador.Application.Tests
{
	[TestClass]
	public class CommandTest
	{
		[ClassInitialize()]
		public static void Initialize(TestContext testContext)
		{
			Composition.Reset();
			Composition.Register(typeof(CompositionTest).Assembly);
		}

		#region TestCanExecuteChanged()

		[TestMethod]
		public void TestCanExecuteChanged1()
		{
			var p = new Person();
			var save = new DelegateCommand(p.Save, () => p.CanSave);

			bool canCalled = false;
			save.CanExecuteChanged += delegate { canCalled = true; };

			p.Age = 23;
			Assert.IsTrue(canCalled);
			Assert.IsFalse(save.CanExecute());

			p.Name = "kd;w";
			p.LastName = "dwjlk";
			Assert.IsTrue(save.CanExecute());
		}

		[TestMethod]
		public void TestCanExecuteChanged2()
		{
			var p = new Foo();
			var dc = p.GetDoCommand();

			Assert.IsFalse(dc.CanExecute("heu"));

			p.IsFixed = false;
			Assert.IsTrue(dc.CanExecute("heu"));
			Assert.IsFalse(dc.CanExecute("snafu"));

			dc.Execute("snafu");
			Assert.AreNotEqual("snafu", p.String);

			dc.Execute("hello");
			Assert.AreEqual("hello", p.String);
		}

		#endregion

		#region class Foo

		class Foo
		{
			public string String { get; set; }

			public void Set(string s)
			{
				String = s;
			}

			public bool CanSet(string s) 
			{
				return !IsFixed && s != "snafu"; 
			}

			public bool IsFixed 
			{
				get { return mIsFixed; }
				set
				{
					if(value == IsFixed)
						return;
					mIsFixed = value;
					if (IsFixedChanged != null)
						IsFixedChanged(this, EventArgs.Empty);
				}
			}
			bool mIsFixed = true;

			public event EventHandler IsFixedChanged;

			public ICommand GetDoCommand()
			{
				return new DelegateCommand<string>(Set, s => CanSet(s), () => IsFixedChanged);
			}
		}

		#endregion

		#region TestForeachCommand()

		[TestMethod]
		public void TestForeachCommand()
		{
			var f = new Foo();
			var fc = f.GetDoCommand();
			f.IsFixed = true;

			int nCalled = 0;
			var each = new ForeachCommand<ICommand>(aC => aC, new ObservableCollection<ICommand>());
			each.CanExecuteChanged += delegate { nCalled++; };

			each.Collection.Add(fc); // nCalled = 1
			Assert.IsFalse(each.CanExecute(null));

			f.IsFixed = false; // nCalled = 2
			Assert.IsTrue(each.CanExecute(null));
			Assert.AreEqual(2, nCalled);

			var p = new Person();
			var pc = new DelegateCommand(p.Save, () => p.CanSave);

			each.Collection.Add(pc); // nCalled 3
			Assert.IsFalse(each.CanExecute(null));

			p.Age = 23; // nCalled = 4
			Assert.IsFalse(each.CanExecute(null));
			Assert.AreEqual(4, nCalled);

			p.Name = "dekj"; // nCalled = 5
			Assert.IsFalse(each.CanExecute(null));
			Assert.AreEqual(5, nCalled);

			p.LastName = "dsew"; // nCalled = 6
			Assert.IsTrue(each.CanExecute(null));
			Assert.AreEqual(7, nCalled); // Initial changed as well, nCalled = 7
		}

		#endregion
	}
}

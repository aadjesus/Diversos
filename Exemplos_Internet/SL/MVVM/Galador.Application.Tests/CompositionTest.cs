using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using Galador.Applications;
using System.ComponentModel.Composition;

namespace Galador.Application.Tests
{
	[TestClass]
	public class CompositionTest
	{
		[ClassInitialize()]
		public static void Initialize(TestContext testContext)
		{
			Composition.Reset();
			Composition.Register(typeof(CompositionTest).Assembly);
			Composition.CurrentContext = ServiceContext.TestTime;
		}

		#region GetViewXXX() 

		#region Data classes

		class Data
		{
		}
		class Data2
		{
		}

		[DataView(typeof(Data))]
		class View1 : FrameworkElement
		{
		}

		[DataView(typeof(Data), "location2")]
		class View2 : FrameworkElement
		{
		}

		#endregion

		[TestMethod]
		public void GetView()
		{
			var d = new Data();
			var v = Composition.GetView(d);

			Assert.IsNotNull(v);
			Assert.AreEqual(typeof(View1), v.GetType());
			Assert.AreEqual(d, ((FrameworkElement)v).DataContext);
		}

		[TestMethod]
		public void GetViewHonorLocation()
		{
			var d = new Data();
			var v = Composition.GetView(d, "location2");

			Assert.IsNotNull(v);
			Assert.AreEqual(typeof(View2), v.GetType());
			Assert.AreEqual(d, ((FrameworkElement)v).DataContext);
		}

		[TestMethod]
		public void GetViewNotFallbackLocation()
		{
			var d = new Data();
			var v = Composition.GetView(d, "location3");
			Assert.AreEqual(d, v);
		}

		[TestMethod]
		public void GetViewData()
		{
			var d = new Data2();
			var v = Composition.GetView(d);

			Assert.AreEqual(d, v);
		}

		#endregion

		#region ExportServiceXXX()

		[TestMethod]
		public void ExportServiceContext()
		{
			Composition.Compose(this);
			Assert.AreEqual(3, Services.Length);
			Assert.IsTrue(Services.FirstOrDefault(aS => aS is Service1) != null, "service import doesn't contains Service1");
			Assert.IsTrue(Services.FirstOrDefault(aS => aS is Service2) != null, "service import doesn't contains Service2");
			Assert.IsTrue(Services.FirstOrDefault(aS => aS is Service3) == null, "service import contains Service3");
			Assert.IsTrue(Services.FirstOrDefault(aS => aS is Service4) != null, "service import doesn't contains Service4");
		}

		[ImportMany(typeof(IMyService))]
		public IMyService[] Services;

		public interface IMyService
		{
			void Do();
		}

		[Export(typeof(IMyService))]
		class Service1 : IMyService
		{
			public void Do() { }
		}

		[ExportService(ServiceContext.All, typeof(IMyService))]
		class Service2 : IMyService
		{
			public void Do() { }
		}

		[ExportService(ServiceContext.DesignTime, typeof(IMyService))]
		class Service3 : IMyService
		{
			public void Do() { }
		}

		[ExportService(ServiceContext.TestTime, typeof(IMyService))]
		class Service4 : IMyService
		{
			public void Do() { }
		}

		#endregion
	}
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galador.Applications;

namespace Galador.Application.Tests
{
	[TestClass]
	public class NotificationsTests
	{
		[ClassInitialize()]
		public static void Initialize(TestContext testContext)
		{
			Composition.Reset();
			Composition.Register(typeof(CompositionTest).Assembly);
		}

		public class MessageData
		{
			public string Info { get; set; }
		}

		#region TestRegister

		[TestMethod]
		public void TestRegister()
		{
			list1.Clear();
			list2.Clear();
			Notifications.Register(this, ThreadOption.PublisherThread);
			Notifications.Publish(new MessageData { Info = "h" });
			Assert.AreEqual(1, list1.Count);
			Assert.AreEqual(1, list2.Count);
			Assert.AreEqual(list1[0], list2[0]);
		}

		[NotificationHandler]
		public void Listen1(MessageData md)
		{
			list1.Add(md);
		}
		List<MessageData> list1 = new List<MessageData>();
		[NotificationHandler]
		public void Listen2(MessageData md)
		{
			list2.Add(md);
		}
		List<MessageData> list2 = new List<MessageData>();

		#endregion

		#region TestSusbcribe

		[TestMethod]
		public void TestSubscribe()
		{
			Notifications.Subscribe<NotificationsTests, MessageData>(null, StaticSubscribed, ThreadOption.PublisherThread);
			Notifications.Publish(new MessageData { });
			Assert.IsTrue(staticSubscribedReceived);

			staticSubscribedReceived = false;
			Notifications.Unsubscribe<NotificationsTests, MessageData>(null, StaticSubscribed);
			Notifications.Publish(new MessageData { });
			Assert.IsFalse(staticSubscribedReceived);
		}

		static void StaticSubscribed(NotificationsTests t, MessageData md)
		{
			staticSubscribedReceived = true;
		}
		static bool staticSubscribedReceived = false;

		#endregion

		#region TestNoInstance

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void TestNoInstance()
		{
			// this.InstanceSubscribed is an instance method, it prevents garbage collection
			// unacceptable!
			Notifications.Subscribe<NotificationsTests, MessageData>(this, InstanceSubscribed);
		}

		void InstanceSubscribed(NotificationsTests self, MessageData md)
		{
			staticSubscribedReceived = true;
		}

		#endregion

		#region TestGC

		[TestMethod]
		public void TestNotifGC()
		{
			var list = new List<string>();
			Prepare1(list);

			Notifications.Publish("hello");

			GC.Collect();

			Notifications.Publish("bye");

			Assert.AreEqual(1, list.Count);
		}
		void Prepare1(List<string> list)
		{
			var l = new Listener(list);
			Notifications.Register(l, ThreadOption.PublisherThread);
		}

		[TestMethod]
		public void TestNotifGC2()
		{
			var list = new List<string>();
			Prepare2(list);

			Notifications.Publish("hello");

			GC.Collect();

			Notifications.Publish("bye");

			Assert.AreEqual(1, list.Count);
		}
		void Prepare2(List<string> list)
		{
			var ler = new Listener(list);
			Notifications.Subscribe<Listener, string>(ler, (l, s) => l.Received(s), ThreadOption.PublisherThread);
		}

		class Listener
		{
			public Listener(List<string> list)
			{
				this.messages = list;
			}
			List<string> messages;

			[NotificationHandler]
			public void Received(string msg)
			{
				messages.Add(msg);
			}
		}

		#endregion
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Web;
using System.Web.UI;

using Mvc.Components;
using Mvc.Components.Controller;
using Mvc.Components.Design;
using Mvc.Components.Model;

namespace PubsMvc
{
	[ToolboxItem(true)]
	public class PublisherController : BaseController
	{
		#region Ctor & Designer stuff

		private System.ComponentModel.IContainer components;

		public PublisherController(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public PublisherController()
		{
			InitializeComponent();
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.publisher = new PubsMvc.PublisherModel(this);
			this.bookTitle = new PubsMvc.TitleModel(this);
			// 
			// publisher
			// 
			this.publisher.ConnectionString = "";
			this.publisher.ModelName = "Publisher";
			// 
			// bookTitle
			// 
			this.bookTitle.ConnectionString = "";
			this.bookTitle.ModelName = "Title";
			this.bookTitle.PublicationDate = new System.DateTime(((long)(0)));

		}
		#endregion

		#endregion

		#region Methods

		public void LoadTitle(string id)
		{
			bookTitle.ID = id;
			bookTitle.Load();
			RaiseModelChanged(bookTitle);
		}

		public void LoadTitle()
		{
			bookTitle.Load();
			RaiseModelChanged(bookTitle);
		}

		public void DeletePublisher()
		{
			// Could refactor to call a business object that saves the business entity.
			publisher.Delete();
			RaiseModelChanged(publisher);
		}

		public void LoadPublisher()
		{
			// Could refactor to call a business object that saves the business entity.
			publisher.Load();
			RaiseModelChanged(publisher);

		}

		public void SavePublisher()
		{
			// Could refactor to call a business object that saves the business entity.
			publisher.Save();
			RaiseModelChanged(publisher);
		}

		#endregion

		private PubsMvc.PublisherModel publisher;
		private PubsMvc.TitleModel bookTitle;

		#region Public properties

		[Category("MVC")]
		[Bindable(true)]
		[Description("Gets/Sets the connection string of the model.")]
		[RecommendedAsConfigurable(true)]
		public string ConnectionString
		{
			get { return _connection; }
			set 
			{ 
				_connection = value; 
				//Propagate setting to connectable models.
				foreach (IComponent model in Components)
					if (model is IConnectable)
						((IConnectable)model).ConnectionString = value;
			}
		} string _connection;

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Cx;
using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;

namespace Cx.Designer.App
{
	static class Program
	{
		public static Form mainForm;
		public static Form currentForm;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			EventHelpers.LogEvents = true;

			try
			{
				// Initialize all components.
				CxApp designer = CxApp.Initialize(@"..\..\..\Cx.DataService\bin\debug\Cx.DataService.dll", Path.GetFullPath("cxDesigner.xml"));
				EventHelpers.App = designer;

				// Once initialized, get our App instance.	We create an app instance so it is registered in our component list for wireups.
				App app = designer.GetComponent<App>();

				// Now we want to initialize the designer model.  For now, we're passing
				// in the same data service and metadata file, but in a more sophisticated implementation
				// we could let the user pick the metadata file and also the data service associated with that metadata.

				// app.Initialize(@"..\..\..\Cx.DataService\bin\debug\Cx.DataService.dll", Path.GetFullPath("cxdesigner.xml"));

				// Register all controls, including the menubar, which generates the events automatically for the menu items.
				mainForm = new Form1(designer.VisualComponents);
				// Do wireup after the menubar has been registered and menu item events are generated.
				designer.WireUpComponents();
				// Initialize the process.
				// app.Initialize(Path.GetFullPath("addComponent.xml"));
				// Run the app.
				Application.Run(mainForm);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error During Startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static Size UpdateExtents(ICxVisualComponent comp, Size extents)
		{
			if ( (comp.Location != null) && (comp.Size != null) )
			{
				string[] loc = comp.Location.Split(',');
				Point p = new Point(Convert.ToInt32(loc[0].Trim()), Convert.ToInt32(loc[1].Trim()));
				string[] size = comp.Size.Split(',');
				Size s = new Size(Convert.ToInt32(size[0].Trim()), Convert.ToInt32(size[1].Trim()));

				int dx = p.X + s.Width;
				int dy = p.Y + s.Height;

				if (dx > extents.Width)
				{
					extents.Width = dx;
				}

				if (dy > extents.Height)
				{
					extents.Height = dy;
				}
			}

			return extents;
		}
	}

	[CxExplicitEvent("Initialize")]
	[CxExplicitEvent("Save")]
	[CxExplicitEvent("FormInitialized")]
	public class App : ICxBusinessComponentClass
	{
		protected EventHelper initialize;
		protected EventHelper save;
		protected EventHelper formInitialized;

		public string Caption
		{
			get { return Program.currentForm.Text; }
			set { Program.currentForm.Text = value; }
		}

		public App()
		{
			initialize = EventHelpers.CreateEvent<String>(this, "Initialize");
			save = EventHelpers.CreateEvent<string>(this, "Save");
			formInitialized = EventHelpers.CreateEvent(this, "FormInitialized");
		}

		public void Initialize(string configUri)
		{
			initialize.Fire(configUri);
		}

		[CxConsumer]								   
		public void ShowModalForm(object sender, CxEventArgs<string> args)
		{									  
			// Create the form first and assign it to the current form so that any
			// property initialization supported by this App class can work with the
			// this new form.
			Form form = new Form();
			Program.currentForm = form;

			using (CxApp dlg = CxApp.Initialize(@"..\..\..\Cx.DataService\bin\debug\Cx.DataService.dll", Path.GetFullPath(args.Data)))
			{
				Size extents = new Size(0, 0);

				foreach (ICxVisualComponent comp in dlg.VisualComponents)
				{
					((ICxVisualComponentClass)comp.Instance).Register(form, comp);
					extents = Program.UpdateExtents(comp, extents);
				}

				dlg.WireUpComponents();
				form.Size = extents + new Size(25, 40);
				form.Activated += new EventHandler(OnActivated);
				formInitialized.Fire();
				form.ShowDialog();
				// The dispose method call is critical to remove wireups to components used in this dialog.
			}
		}
				
		/// <summary>
		/// Saves the last activated form.  See CloseDialog below.
		/// </summary>
		protected void OnActivated(object sender, System.EventArgs e)
		{
			Program.currentForm = (Form)sender;
		}

		[CxConsumer]
		public void CloseDialog(object sender, System.EventArgs args)
		{
			// We use the current form instead of Form.ActiveForm because the latter is null when debugging events, 
			// because VS gets focus and so our app is no longer the current form!
			Program.currentForm.Close();
		}

		[CxConsumer]
		public void ShowSaveAsDialog(object sender, CxEventArgs<string> args)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = args.Data;
			DialogResult ret = sfd.ShowDialog();

			if (ret == DialogResult.OK)
			{
				save.Fire(sfd.FileName);
			}
		}

		[CxConsumer]
		public void SetCaption(object sender, CxEventArgs<string> args)
		{
			Program.mainForm.Text = "Cx Designer - "+Path.GetFileName(args.Data);
		}
	}
}

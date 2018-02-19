using System.Xml;		// for XMLTextWriter and XMLTextReader
using System.IO;		// for StreamReader and StreamWriter

// SAS Enterprise Guide addins namespace
using SAS.Shared.AddIns;
using SAS.Tasks.Toolkit;

namespace EGAddin.CalculateLag
{
	/// <summary>
	/// Calculate Lag/Dif custom task for use in SAS Enterprise Guide or SAS Add-In for Microsoft Office
	/// </summary>
    [ClassId("9F259B91-8490-4457-B9A9-CF088713AE28")]
    [IconLocation("CalculateLag.function.ico")]
    [InputRequired(InputResourceType.Data)]
    [Version(4.2)]
	public class Lag : SAS.Tasks.Toolkit.SasTask
	{

		#region Properties and members

		public enum eFunction
		{
			Lag,
			Dif
		}

		private eFunction _function=eFunction.Lag;
		/// <summary>
		/// Which function to calc: lag or dif
		/// </summary>
		public eFunction Function
		{
			get { return _function; }
			set { _function = value; }
		}

		private int _n = 1;
		/// <summary>
		/// By how many records to lag/dif
		/// </summary>
		public int n 
		{
			get { return _n; }
			set { value = _n; }
		}

		private string _lagcolumn="";
		/// <summary>
		/// Column to Lag/dif
		/// </summary>
		public string LagColumn
		{
			get { return _lagcolumn; }
			set { _lagcolumn = value; }
		}
		#endregion

		#region Ctor
		public Lag()
		{
            InitializeComponent();
		}
		#endregion


		private string MakeOutDataName()
		{
			return string.Format("WORK.{0}_{1}",Consumer.ActiveData.Member,_function==eFunction.Lag ? "LAG" : "DIF");
		}

        public override int OutputDataCount
        {
            get { return 1; }
        }

        public override System.Collections.Generic.List<ISASTaskDataDescriptor> OutputDataDescriptorList
        {
            get
            {
                System.Collections.Generic.List<ISASTaskDataDescriptor> outputData = new System.Collections.Generic.List<ISASTaskDataDescriptor>();
                outputData.Add(SAS.Shared.AddIns.SASTaskDataDescriptor.CreateLibrefDataDescriptor(Consumer.AssignedServer, "WORK", 
                    string.Format("{0}_{1}",Consumer.ActiveData.Member,_function==eFunction.Lag ? "LAG" : "DIF"), string.Empty));
                return outputData;
            }
        }

		public override string  GetSasCode()
		{
			string outdata=MakeOutDataName();
			string codetemplate = SAS.Tasks.Toolkit.Helpers.UtilityFunctions.ReadFileFromAssembly("CalculateLag.CalculateLag.sas");
			codetemplate = codetemplate.Replace("{outdata}",outdata);
			codetemplate = codetemplate.Replace("{inputdata}",string.Format("{0}.{1}",Consumer.ActiveData.Library,Consumer.ActiveData.Member));
			string stmt = "";
			if (_lagcolumn.Length >0)
			{
				if (_function==eFunction.Lag)
					stmt = string.Format("lag{1}_{0} = lag{1}({0});",_lagcolumn,_n.ToString());
				else
					stmt = string.Format("dif{1}_{0} = dif{1}({0});",_lagcolumn, _n.ToString());
			}
			
			codetemplate = codetemplate.Replace("{function_assign}",stmt);
			return codetemplate;
		}


        
		override public SAS.Shared.AddIns.ShowResult Show(System.Windows.Forms.IWin32Window Owner)
		{
			// Show the default form for this custom task
			LagForm dlg = new LagForm();
			dlg.Consumer = Consumer;
			dlg.Text = Label;
			
			if (_lagcolumn.Length>0)
				dlg.LagColumn = _lagcolumn;

			dlg.Function = _function;

			dlg.n = _n;

			if (dlg.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				_lagcolumn = dlg.LagColumn;
				_function = dlg.Function;
				_n = dlg.n;

				return SAS.Shared.AddIns.ShowResult.RunNow;
			}
			else
				return SAS.Shared.AddIns.ShowResult.Canceled;
		}

        public override string GetXmlState()
        {
            StringWriter sw = new StringWriter();

            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.WriteStartElement("Lag");
            writer.WriteElementString("Function", XmlConvert.ToString((int)_function));
            writer.WriteElementString("Column", _lagcolumn);
            writer.WriteElementString("N", XmlConvert.ToString(_n));
            writer.WriteEndElement();
            writer.Close();

            return sw.ToString();
        }


        public override void RestoreStateFromXml(string xmlState)
        {
            if (xmlState != null && xmlState.Length > 0)
            {
                try
                {
                    StringReader sr = new StringReader(xmlState);
                    XmlTextReader reader = new XmlTextReader(sr);
                    reader.ReadStartElement("Lag");
                    _function = (eFunction)XmlConvert.ToInt32(reader.ReadElementString("Function"));
                    _lagcolumn = reader.ReadElementString("Column");
                    _n = XmlConvert.ToInt32(reader.ReadElementString("N"));
                    reader.ReadEndElement();
                    reader.Close();
                }
                catch
                {
                }
            }

        }

        private void InitializeComponent()
        {
            // 
            // Lag
            // 
            this.GeneratesReportOutput = false;
            this.ProcsUsed = "DATA step";
            this.ProductsRequired = "BASE";
            this.TaskCategory = "SAS Examples";
            this.TaskDescription = "Calculate lag or diff for a column in a data set.";
            this.TaskName = "Calculate Lag/Dif";
        }
    }
}

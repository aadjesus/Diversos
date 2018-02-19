using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace Uchukamen.CPUMeter
{
	/// <summary>
	/// This class is used for saving and loading configuration settings.
	/// 
	/// </summary>
	public class ConfigrationFile
	{

		// Configuration Data to be Serialized/Deserialized.
		public Point location;
		public Size size;
		public bool alwaysTop;

		
		private const string confFile = "CPUMeter.conf";
		private string confFilePath = null;

		public ConfigrationFile()
		{
			confFilePath = Application.UserAppDataPath	+ @"\" + confFile;
		}

		public void SaveAppSetting()
		{
			StreamWriter sw = null;
			XmlSerializer xmlSerializer = null;
			try
			{
				xmlSerializer = new XmlSerializer( 
					typeof(ConfigrationFile));
				sw = new StreamWriter(confFilePath);
				xmlSerializer.Serialize(sw, this);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message); 
			}
			finally
			{
				if(sw != null)
				{
					sw.Close();
				}
			}
		}

		public void LoadAppSetting()
		{
			XmlSerializer xmlSerializer = null;
			FileStream fs = null;

			try
			{
				// Create an XmlSerializer for the ApplicationSettings type.
				xmlSerializer = new XmlSerializer(typeof(ConfigrationFile));
				// If the config file exists, open it.
				FileInfo confFileInfo = new FileInfo(confFilePath);
				if(confFileInfo.Exists)
				{
					fs = confFileInfo.OpenRead();
					ConfigrationFile config = 
						(ConfigrationFile)xmlSerializer.Deserialize(fs);
					this.location= config.location;
					this.size= config.size;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if(fs != null)
				{
					fs.Close();
				}
			}
		}
	}
}


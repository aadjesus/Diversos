/*
 * Erstellt mit SharpDevelop.
 * Benutzer: hmurray
 * Datum: 01.07.2010
 * Zeit: 23:19
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CPControlDesigner
{
	/// <summary>
	/// Description of EnterControlNameDialog.
	/// </summary>
	public partial class EnterControlNameDialog : Form
	{
		public EnterControlNameDialog()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public string ControlName{
			get{
				return tbControlName.Text;
			}
			set{
				tbControlName.Text = value;
			}
		}
	}
}

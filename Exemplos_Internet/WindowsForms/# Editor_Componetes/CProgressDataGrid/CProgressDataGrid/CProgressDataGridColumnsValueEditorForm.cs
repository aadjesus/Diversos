using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CProgressControls
{
	#region utils
	public class utils 
	{
		public static ArrayList GetColArrayList(string sItems)
		{
			ArrayList aList = new ArrayList();

			int nStart = 0;
			
			while(nStart != -1)
			{
				nStart = sItems.IndexOf(",");

				if (nStart != -1)
				{
					if (nStart + 1 == sItems.Length || nStart + 1 > sItems.Length)
					{
						return aList;
					}

					string sItem = sItems.Substring(nStart + 1);
					int nLen = sItems.Length - sItem.Length;
					string sIndex = sItems.Substring(0, nLen - 1);
					int nIndex = Convert.ToInt32(sIndex);
					sItems = sItem;
					aList.Add(nIndex);
				}
			}

			if (sItems.Length > 0)
			{
				int nIndex = Convert.ToInt32(sItems);
				aList.Add(nIndex);
			}

			return aList;
		}
	}
	#endregion

	/// <summary>
	/// The list item class. Maps the text between its index. It is possible to optimize it using the hastable...
	/// </summary>
	class sListItem
	{
		public string sText;
		public int nIndex;
	}

	/// <summary>
	/// Summary description for CProgressDataGridColumnsEditorForm.
	/// </summary>
	public class CProgressDataGridColumnsValueEditorForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string sData = null;

		private ArrayList aColumnsOrig = new ArrayList();
		private ArrayList aColumnsSel = new ArrayList();

		private string m_oValue = null;
		private CProgressDataGrid m_oGrid = null;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button4;

		private bool m_bDataChanged = false;

		public bool Changed
		{
			get {return m_bDataChanged;}			
		}

		public CProgressDataGridColumnsValueEditorForm(string oValue, CProgressDataGrid oGrid)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// copy the input parameters
			m_oValue = oValue;
			m_oGrid = oGrid;

			if (m_oGrid.TableStyles.Count == 0)
			{
				MessageBox.Show(this, "No table style defined. Please specify some style first", "warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				return;
			}

			// get the style of the data grid... well, this is the limitation, we support only one table style 
			// and the table style must already exist
			DataGridTableStyle oStyle  = m_oGrid.TableStyles[0];

			if (oStyle != null)
			{
				int nCol = 0;

				// for every grid column style in the grid style, get its column heading and position and
				// add it to the selection list box
				foreach(DataGridColumnStyle oColStyle in oStyle.GridColumnStyles)
				{
					sListItem oItem = new sListItem();

					oItem.nIndex = nCol;
					oItem.sText = oColStyle.HeaderText;

					listBox1.Items.Add(oItem.sText);

					aColumnsOrig.Add(oItem);

					nCol ++;
				}
			}
			else
			{
				MessageBox.Show(this, "No grid style defined.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (oStyle != null)
			{
				// add all already selected columns to the second list box
				foreach(int nCol in utils.GetColArrayList(m_oGrid.ProgressColumns))
				{					
					sListItem oItem = new sListItem();

					oItem.nIndex = nCol;
					oItem.sText = oStyle.GridColumnStyles[nCol].HeaderText;

					aColumnsSel.Add(oItem);

					listBox2.Items.Add(oItem.sText);
				}
			}

			// for every already added column heading remove it from the box of columns to select from
			foreach(string sItem in listBox2.Items)
				listBox1.Items.Remove(sItem);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button3 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button4 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox2
			// 
			this.listBox2.Location = new System.Drawing.Point(200, 0);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(152, 173);
			this.listBox2.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = ">";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(160, 144);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(32, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "<";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// listBox1
			// 
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(152, 173);
			this.listBox1.TabIndex = 3;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(80, 0);
			this.button3.Name = "button3";
			this.button3.TabIndex = 4;
			this.button3.Text = "Ok";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.button3,
																				 this.button4});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 181);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(352, 32);
			this.panel1.TabIndex = 5;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(200, 0);
			this.button4.Name = "button4";
			this.button4.TabIndex = 4;
			this.button4.Text = "Cancel";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// CProgressDataGridColumnsValueEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.listBox1,
																		  this.button1,
																		  this.listBox2,
																		  this.button2});
			this.Name = "CProgressDataGridColumnsValueEditorForm";
			this.Text = "Progress Grid Columns Editor";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The handler for the add to select button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
		{
			if (listBox1.SelectedItem == null)
				return;

			// get the selected text and appropriate index
			string sText = (string)listBox1.SelectedItem;
			sListItem oText = GetSelectedItem(aColumnsOrig, sText);

			// add it to the selected ones
			listBox2.Items.Add(oText.sText);
			aColumnsSel.Add(oText);

			// remove it from the list
			listBox1.Items.Remove(oText.sText);
			aColumnsOrig.Remove(oText);
		}

		/// <summary>
		/// Handler for the remove from button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, System.EventArgs e)
		{
			if (listBox2.SelectedItem == null)
				return;

			// get the text and its appropriate index
			string sText = (string)listBox2.SelectedItem;
			sListItem oText = GetSelectedItem(aColumnsSel, sText);

			// add it back to the selection list
			listBox1.Items.Add(oText.sText);
			aColumnsOrig.Add(oText);

			// remove it from the selected items list
			listBox2.Items.Remove(oText.sText);
			aColumnsSel.Remove(oText);
		}

		/// <summary>
		/// Function finds the item object via its text in the specified arraylist object
		/// </summary>
		/// <param name="oList"></param>
		/// <param name="sText"></param>
		/// <returns></returns>
		private sListItem GetSelectedItem(ArrayList oList, string sText)
		{
			foreach(sListItem sItemText in oList)
			{
				if (sItemText.sText == sText)
					return sItemText;
			}

			return null;
		}

		/// <summary>
		/// The OK button handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button3_Click(object sender, System.EventArgs e)
		{
			// iterate the listbox 2 and add it to the columns
			foreach(sListItem sItem in aColumnsSel)
			{
				m_oValue += sItem.nIndex.ToString() + ",";
				sData = new string(m_oValue.ToCharArray());

				if (sData.LastIndexOf(",") == sData.Length - 1)
					sData = sData.Substring(0, sData.Length - 1);

				if (sData.IndexOf(" ") == 0)
					sData = sData.Substring(1);
			}

			// set the changed property
			m_bDataChanged = true;

			Close();
		}

		/// <summary>
		/// The cancel button handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button4_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}

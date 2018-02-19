// Form1.cs: Main Form for CodeTreeView utility
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using IvanZ.CSParser;
using Mono.CSharp;
using System.CodeDom;

namespace CodeTreeView
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Form about;
		private ErrorMessage errForm=null;
		private int[] linesLength;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			about=new About();
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Open...";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "About";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(168, 457);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(168, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 457);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.HideSelection = false;
			this.richTextBox1.Location = new System.Drawing.Point(171, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(557, 457);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFile);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(728, 457);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.richTextBox1,
																		  this.splitter1,
																		  this.treeView1});
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "CodeView";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			about.ShowDialog();
		
		}

		

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.ShowDialog();
			
		
		}

		FileStream f;
		string fname;

		private void OpenFile(object sender, CancelEventArgs args)
		{
			if (!args.Cancel)
			{
				fname=openFileDialog1.FileName;
				f=(FileStream) openFileDialog1.OpenFile();
				richTextBox1.Text=new StreamReader(f).ReadToEnd();
				richTextBox1.SelectAll();
				richTextBox1.SelectionIndent=20;
				richTextBox1.Select(0,0);
				CountLinesLength();
				f.Position=0;
				CreateTree();
			}
		}

		void CountLinesLength()
		{
			string[] lines = richTextBox1.Lines;
			linesLength = new int[lines.Length];
			for (int i=0; i<lines.Length; i++)
			linesLength[i]=lines[i].Length;
		}

		void CreateTree()
		{
			StringWriter sw =new StringWriter();
			Console.SetOut(sw);
			Console.SetError(sw);
           
			CSharpParser p= new CSharpParser(fname, f, null);
			
			int errno= p.parse();
			if (errno>0 || Report.Warnings>0)
				ShowError(sw);
			CodeCompileUnit unit= p.Builder.CurrCompileUnit;
			if (unit!=null)
			{
				treeView1.BeginUpdate();
				treeView1.Nodes.Clear();
				new TreeAnalyzer(new TreeCreator(treeView1),unit).DoAnalysis();
				treeView1.EndUpdate();
			}
			


		}

		void ShowError(StringWriter sw)
		{
			if (errForm==null)
				errForm=new ErrorMessage();
			errForm.Listing=sw.ToString();
			errForm.ShowDialog();
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			CodeLinePragma lp= (CodeLinePragma) e.Node.Tag;
			if (lp!=null)
			{
				int line=lp.LineNumber;
				GoToLine(line);
			}


		}

		private void GoToLine(int line)
		{
			line=line-1;
			if (line>=0 && line<richTextBox1.Lines.Length)
			{
				int pos = CountPos(line);
				richTextBox1.Focus();
				richTextBox1.Select(pos,linesLength[line]);
				richTextBox1.ScrollToCaret();
			}
		}

		private int CountPos(int line)
		{
			int sum=0;
			for (int i=0; i<line; i++)
				sum+=linesLength[i]+1;
			return sum;
		}

		
	}
}

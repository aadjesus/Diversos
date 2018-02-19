using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Snippy
{
	public class Help : Form
	{
		// Fields
		private Button button1;
		private IContainer components = null;
		private RichTextBox richTextBox1;

		// Methods
		public Help()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void Help_Load(object sender, EventArgs e)
		{
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Text = "How to get started: \n\nThis code snippet editor will help you write snippets that conform to the following schema: http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet.\n\nThe following xml is a simple example of what this will produce:\n<CodeSnippets xmlns=\"http://schemas.microsoft.com//VisualStudio//2005//CodeSnippet\">\n   <CodeSnippet Format=\"1.0.0\">\n       <Header>\n       <Title>\n              My Snippet\n         </Title>\n       </Header>\n       <Snippet>\n           <Code Language=\"CSharp\">\n               <![CDATA[MessageBox.Show(\"Hello World\");]]>\n           </Code>\n       </Snippet>\n   </CodeSnippet>\n</CodeSnippets>\n\nThe following fields are required for a snippet to work inside of Visual Studio:  Title, SnippetTypes, Code, Language.\n\nTitle        - This is the name of the snippet that will be visible in the code snippet picker and the code snippet manager.\nShortcut     - If you want to invoke your snippet through the keyboard, you can assign it a shortcut.  Pressing the shortcut and hitting \"Tab\" will insert your new snippet in the editor.\nSnippetTypes - Expansion is for a snippet that inserts text.  SurroundsWith is for snippets that will surround a block of text such as a for loop\nLanguage     - This is required to know which type of language the snippet you are creating will work for.  The currently supported lanugages are VB, C#, J# and XML\nCode         - The actual code that will be spit into the editor is required here.  In the above example, this is the text that is wrapped in the CDATA section.  You do not need to include the CDATA block\n\n\nAdding a Literal or Object:\n   The Literal element is used to identify a replacement for a piece of code that is entirely contained within the snippet, but will likely be customized after it is inserted into the code. For example, literal strings, numeric values, and some variable names should be declared as literals.\n   The Object element is used to identify an item that is required by the code snippet but is likely to be defined outside of the snippet itself. For example, Windows Forms controls, ASP.NET controls, object instances, and type instances should be declared as objects. Object declarations require that a type be specified.\nUse the Add button on the literals/objects section to add a new literal.   ID and default value are the only required fields.\nNow that you have created literals and objects, you need a way to use them in the code that will be inserted by the code snippet. You reference the literals and objects you have declared in the Declarations element by placing $ symbols at the beginning and end of the value in the literal or object's ID elementTo reference a literal or object in a Code element, place $ symbols at the beginning and end of the literal or object's ID element value. For example, if a literal has an ID element that contains the value MyID, you would reference that literal in the Code text box with $MyID$.\n\n\nAfter creating your snippet, you need to save it to a location that Visual Studio will be able to access it from.  In Visual Studio, go to the Tools menu and choose Code Snippet Manager. Here you can select the language that the snippets you are creating refer to.  From there you will be able to see the directories where you can place the snippet.  Copy one of these locations and save your snippet there. Visual Studio wil then automatically pick up your newly created snippet.\n";
		}

		private void InitializeComponent()
		{
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.richTextBox1.Location = new System.Drawing.Point(12, 12);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(601, 437);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "Snippy Help";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(538, 455);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// Help
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(625, 483);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.richTextBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Help";
			this.ShowInTaskbar = false;
			this.Text = "Help on Snippy...";
			this.Load += new System.EventHandler(this.Help_Load);
			this.ResumeLayout(false);

		}
	}
}


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;


namespace client
{
	/// <summary>
	/// This is a client program.
	/// Creates and sends a UserName or a Kerberos Token to call and execute a Service.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textLong1;
		private System.Windows.Forms.TextBox textLong2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
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
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textLong1 = new System.Windows.Forms.TextBox();
			this.textLong2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(40, 152);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Invoke";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(88, 44);
			this.textBox2.Name = "textBox2";
			this.textBox2.PasswordChar = '*';
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(240, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(128, 72);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Authorization type";
			// 
			// radioButton2
			// 
			this.radioButton2.Checked = true;
			this.radioButton2.Location = new System.Drawing.Point(8, 40);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(112, 24);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Kerberos Token";
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(8, 16);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(112, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "UserName Token";
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Location = new System.Drawing.Point(240, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 80);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "UserName Password";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Password";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "User Name";
			// 
			// textLong1
			// 
			this.textLong1.Location = new System.Drawing.Point(72, 24);
			this.textLong1.Name = "textLong1";
			this.textLong1.TabIndex = 6;
			this.textLong1.Text = "100";
			// 
			// textLong2
			// 
			this.textLong2.Location = new System.Drawing.Point(72, 64);
			this.textLong2.Name = "textLong2";
			this.textLong2.TabIndex = 7;
			this.textLong2.Text = "200";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 8;
			this.label3.Text = "Param 1";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Param 2";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 222);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textLong2);
			this.Controls.Add(this.textLong1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Web Services Security Tokens";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			groupBox2.Visible= radioButton1.Checked;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				//default option selected is Kerberos.
				string option="Kerberos";

				if (radioButton1.Checked)
					option="UserName";
				if (radioButton2.Checked)
					option="Kerberos";

				//declare any Security Token
				SecurityToken token=null;
				switch (option)
				{
					case "UserName":
					{
						try
						{
							//create a username Token.
							UsernameToken unToken=new UsernameToken(textBox1.Text,textBox2.Text,PasswordOption.SendPlainText);
							//assign the any SecurityToken an Username Token.
							token=unToken;
						}
						catch(Exception ex)
						{
							MessageBox.Show(ex.Message);
							return;
						}
						break;
					}
					case "Kerberos":
					{
						try
						{
							//create a kerberos Token.
							KerberosToken kToken = new KerberosToken(  System.Net.Dns.GetHostName() );
							//assign the any SecurityToken an Username Token.
							token=kToken;
						}
						catch(Exception ex)
						{
							MessageBox.Show(ex.Message);
							return;
						}
						break;
					}
					default:
					{
						break;
					}
				}

				if (token == null)
					throw new ApplicationException( "Unable to obtain security token." );

				// Create an instance of the web service proxy that has been generated.
				SecureServiceProxy.Service1Wse proxy=new client.SecureServiceProxy.Service1Wse();

				//set the time to live to any value.
				proxy.RequestSoapContext.Security.Timestamp.TtlInSeconds = 60;


				// Add the SecurityToken to the SOAP Request Context.
				proxy.RequestSoapContext.Security.Tokens.Add( token );

				// Sign the SOAP message with a signatureobject.
				proxy.RequestSoapContext.Security.Elements.Add( new MessageSignature( token ) );


				// Create and Send the request
				long a=long.Parse(textLong1.Text);
				long b=long.Parse(textLong2.Text);
				//call the web service.
				long result=proxy.perform(a,b);
				//Display the result.
				MessageBox.Show(a + " + " + b + " = " + result.ToString());
			}
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
				return;
			}
		}


		private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			//show or hide the username/password entry fields.
			groupBox2.Visible= radioButton1.Checked;
		}


	}
}

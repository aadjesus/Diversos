using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "foo.exe", true);
            //            parameters.GenerateExecutable = true;
            //            CompilerResults results = csc.CompileAssemblyFromSource(parameters,
            //            @"using System.Linq;
            //            class Program 
            //            {
            //                public static void Main(string[] args) 
            //                {
            //                    var q = from i in Enumerable.Rnge(1,100)
            //                          where i % 2 == 0
            //                          select i;
            //                }
            //            }");

            //            results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
            //            System.Windows.Forms.MessageBox.Show("ssssssssssss");

            StreamWriter streamWriter = File.CreateText("CSharpFriends.cs");
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine("namespace CSharpFriendsRocks");
            streamWriter.WriteLine("{");
            streamWriter.WriteLine("    class CSharpFriends");
            streamWriter.WriteLine("    {");
            streamWriter.WriteLine("        public void HelloWorld()");
            streamWriter.WriteLine("        { ");
            streamWriter.WriteLine("            System.Windows.Forms.MessageBox.Show(\"ssssssssssss\"); ");
            streamWriter.WriteLine("        }");
            streamWriter.WriteLine("    }");
            streamWriter.WriteLine("}");


            //streamWriter.WriteLine("using System;");
            //streamWriter.WriteLine("using System.Collections.Generic;");
            //streamWriter.WriteLine("using System.ComponentModel;");
            //streamWriter.WriteLine("using System.Data;");
            //streamWriter.WriteLine("using System.Drawing;");
            //streamWriter.WriteLine("using System.Linq;");
            //streamWriter.WriteLine("using System.Text;");
            //streamWriter.WriteLine("using System.Windows.Forms;");
            //streamWriter.WriteLine("");
            //streamWriter.WriteLine("namespace CSharpFriendsRocks");
            //streamWriter.WriteLine("{");
            //streamWriter.WriteLine("    public partial class CSharpFriends : Form");
            //streamWriter.WriteLine("    {");
            //streamWriter.WriteLine("        private Label label1;");
            //streamWriter.WriteLine("        private TextBox textBox1;");
            //streamWriter.WriteLine("        private Button button1;");

            //streamWriter.WriteLine("        public void HelloWorld()");
            //streamWriter.WriteLine("        { ");
            //streamWriter.WriteLine("            CSharpFriends cSharpFriends = new CSharpFriends();");
            //streamWriter.WriteLine("            cSharpFriends.Show();");
            //streamWriter.WriteLine("        }");


            //streamWriter.WriteLine(" ");
            //streamWriter.WriteLine("        public Form1()");
            //streamWriter.WriteLine("        {");
            //streamWriter.WriteLine("            InitializeComponent();");
            //streamWriter.WriteLine("        }");
            //streamWriter.WriteLine("");
            //streamWriter.WriteLine("        private void InitializeComponent()");
            //streamWriter.WriteLine("        {");
            //streamWriter.WriteLine("            this.button1 = new System.Windows.Forms.Button();");
            //streamWriter.WriteLine("            this.label1 = new System.Windows.Forms.Label();");
            //streamWriter.WriteLine("            this.textBox1 = new System.Windows.Forms.TextBox();");
            //streamWriter.WriteLine("            this.SuspendLayout();");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            // button1");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            this.button1.Location = new System.Drawing.Point(58, 138);");
            //streamWriter.WriteLine("            this.button1.Name = \"button1\";");
            //streamWriter.WriteLine("            this.button1.Size = new System.Drawing.Size(75, 23);");
            //streamWriter.WriteLine("            this.button1.TabIndex = 0;");
            //streamWriter.WriteLine("            this.button1.Text = \"button1\";");
            //streamWriter.WriteLine("            this.button1.UseVisualStyleBackColor = true;");
            //streamWriter.WriteLine("            this.button1.Click += new System.EventHandler(this.button1_Click);");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            // label1");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            this.label1.AutoSize = true;");
            //streamWriter.WriteLine("            this.label1.Location = new System.Drawing.Point(64, 66);");
            //streamWriter.WriteLine("            this.label1.Name = \"label1\";");
            //streamWriter.WriteLine("            this.label1.Size = new System.Drawing.Size(35, 13);");
            //streamWriter.WriteLine("            this.label1.TabIndex = 1;");
            //streamWriter.WriteLine("            this.label1.Text = \"label1\";");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            // textBox1");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            this.textBox1.Location = new System.Drawing.Point(54, 98);");
            //streamWriter.WriteLine("            this.textBox1.Name = \"textBox1\";");
            //streamWriter.WriteLine("            this.textBox1.Size = new System.Drawing.Size(100, 20);");
            //streamWriter.WriteLine("            this.textBox1.TabIndex = 2;");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            // Form1");
            //streamWriter.WriteLine("            // ");
            //streamWriter.WriteLine("            this.ClientSize = new System.Drawing.Size(284, 264);");
            //streamWriter.WriteLine("            this.Controls.Add(this.textBox1);");
            //streamWriter.WriteLine("            this.Controls.Add(this.label1);");
            //streamWriter.WriteLine("            this.Controls.Add(this.button1);");
            //streamWriter.WriteLine("            this.Name = \"Form1\";");
            //streamWriter.WriteLine("            this.ResumeLayout(false);");
            //streamWriter.WriteLine("            this.PerformLayout();");
            //streamWriter.WriteLine("");
            //streamWriter.WriteLine("        }");
            //streamWriter.WriteLine("");
            //streamWriter.WriteLine("        private void button1_Click(object sender, EventArgs e)");
            //streamWriter.WriteLine("        {");
            //streamWriter.WriteLine("            MessageBox.Show(label1.Text);");
            //streamWriter.WriteLine("        }");
            //streamWriter.WriteLine("    }");
            //streamWriter.WriteLine("}");
            //streamWriter.WriteLine("");
            //streamWriter.WriteLine("");

            streamWriter.Close();

            // Create the C# compiler
            //CSharpCodeProvider csCompiler = new CSharpCodeProvider();            

            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.OutputAssembly = "CSharpFriends.dll";
            compilerParams.ReferencedAssemblies.Add("system.dll");

            //compilerParams.ReferencedAssemblies.Add("System.Core.dll");
            //compilerParams.ReferencedAssemblies.Add("System.Data.dll");
            //compilerParams.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            //compilerParams.ReferencedAssemblies.Add("System.Drawing.dll");
            compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            //compilerParams.ReferencedAssemblies.Add("System.Xml.dll");
            //compilerParams.ReferencedAssemblies.Add("System.Xml.Linq.dll");

            compilerParams.GenerateExecutable = false;
            new CSharpCodeProvider().CompileAssemblyFromFile(compilerParams, "CSharpFriends.cs");

            Assembly asm = Assembly.LoadFrom("CSharpFriends.dll");
            Type t = asm.GetType("CSharpFriendsRocks.CSharpFriends");
            Object obj = t.InvokeMember("HelloWorld",
                BindingFlags.DeclaredOnly |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.CreateInstance, null, null, null);

            t.InvokeMember("HelloWorld",
                BindingFlags.DeclaredOnly |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.InvokeMethod, null, obj, null);

            //try
            //{
            //    Assembly assemblyProjetoPesquisas = Assembly.LoadFrom("CSharpFriends.dll");
            //    Type tela = assemblyProjetoPesquisas.GetType("CSharpFriendsRocks.CSharpFriends");
            //    object item = Activator.CreateInstance(tela) ;
            //    ((System.Windows.Forms.Form)item).Show();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}

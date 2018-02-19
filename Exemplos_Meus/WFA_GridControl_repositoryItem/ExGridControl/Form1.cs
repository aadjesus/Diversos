using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExGridControl.GFO;
using FGlobus.Util;
using ExGridControl.GLB;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Reflection;
using System.Dynamic;


namespace ExGridControl
{
    public partial class Form1 : Form
    {
        //static class ExpandoExtension
        //{
        //    static IDictionary<object, dynamic> props = new Dictionary<object, dynamic>();
        //    public static dynamic Props(this object key)
        //    {
        //        dynamic o;
        //        if (!props.TryGetValue(key, out o))
        //        {
        //            o = new ExpandoObject();
        //            props[key] = o;
        //        }
        //        return o;
        //    }
        //}


        IEnumerable<ItinerarioDTO> listaItinerarioDTO;
        public Form1()
        {
            InitializeComponent();


            //MyClass c = new MyClass();
            //c.Properties["name"] = "value";
            //// alternate syntax
            //c.Properties.Add("another property", "another value");

            //object objeto = Activator.CreateInstance(typeof(ItinerarioDTO));

            //object xxx = typeof(ItinerarioDTO).GetType().GetProperties["XXX"];

            


            //dynamic employee = Activator.CreateInstance(typeof(ItinerarioDTO));
            ////employee.Name = "John Smith";           
            //((IDictionary<String, Object>)employee).Remove("Name");





            listaItinerarioDTO = new GFO.GestaoDeFrotaOnLineWS().GenericoRetornarTodos<ItinerarioDTO>();
            int[] lista = listaItinerarioDTO
                .GroupBy(g => g.CodIntLinha)
                .Select(s => s.Key)
                .ToArray();

            IEnumerable<LinhaDTO> listaLinhaDTO = new GLB.GlobusWS().GenericoConsultaBasica<LinhaDTO>(
                new ExGridControl.GLB.sCondicaoAdicionalCriteria()
                {
                    Operador = GLB.eOperador.Contido,
                    Valor = lista,
                    Propriedade = "CodIntLinha"
                });

            repositoryItemImageComboBox1.Items.AddRange(listaLinhaDTO
                .Select(s => new ImageComboBoxItem()
                {
                    Description = s.NomeAbreviado,
                    Value = s.CodIntLinha
                })
                .ToArray());

            colAchouRegistro.FilterInfo = new ColumnFilterInfo("[AchouRegistro] = True");
            gridColumn6.FilterInfo = new ColumnFilterInfo("[AchouRegistro] = False");

            gridControl1.DataSource = listaItinerarioDTO
                .ToArray();
            gridControl2.DataSource = listaItinerarioDTO
                .ToArray();

            //propertyGridControl1.SelectedObject = gridView1.GetFocusedRow();
            vGridControl1.DataSource = new object[] 
            {
                listaItinerarioDTO
                .FirstOrDefault()
            };
         
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            listaItinerarioDTO
                .Update(u => u.AchouRegistro =
                    
                    
                     !(gridView1.GetFocusedRow() as ItinerarioDTO).AchouRegistro

                    );


            gridView1.RefreshData();
            gridView2.RefreshData();
        }
    }


    public partial class xxxxx
    {
        void MainX()
        {
            string className = "BlogPost";

            var props = new Dictionary<string, Type>() {
                { "Title", typeof(string) },
                { "Text", typeof(string) },
                { "Tags", typeof(string[]) }
            };

            createType(className, props);
        }

        static void createType(string name, IDictionary<string, Type> props)
        {
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "Test.Dynamic.dll", false);
            parameters.GenerateExecutable = false;

            var compileUnit = new CodeCompileUnit();
            var ns = new CodeNamespace("Test.Dynamic");
            compileUnit.Namespaces.Add(ns);
            ns.Imports.Add(new CodeNamespaceImport("System"));

            var classType = new CodeTypeDeclaration(name);
            classType.Attributes = MemberAttributes.Public;
            ns.Types.Add(classType);

            foreach (var prop in props)
            {
                var fieldName = "_" + prop.Key;
                var field = new CodeMemberField(prop.Value, fieldName);
                classType.Members.Add(field);

                var property = new CodeMemberProperty();
                property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                property.Type = new CodeTypeReference(prop.Value);
                property.Name = prop.Key;
                property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
                property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodePropertySetValueReferenceExpression()));
                classType.Members.Add(property);
            }

            var results = csc.CompileAssemblyFromDom(parameters, compileUnit);
            results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            userControl11.btnFecharTela.Click += new System.Windows.RoutedEventHandler(btnFecharTela_Click);
            userControl11.btnMinimizarTela.Click += new System.Windows.RoutedEventHandler(btnMinimizarTela_Click);

            //MessageBox.Show(
            //    String.Concat(
            //        Directory.GetCurrentDirectory()
            //        , "\n",
            //        Environment.CurrentDirectory
            //        , "\n",
            //        Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).CodeBase)
            //        , "\n",
            //        Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location)
            //        , "\n",
            //        System.Reflection.Assembly.GetExecutingAssembly().Location
            //    )
            //    );

            //MyClass[] aaa = new MyClass[] { 
            //    new MyClass(){ codigo=1, descricao="1"} ,
            //    new MyClass(){ codigo=2, descricao="1"} ,
            //    new MyClass(){ codigo=3, descricao="1"} ,
            //    new MyClass(){ codigo=4, descricao="1"} 
            //};

            //new MyClass().GenericoConsultaPorChave(a=> a

            //var xx = aaa.Select1(a => new { a.codigo, a.descricao });
            //new MyClass().Min12(a => new { a.codigo, a.descricao });
            var xx = new MyClass().GenericoConsutaBasicaPorCondicaoDosCampos(a => a.codigo);            
            if (xx == null)
            {

            }



        }



        private void btnMinimizarTela_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnFecharTela_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Directory.GetCurrentDirectory()
            System.Reflection.Assembly assem = System.Reflection.Assembly.GetEntryAssembly();
            System.Reflection.AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            MessageBox.Show(
            String.Concat("Application {0}, Version {1}", assemName.Name, ver.ToString()
            )
            )
            ;
        }
    }

    class MyClass
    {
        public int codigo { get; set; }
        public string descricao { get; set; }
        public int tipo { get; set; }
    }

    public static class ExtensaoDeMetodos
    {

        public static IEnumerable<TResult> GenericoConsutaBasicaPorCondicaoDosCampos<TSource, TResult>(this TSource source, Expression<Func<TSource, TResult>> expressionCampos)
        {
            IEnumerable<string> listaCampos = source.GetType().GetProperties()
                .Select(w => w.Name)
                .Where(w => expressionCampos.Body.ToString().IndexOf(String.Concat(expressionCampos.Parameters.First(), ".", w)) != -1);

            if (listaCampos == null)
            {

            }

            TResult[] results = new TResult[listaCampos.Count()];

            return results;
        }

        // ################################
        #region Legal

        public static TResult SelectOrDefault1<TSource, TResult>(this TSource obj, Func<TSource, TResult> selector) where TSource : class
        {
            return SelectOrDefault2<TSource, TResult>(
                obj, selector, default(TResult));
        }
        public static TResult SelectOrDefault2<TSource, TResult>(this TSource obj, Func<TSource, TResult> selector, TResult @default) where TSource : class
        {
            return obj == null ? @default : selector(obj);
        }

        /// <summary>
        /// Returns the values in a sequence whose resulting value of the specified 
        /// selector is between the lowest and highest values given in the parameters.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of elements in the sequence.
        /// </typeparam>
        /// <param name="source">
        /// The IEnumerable object on which this method works.
        /// </param>
        /// <param name="selector">
        /// The selector predicate used to attain the value.
        /// </param>
        /// <param name="lowest">
        /// The lowest value of the selector that will appear in the new list.
        /// </param>
        /// <param name="highest">
        /// The hightest value of the selector that will appear in the new list.
        /// </param>
        /// <returns>
        /// An IEnumerable sequence of TSource whose selector values fall within the range 
        /// of <paramref name="lowest"/> and <paramref name="highest"/>.
        /// </returns>
        public static IEnumerable<TSource> Between<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult lowest, TResult highest) where TResult : IComparable<TResult>
        {
            return source.OrderBy(selector).
                SkipWhile(s => selector.Invoke(s).CompareTo(lowest) < 0).
                TakeWhile(s => selector.Invoke(s).CompareTo(highest) <= 0);
        }

        #endregion
        // #############################

    }

}

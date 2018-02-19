using FGlobus.Comum;
using FGlobus.Util;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Ini();

            OleDbConnection connection = new OleDbConnection(String.Format("Provider=OraOLEDB.Oracle.1;Data Source={0};Persist Security Info=True;User ID={1};Password={2};Unicode=True", textBox1.Text, textBox2.Text, textBox2.Text));
            DataSet DS = new DataSet();
            connection.Open();


            OleDbDataAdapter DBAdapter = new OleDbDataAdapter(textBox4.Text, connection);
            DBAdapter.Fill(DS);

            Fin(DS.Tables[0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ini();
            DataTable dataTable = new System.Data.DataTable();
            using (OracleConnection conn = new OracleConnection(String.Format("Data Source={0};User Id={1};Password={2}", textBox1.Text, textBox2.Text, textBox2.Text)))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = textBox4.Text;
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();

                dataTable.Load(dr);

            }

            Fin(dataTable);
        }


        #region NHibernate
        private void button1_Click(object sender, EventArgs e)
        {
            AtualizarAppConfig();
            Ini();

            //var x1 = new BLL.CadastroBO().Retornar();

            //Fin(x1.ToArray());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AtualizarAppConfig();
            Ini();
            var x0 = new ClienteDAO1();
            using (var session = x0.Sessao)
            {
                var x12 = "select * from bgm_cliente";
                var x2 = session.CreateSQLQuery(x12);
                if (x2 == null)
                {

                }




            }


            //var x1 = new BLL.CadastroBO().Retornar2();
            //Fin(x1.ToArray());
            AtualizarAppConfig(false);
        }

        private void AtualizarAppConfig(bool ok = true)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["connection.connection_string"].Value = ok
                ? String.Format("Provider=OraOLEDB.Oracle.1;Data Source={0};Persist Security Info=True;User ID={1};Password={2};Unicode=True", textBox1.Text, textBox2.Text, textBox2.Text)
                : "";

            //if (configuration.AppSettings.Settings["Assembly.DTO"] == null)
            //{
            //    configuration.AddAssembly("GLB.DTO");
            //}
            //else if (!configuration.AppSettings.Settings["Assembly.DTO"].ToString().NuloOuVazio())
            //    configuration.AddAssembly(configuration.AppSettings.Settings["Assembly.DTO"]);


            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion

        Stopwatch _stopwatch = new Stopwatch();
        private void Ini()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            _stopwatch.Start();
        }

        private void Fin(object lista)
        {
            _stopwatch.Stop();

            this.Cursor = System.Windows.Forms.Cursors.Default;
            MessageBox.Show(_stopwatch.Elapsed.ToString(), "Tempo");

            gridControl1.DataSource = lista;
        }
    }

    public class ClienteDAO1 : NHibernateHelper
    {

    }
}
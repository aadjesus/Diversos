/*
 * Criado por SharpDevelop.
  * Data: 05/08/2009
 * Hora: 12:55
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace exportaDB
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

	
		void BtnExportarClick(object sender, EventArgs e)
		{
			       string caminho = txtNomeArquivoXLS.Text;

     				if(cboSGBD.SelectedIndex == 0)
			        {
						// criar um arquivo para escrever
	          			using (StreamWriter sw = File.CreateText(caminho))
	          			{
                            //Monta a string de conexão para MS Access com os dados do formulário
                            String conn = @"provider=Microsoft.Jet.OLEDB.4.0;data source = c:\dados\ " + txtBD.Text + ".mdb";
	               			OleDbConnection cn = new OleDbConnection(conn);
	      					OleDbCommand cmd = new OleDbCommand("SELECT * FROM " + txtTabela.Text, cn);
	      					try
	      					{
	           					cn.Open();
	           					OleDbDataReader dr = cmd.ExecuteReader();
                                // percorre o datareader e escreve os dados no arquivo .xls definido
	           					while (dr.Read())
	      						{
	           						sw.WriteLine(dr["ProductName"].ToString() + "\t" + dr["UnitPrice"].ToString());
	      						}
                                //exibe mensagem ao usuario
	           					MessageBox.Show("Arquivo " + caminho + " gerado com sucesso.");
	      					}
	      					catch (Exception excpt)
	      					{
	           					MessageBox.Show(excpt.Message);
	      					}
	          			}
     				}
     				else
     				{
     					// criar um arquivo para escrever
	          			using (StreamWriter sw = File.CreateText(caminho))
	          			{
                            //Server=myServerName\theInstanceName;Database=myDataBase;Trusted_Connection=True;
                            //Monta a string de conexão para SQL Server com os dados do formulário
                            string conn = @"Server = " + txtServidor.Text;
                            conn = conn + "; Database = " + txtBD.Text;
                            conn = conn + "; Trusted_Connection=True";
                            SqlConnection cn = new SqlConnection(conn);
                            //define a instrução SQL para executar contra o banco de dados
                            string sql = " Select * from " + txtTabela.Text;
	      					SqlCommand cmd = new SqlCommand(sql, cn);
	      					try
	      					{
                                //abre a conexão e gera o datareader
	           					cn.Open();
	           					SqlDataReader dr = cmd.ExecuteReader();
	           					// percorre o datareader e escreve os dados no arquivo .xls definido
	           					while (dr.Read())
	      						{
                                    sw.WriteLine(dr["ProductName"].ToString() + "\t" + dr["UnitsInStock"].ToString() + "\t" + dr["UnitPrice"].ToString());
	      						}
                                //exibe mensagem ao usuario
                                MessageBox.Show("Arquivo " + caminho + " gerado com sucesso.");
	      					}
	      					catch (Exception excpt)
	      					{
	           					MessageBox.Show(excpt.Message);
	      					}
	          			}
     				}
     			}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboSGBD.SelectedItem.Equals("SQL Server Express" ))
				pnlSGBD.Visible=true;
			else
				pnlSGBD.Visible=false;
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			cboSGBD.SelectedIndex = 0 ;
		}

       private void btnAbrirXLS_Click_1(object sender, EventArgs e)
        {

            string arqExcel = txtNomeArquivoXLS.Text; ;

            if (arqExcel != "")
            {
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = true;
                    proc.StartInfo.FileName = arqExcel;
                    proc.Start();
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Diretório não encontrado !");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Arquivo não encontrado !");
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("Arquivo não esta disponível. Pode estar em uso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Houve um problema ao abrir o arquivo :" + ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Informe a localização do arquivo Excel.");
            }
        }
	}
}

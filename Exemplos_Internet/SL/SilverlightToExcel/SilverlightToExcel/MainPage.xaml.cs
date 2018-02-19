using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ClienteSilverlight.Vo;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace SilverlightToExcel
{
    /**
     * 
     * @author Robson Fernandes
     * Site -   www.riasoftware.com.br/blog
     */
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			selectListCliente();
		}
		
		/**
         *
         * //Command -> selectListCliente
         * 
         **/
		private void selectListCliente()
		{
            WebClient webClientCliente = new WebClient();
			webClientCliente.DownloadStringCompleted+=new System.Net.DownloadStringCompletedEventHandler(onSelecListCliente);
			webClientCliente.DownloadStringAsync(new Uri("http://www.riasoftware.com.br/aplicacoes/php/crud/cliente/service/ClienteService.php?operacao=select", UriKind.Absolute));
			this.Cursor = Cursors.Wait;
		}
		
		//********************************************************
        ////Responder -> onSelecListCliente
        //********************************************************
		private void onSelecListCliente(object sender, System.Net.DownloadStringCompletedEventArgs e)
		{
  				XDocument doc = XDocument.Parse(e.Result);  
  				var clientes = from c in doc.Descendants("Cliente")
								select new ClienteVo  
								{  
										id = Convert.ToInt32(c.Element("id").Value.ToString()),  
										nome = c.Element("nome").Value,  
										endereco = c.Element("endereco").Value,
										cidade = c.Element("cidade").Value,
                                        bairro = c.Element("bairro").Value,
                                        estado = c.Element("estado").Value,
                                        cep = c.Element("cep").Value,
                                        telefone = c.Element("telefone").Value,
                                        email = c.Element("email").Value
								};  
            	dataGridCliente.ItemsSource = clientes;
                this.Cursor = Cursors.Arrow;
		}

 		/**
         * Exporta dados do DataGrid para o Excel
         * @param event
         * 
         */
		private void btnExportar_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SaveFileDialog  file    =  new SaveFileDialog();
			ExportExcel		export  = new ExportExcel();
			
			file.DefaultExt = "*.xls";
			file.Filter = "Excel Worksheets|*.xls";
			if (file.ShowDialog() == false) return;
			using (StreamWriter sw = new StreamWriter(file.OpenFile()))
			{
               sw.WriteLine(export.DgToHTML<ClienteVo>(dataGridCliente));
			}
		}
	}
}
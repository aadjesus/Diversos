using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClienteSilverlight.Vo
{
	public class ClienteVo
	{
		public int id { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
		
		public ClienteVo()
		{
		}
	}
}
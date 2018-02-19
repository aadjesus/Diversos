using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Globus5.WPF.Comum;
using Globus5.WPF.Sistemas.Sistemaxxxx;

namespace Globus5.WPF.Sistemas.Sistemaxxxx
{
    static class Program
    {
        /// <summary>
        /// Form de cadastro para a classe Program.
        /// <remarks>
        /// Arquivo criado : 8/17/2011 4:05:35 PM. 
        /// Criado por     : alessandro.augusto.
        /// </remarks>
        /// </summary>
        [STAThread]
        static void Main(string[] parametros)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuPrincipal());

            //MenuPrincipal menuPrincipal = new MenuPrincipal();
            //System.IO.FileInfo fileDataHora = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Util.AbrirSistema(new MenuPrincipal(), new MenuPrincipal(), sigla_sistema, fileDataHora.LastWriteTime.ToString("dd/MM/yyyy HH:mm"), parametros);
        }


        //public void SalvarXisto(int empresa, int veiculo, string[] roatreadorInc, string[] roatreadorExc)
        //{
        //    List<DTO> lista = roatreadorInc
        //        .Select(s => new DTO()
        //        {
        //            Empresa = empresa,
        //            Veiculo = veiculo,
        //            RoatreadorExc = s
        //        })
        //        .ToList();

        //    DAO.SalverLista(lista);
        //}
    }
}

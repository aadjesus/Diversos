using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Globus5.WPF.Comum;
using Globus5.WPF.Sistemas.CriaMenuPrincipal;

namespace Globus5.WPF.Sistemas.CriaMenuPrincipal
{
    static class Program
    {
        /// <summary>
        /// Form de cadastro para a classe Program.
        /// <remarks>
        /// Arquivo criado : 8/10/2011 2:07:00 PM. 
        /// Criado por     : alessandro.augusto.
        /// </remarks>
        /// </summary>
        [STAThread]
        static void Main(string[] parametros)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.Globus.Skin).Assembly);
            Application.Run(new MenuPrincipal());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FGlobus.Componentes.WinForms;

namespace Tela_Generica
{
    /// <summary>
    /// Panel flutuante para a classe MasterPanel1.
    /// <remarks>
    /// Arquivo criado : 7/24/2012 9:38:04 AM. 
    /// Criado por     : alessandro.augusto.
    /// </remarks>
    /// </summary>
    public partial class MasterPanel1 : FGlobus.Componentes.WinForms.MasterPanel
    {
        #region Construtor

        /// <summary>
        /// Inicializador da classe.
        /// </summary>
        public MasterPanel1()
        {
            InitializeComponent();
        }

        #endregion

        private void buttonEdit1_Validating(object sender, CancelEventArgs e)
        {            
           // MessageBox.Show(this.SentidoNavegacao.ToString());
        }

        private void buttonEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            //   checkEdit1.Text = e.KeyCode.Equals(Keys.Enter).ToString();
        }
    }
}
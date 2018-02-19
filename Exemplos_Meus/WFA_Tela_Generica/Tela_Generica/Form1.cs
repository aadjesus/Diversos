using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FGlobus.Componentes.WinForms;

namespace Tela_Generica
{
    public partial class Form1 : MasterForm
    {
        public Form1()
        {
            //x strikeout
            //- small comment
            //+ big comment
            //++ very big comment

            //! important comment
            //!+ big important
            //!++ very big important

            //? question
            //?+ big question
            //?++ very big/ 

            // todo: text-text-text
            // HACK: text-text-text

            InitializeComponent();

            //! Environment.NewLine

            MasterPanel1 masterPanel1 = new MasterPanel1();
            masterPanel1.Trazer(panelControl1);           
            //this.Controls.Add(new MasterPanel1());
        }

    }


    interface IInicializacao
    {
        IEnumerable<Empresa> Empresas { get; }
        IEnumerable<Filial> Filiais { get; }
        IEnumerable<Garagem> Garagens { get; }
        Usuario Usuario { get; }
        Sistema Sistema { get; }
        Concedente Concedente { get; }
    }

    public class Inicializacao : IInicializacao
    {
        private IEnumerable<Empresa> _empresas;
        private IEnumerable<Filial> _filiais;
        private IEnumerable<Garagem> _garagens;
        private Usuario _usuario;
        private Sistema _sistema;
        private Concedente _concedente;

        #region IInicializacao Members

        public IEnumerable<Empresa> Empresas
        {
            get { return _empresas; }
        }

        public IEnumerable<Filial> Filiais
        {
            get { return _filiais; }
        }

        public IEnumerable<Garagem> Garagens
        {
            get { return _garagens; }
        }

        public Usuario Usuario
        {
            get { return _usuario; }
        }

        public Sistema Sistema
        {
            get { return _sistema; }
        }

        public Concedente Concedente
        {
            get { return _concedente; }
        }

        #endregion
    }

    public class Empresa
    {
    }

    public class Filial
    {
    }

    public class Garagem
    {
    }

    public class Usuario
    {
        public string Codigo;
        public string Nome;
        public int? Grupo;
        public string Senha;
        //public Funcionario Funcionario;
        public IEnumerable<ItemDeMenu> ItensDeMenu;
        public IEnumerable<Empresa> Empresas;
        public IEnumerable<Filial> Filiais;
        public IEnumerable<Garagem> Garagens;

        public class Funcionario
        {
        }

        public class ItemDeMenu
        {
        }

    }


    public class Sistema
    {
    }

    public class Concedente
    {

    }
}

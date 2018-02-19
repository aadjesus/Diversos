using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Meus
{
    public partial class GerenciadorMsg : Component
    {
        public GerenciadorMsg()
        {
            InitializeComponent();
        }

        public GerenciadorMsg(IContainer container)
        {
            container.Add(this);
            _mensagens1 = new List<PropiedadesMensagens>();
            InitializeComponent();
        }

        private List<PropiedadesMensagens> _mensagens1;

        public List<PropiedadesMensagens> Mensagens1
        {
            get { return _mensagens1; }
            set { _mensagens1 = value; }
        } 

       

    }

    public class PropiedadesMensagens
    {
        private string _mesagem;

        public string Mesagem
        {
            get { return _mesagem; }
            set { _mesagem = value; }
        }
        private string _tipo;

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


    }
}

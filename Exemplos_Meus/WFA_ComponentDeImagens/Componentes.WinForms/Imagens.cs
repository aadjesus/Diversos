using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.Utils;

namespace Componentes.WinForms
{
    public partial class Imagens : ImageCollection
    {
        public Imagens()
        {
            InitializeComponent();
        }

        public Imagens(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

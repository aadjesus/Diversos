using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BgmRodotec.FrameWork
{
    /// <summary>
    /// Classe base para classes de entidades de banco de dados, geralmente utilizadas para efetuar cadastros (Gravação / Alteração / Exclusão). 
    /// Implementa interface IClonable garantindo que todos os objetos da aplicação herdarão os métodos de clone de objetos.
    /// </summary>

    public abstract class BaseCadastro
    {
        private bool _achouRegistro;

        /// <summary>
        /// Property para controlar se o registro existe.
        /// </summary>
        public virtual bool AchouRegistro
        {
            get
            {
                return _achouRegistro;
            }
            set
            {
                _achouRegistro = value;
            }
        }
    }
}

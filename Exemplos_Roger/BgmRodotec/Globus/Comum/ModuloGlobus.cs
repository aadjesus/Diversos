using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BgmRodotec.FrameWork;

namespace BgmRodotec.Globus.Comum
{
    /// <summary>
    /// Entidade que contém todos os módulos do Globus
    /// </summary>

    class ModuloGlobus : BaseCadastro
    {

        #region private fields

        /// <summary>
        /// Sigla do módulo.
        /// </summary>
        private String modulo;

        /// <summary>
        /// Nome do módulo.
        /// </summary>
        private String nomeDoModulo;

        /// <summary>
        /// Descrição do módulo.
        /// </summary>
        private String descricaoDoModulo;

        /// <summary>
        /// Este campo irá identificar a necessidade da atualização dos itens de menu
        /// do módulo, necessários para a autorização dos itens aos usuários.
        /// </summary>
        private String dataHoraMenus;

        #endregion

        #region constructors

        /// <summary>
        /// Construtor default.
        /// </summary>
        public ModuloGlobus()
		{
            modulo = null;
            nomeDoModulo = null;
            descricaoDoModulo = null;
            dataHoraMenus = null;
		}

        #endregion

        #region properties

        /// <summary>
        /// Sigla do módulo.
        /// </summary>
        public String Modulo
        {
            get
            {
                return this.modulo;
            }
            set
            {
                this.modulo = value;
            }
        }

        /// <summary>
        /// Nome do módulo.
        /// </summary>
        public String NomeDoModulo
        {
            get
            {
                return this.nomeDoModulo;
            }
            set
            {
                this.nomeDoModulo = value;
            }
        }

        /// <summary>
        /// Descrição do módulo.
        /// </summary>
        public String DescricaoDoModulo
        {
            get
            {
                return this.descricaoDoModulo;
            }
            set
            {
                this.descricaoDoModulo = value;
            }
        }

        /// <summary>
        /// Este campo irá identificar a necessidade da atualização dos itens de menu
        /// do módulo, necessários para a autorização dos itens aos usuários.
        /// </summary>
        public String DataHoraMenus
        {
            get
            {
                return this.dataHoraMenus;
            }
            set
            {
                this.dataHoraMenus = value;
            }
        }

        #endregion

    }
}

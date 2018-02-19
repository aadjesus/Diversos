using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FGlobus.Componentes.WinForms
{
    public partial class EnumeradoresMascaras
    {
        #region eTipoCalcEditMascarasBGM

        /// <summary>
        /// Enumerador do Tipo do CalcEditMascarasBGM.
        /// </summary>
        public enum eTipoCalcEditMascarasBGM
        {
            /// <summary>
            /// Default.
            /// </summary>
            [XmlEnum("Default")]
            Default,

            /// <summary>
            /// Valor.
            /// </summary>
            [XmlEnum("Valor")]
            Valor,

            /// <summary>
            /// Percentual.
            /// </summary>
            [XmlEnum("Percentual")]
            Percentual
        }

        #endregion

        #region eTipoDateEditMascarasBGM

        /// <summary>
        /// Enumerador do Tipo do eTipoDateEditMascarasBGM.
        /// </summary>
        public enum eTipoDateEditMascarasBGM
        {
            /// <summary>
            /// Default.
            /// </summary>
            [XmlEnum("Default")]
            Default,


            /// <summary>
            /// Dia, mês e ano (2 digítos).
            /// </summary>
            [XmlEnum("Dia, mês e ano (2 digítos)")]
            DiaMesEAnoDoisDigitos,

            /// <summary>
            /// Dia, mês e ano (4 digítos).
            /// </summary>
            [XmlEnum("Dia, mês e ano (4 digítos)")]
            DiaMesEAnoQuatroDigitos,


            /// <summary>
            /// Horas sem segundos.
            /// </summary>
            [XmlEnum("Horas sem segundos")]
            HoraSemSegundos,

            /// <summary>
            /// Horas com segundos.
            /// </summary>
            [XmlEnum("Horas com segundos")]
            HoraComSegundos,


            /// <summary>
            /// Dia, mês e ano (2 digítos) / Horas sem segundos.
            /// </summary>
            [XmlEnum("Dia, mês e ano (2 digítos) / Horas sem segundos")]
            DiaMesAnoDoisDigitosHorasSemSegundos,

            /// <summary>
            /// Dia, mês e ano (2 digítos) / Horas com segundos.
            /// </summary>
            [XmlEnum("Dia, mês e ano (2 digítos) / Horas com segundos")]
            DiaMesAnoDoisDigitosHorasComSegundos,


            /// <summary>
            /// Dia, mês e ano (4 digítos) / Horas sem segundos.
            /// </summary>
            [XmlEnum("Dia, mês e ano (4 digítos) / Horas sem segundos")]
            DiaMesAnoQuatroDigitosHorasSemSegundos,

            /// <summary>
            /// Dia, mês e ano (4 digítos) / Horas com segundos.
            /// </summary>
            [XmlEnum("Dia, mês e ano (4 digítos) / Horas com segundos")]
            DiaMesAnoQuatroDigitosHorasComSegundos,


            /// <summary>
            /// Dia e mês (número).
            /// </summary>
            [XmlEnum("Dia e mês (número)")]
            DiaEMesNumero,

            /// <summary>
            /// Dia e mês (extenso).
            /// </summary>
            [XmlEnum("Dia e mês (extenso)")]
            DiaEMesExtenso,


            /// <summary>
            /// Dia e dia da semana.
            /// </summary>
            [XmlEnum("Dia e dia da semana")]
            DiaEDiaDaSemana
        }

        #endregion
        
        #region eTipoTextEditMascarasBGM

        /// <summary>
        /// Enumerador do Tipo do TextEditMascarasBGM.
        /// </summary>
        public enum eTipoTextEditMascarasBGM
        {
            /// <summary>
            /// Máscara.
            /// </summary>
            [XmlEnum("Default")]
            Default,

            /// <summary>
            /// Placa dos veículos.
            /// </summary>
            [XmlEnum("Placa do veículo")]
            PlacaVeiculo,

            /// <summary>
            /// CPF.
            /// </summary>
            [XmlEnum("CPF")]
            CPF,

            /// <summary>
            /// RG.
            /// </summary>
            [XmlEnum("RG")]
            RG,

            /// <summary>
            /// CNPJ.
            /// </summary>
            [XmlEnum("CNPJ")]
            CNPJ,

            /// <summary>
            /// CEP.
            /// </summary>
            [XmlEnum("CEP")]
            CEP,

            /// <summary>
            /// Telefone com 3 dígitos no DDD.
            /// </summary>
            [XmlEnum("Telefone sem DDD")]
            TelefoneSemDDD,

            /// <summary>
            /// Telefone com 2 dígitos no DDD.
            /// </summary>
            [XmlEnum("Telefone com 2 dígitos no DDD")]
            TelefoneDoisDigitosDDD,

            /// <summary>
            /// Telefone com 3 dígitos no DDD.
            /// </summary>
            [XmlEnum("Telefone com 3 dígitos no DDD")]
            TelefoneTresDigitosDDD,

            /// <summary>
            /// E-mail.
            /// </summary>
            [XmlEnum("E-mail")]
            Email,

            /// <summary>
            /// Senha.
            /// </summary>
            [XmlEnum("Senha")]
            Senha
        }

        #endregion
    }
}

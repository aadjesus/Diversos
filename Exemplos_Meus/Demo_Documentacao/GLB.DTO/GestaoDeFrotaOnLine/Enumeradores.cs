using System;
using System.Collections.Generic;
using System.Text;

namespace Globus5.GestaoDeFrotaOnLine.DTO
{
    public class EnumeradoresGestaoDeFrotaOnLine
    {
        /// <summary>
        /// Enum para identificar os tipos de locais com o código reservado para o sistema.
        /// </summary>
        public enum eTipoDeLocal
        {
            /// <summary>
            /// Código reservado para sistema: 50
            /// </summary>
            PontoDeControle,
            /// <summary>
            /// Código reservado para sistema: 51
            /// </summary>
            SolturaDeFrota
        }

        /// <summary>
        /// Enum para identificar o tipo de mensagem do validador.
        /// </summary>
        public enum eTipoDeMensagem
        {
            /// <summary>
            /// Mensagem recebida do validador.
            /// </summary>
            Recebimento = '1',
            /// <summary>
            /// Mensagem enviada para o validador.
            /// </summary>
            Envio = '2',
            /// <summary>
            /// Mensagem recebida do validador que não valida horario.
            /// </summary>
            NaoValidarHorario = '3',
            /// <summary>
            /// Mensagem recebida do validador que identitificar o inicio da viagem,
            /// <para>
            /// Obs: Tambem não valida horario.
            /// </para>
            /// </summary>
            InicioDeViagem = '4',
            /// <summary>
            /// Mensagem recebida do validador que identitificar o fim da viagem.
            /// <para>
            /// Obs: Tambem não valida horario.
            /// </para>
            /// </summary>
            FimDeViagem = '5'
        }

        /// <summary>
        /// Enum para identificar o tipo de oreradora.
        /// </summary>
        public enum eTipoOperadora
        { 
            /// <summary>
            /// Prodata
            /// </summary>
            Prodata = '1',
            /// <summary>
            /// BGMRodotec
            /// </summary>
            Wap = 2
        }
    }
}

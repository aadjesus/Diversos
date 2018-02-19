using System;
using System.Collections.Generic;
using System.Text;

namespace Globus5.GestaoDeFrotaOnLine.DTO
{
    [Serializable]
    public class EstruturasGestaoDeFrotaOnLine
    {
        public struct sSolturaDeFrota
        {
            public int CodigoEmpresa;
            public int CodigoFl;
            public int CodIntLinha;
            public string CodigoLinha;
            public string DescricaoLinha;
            public int CodIntVeiculo;
            public string PrefixoVeiculo;
            public string PlacaVeiculo;
            public string CodigoServico;
            public int CodigoHorario;
            public DateTime DataSoltura;
            public string HoraPrev;
            public string HoraReal;
            public string HoraAtra;
            public string HoraAdia;
            public int CodLocalidade;
            public string DescLocalidade;
            public string Tipo;
            public int QtdeAtrasado;
            public int QtdeAdiantado;
            public int QtdePrevisto;
        }

        public struct sHistoricoConsolidado
        {
            public int CodIntVeiculo;
            public int CodIntLinha;
            public string Sentido;
            public DateTime DataEscala;
            public string Servico;
            public int CodHoraria;
            public int Sequencia;
            public DateTime Programado;
            public DateTime Realizado;
            public DateTime DifRealProg;
            public int Peso;
            public int Prioridade;
            public int Velocidade;
            public int Passageiros;
            public int Localidade;
            public int Ocorrencia;
            public string DescOcorrencia;
            public int RealizadoPonto;
            public int PassagAcumulado;
            public int PassagAnterior;
            public int Turno;
        }

        /// <summary>
        /// Estrutura para o envio e recebimento de mensagens do validador.
        /// </summary>
        public struct sMensagens
        {
            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
            /// <remarks>
            /// <list type="bullet">
            /// <item>N: <description>Nova mensagem.</description></item>
            /// <item>R: <description>Resposta de uma mensagem.</description></item>
            /// </list>
            /// </remarks>
            /// </summary>
            public string Tipo;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
            /// </summary>
            public int Grupo;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
            /// </summary>
            public int Codigo;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
            /// </summary>
            public int IDMensagem;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
            /// </summary>
            public int IDMensagemRetorno; 

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
            /// </summary>
            public int IDOperador;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
            /// </summary>
            public string Prefixo;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="int"/>.
            /// </summary>
            public int IDEmpresa;

            /// <summary>
            /// (Get/Set) Retorna ou informa o valor da propriedade do tipo <see cref="string"/>.
            /// </summary>
            public string Linha;
        }

        /// <summary>
        /// Estrutura para retornar q a quantidade de mensagens enviadas do validador.
        /// </summary>
        public struct sQtdeMensagens
        {
            /// <summary>
            /// Codigo interno da linha.
            /// </summary>
            public int CodIntLinha;
            /// <summary>
            /// Codigo interno do veiculo.
            /// </summary>
            public int CodigoVeic;
            /// <summary>
            /// Quantidade de mensagens.
            /// </summary>
            public int QtdeMsg;
            /// <summary>
            /// Cor da mensagem.
            /// </summary>
            public int CorMensagem;
        }

        /// <summary>
        /// Estrutura para retornar os veiculos que vao passar no local informado
        /// </summary>
        public struct sRetornoSMS
        {
            /// <summary>
            /// Tempo previsto para chegar no local
            /// </summary>
            public string TempoPrevisto;
            /// <summary>
            /// Código da linha
            /// </summary>
            public string CodigoLinha;
            /// <summary>
            /// Nome abreviado da linha
            /// </summary>
            public string NomeAbrevLinha;
            /// <summary>
            /// Origem da linha
            /// </summary>
            public string OrigemLinha;
            /// <summary>
            /// Destino da linha
            /// </summary>
            public string DestinoLinha;
            /// <summary>
            /// Sentido
            /// </summary>
            public string Sentido;
        }


        /// <summary>
        /// Estrutura para retornar o tempo do veiculo no trecho
        /// </summary>
        public struct sTempoNoTrecho
        {
            /// <summary>
            /// Data hora execução.
            /// </summary>
            public DateTime Realizado;
            /// <summary>
            /// Codigo interno da linha.
            /// </summary>
            public int CodIntLinha;
            /// <summary>
            /// Codigo da linha.
            /// </summary>
            public string CodigoLinha;
            /// <summary>
            /// Sentido
            /// </summary>
            public string Sentido;
            /// <summary>
            /// Codigo interno do veiculo.
            /// </summary>
            public int CodIntVeiculo;
            /// <summary>
            /// Qtde passageiros entre o trecho.
            /// </summary>
            public int Passageiros;
            /// <summary>
            /// Filial da linha.
            /// </summary>
            public int Filial;
            /// <summary>
            /// Hora de ponto de controle origem.
            /// </summary>
            public DateTime PCOrigem;
            /// <summary>
            /// Hora de ponto de controle destino.
            /// </summary>
            public DateTime PCDestino;
            /// <summary>
            /// Tempo entre os trechos.
            /// </summary>
            public DateTime Tempo;
            /// <summary>
            /// Código do serviço.
            /// </summary>
            public string CodigoServico;
            /// <summary>
            /// Código do horario.
            /// </summary>
            public int CodigoHorario;
            /// <summary>
            /// Faixa horario
            /// </summary>
            public int FaixaHoraria;
        }
    }
}
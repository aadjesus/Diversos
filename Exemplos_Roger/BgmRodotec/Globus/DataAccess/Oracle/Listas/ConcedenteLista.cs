using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Diagnostics;

namespace BgmRodotec.Globus.DataAccess.Oracle.Listas {
    using Comum;

    #region Comum

    public class ConcedenteLista : IConcedente
    {
        /// <summary>
        /// Recupera uma lista de concedentes.
        /// </summary>
        /// <returns>Retorna uma coleção de objetos
        /// do tipo Concedente ou null se ocorreu algum erro.</returns>
        public Concedente[] ListadeConcedentes()
        { 
            //retorna null, se a operação nao pode ser realizada.
            List<Concedente> itens = null;

            //TODO: realizar o tratamento de exception adequado.
            try
            {
                using (OracleConnection connection = new OracleConnection())
                {
                    using (OracleCommand command = new OracleCommand())
                    {

                        command.CommandText = "SELECT " +
                                              "  A.CODINTCONCEDENTE, " +
                                              "  A.CHECKSUMCONCEDENTE, " +
                                              "  A.NOMECONCEDENTE, " +
                                              "  A.CODIGOCLIENTE, " +
                                              "  A.CHAVECLIENTE " +
                                              "FROM " +
                                              "  CTR_CONCEDENTE A";

                        command.CommandType = CommandType.Text;
                        command.Connection = connection;

                        IDataReader reader = null;

                        connection.ConnectionString = "Data Source=ora10g64;Persist Security Info=True;User ID=glb5_comporte;Password=glbcinco;Unicode=True";
                        connection.Open();

                        reader = command.ExecuteReader();

                        itens = new List<Concedente>();

                        while (reader.Read())
                        {
                            Concedente concedente = new Concedente();
                            //concedente.CodIntConcedente = reader["CODINTCONCEDENTE"];
                            concedente.ChecksumConcedente = reader["CHECKSUMCONCEDENTE"].ToString();
                            concedente.NomeConcedente = reader["NOMECONCEDENTE"].ToString();
                            //concedente.CodigoCliente = reader["CODIGOCLIENTE"];
                            concedente.ChaveCliente = reader["CHAVECLIENTE"].ToString();
                            itens.Add(concedente);
                            concedente = null;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine("Exemplo: " + exception.Message);
#endif
            }
            finally
            {
                //TODO: Incluir algum código para liberação de recursos, se necessário.
            }
            return itens.ToArray<Concedente>();
        }
    }

    #endregion
}

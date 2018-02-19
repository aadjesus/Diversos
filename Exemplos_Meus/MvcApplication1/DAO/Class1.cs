using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO
{
    //interface IGerenciaCRUD<TEntity> where TEntity : class
    //{
    //    List<TEntity> Get(string sql);
    //    TEntity GetById(int id);
    //    void Cadastra(TEntity obj);
    //    void Atualiza(TEntity obj);
    //    void Exclui(int id);
    //}

    //public class Produto
    //{
    //    public int ProdutoID { get; set; }
    //    public string Nome { get; set; }
    //    public decimal Preco { get; set; }
    //    public int Estoque { get; set; }
    //}


    //public class DALProduto : IGerenciaCRUD<Produto>
    //{
    //    // string de conexao
    //    string conexao = WebConfigurationManager.ConnectionStrings["BancoContexto"].ConnectionString;

    //    public List<Produto> Get(string sql)
    //    {
    //        string sql = "Select * FROM Produtoes ORDER BY Nome";
    //        using (var conn = new SqlConnection(conexao))
    //        {
    //            var cmd = new SqlCommand(sql, conn);
    //            List<Produto> dados = new List<Produto>();
    //            Produto p = null;
    //            try
    //            {
    //                conn.Open();
    //                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
    //                {
    //                    while (reader.Read())
    //                    {
    //                        p = new Produto();
    //                        p.ProdutoID = (int)reader["ProdutoID"];
    //                        p.Nome = reader["Nome"].ToString();
    //                        p.Preco = (decimal)reader["Preco"];
    //                        p.Estoque = (int)reader["Estoque"];
    //                        dados.Add(p);
    //                    }
    //                }
    //            }
    //            finally
    //            {
    //                conn.Close();
    //            }
    //            return dados;
    //        }
    //    }
    //}
}

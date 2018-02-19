using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mycon"].ToString());
            //SqlCommand cmd = new SqlCommand("select * from Product", con);
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //DataSet myrec = new DataSet();
            //da.Fill(myrec);
            //return myrec;

            //OleDbConnection conexao = new OleDbConnection("");
            //OleDbCommand comando = new OleDbCommand(sssss);
            //OleDbDataReader oleDbDataReader = comando.ExecuteReader();

            OleDbConnection connection = new OleDbConnection("Provider=OraOLEDB.Oracle.1;Data Source=ORA11G64;Persist Security Info=True;User ID=SIITESTE;Password=SIITESTE;Unicode=True");
            DataSet DS = new DataSet();
            connection.Open();

            sssss = @"select a.codcliente from SII_CLIENTE a where a.ativocliente = 'A'";

            OleDbDataAdapter DBAdapter = new OleDbDataAdapter(sssss,connection);
            DBAdapter.Fill(DS);

            return View(DS);



            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable("Marks");

            //dt.Clear();
            //dt.Columns.Add("Name");
            //dt.Columns.Add("Marks");
            //DataRow dr = dt.NewRow();
            //dr["Name"] = "rami";
            //dr["Marks"] = "500";
            //dt.Rows.Add(dr);


            //ds.Tables.Add(dt);

            //return View(ds);
        }


        string sssss = @"SELECT
                          CL.NOMEFANTASIACLIENTE CLIENTE,
                          GR.DESGRUPOEMP GRUPO,
                          NVL(GR.CLASSIFICACAOGRUPO,'-') CLASSIFICACAO,
                          1 QTDE,
                          A.CODIGOSAS,
                          A.CODMODULO,
                          TO_CHAR(B.DTEFETIVAENTREGA,'YYYY/MM') ENTREGA,
                          C.ABREVIADOMODULO_DES SETOR,
                          A.VALORFINALSAS VALOR,
                          DECODE(A.TIPOSOLICITACAOSAS,'E' ,'ERRO',
                                                      'EA','ERRO',
                                                      DECODE(NVL(A.VALORFINALSAS,0),0,DECODE(A.CODCLIENTE,169,'ALTERACAO BGMRODOTEC',
                                                                                                          176,'ALTERACAO BGMRODOTEC',
                                                                                                           26,'ALTERACAO BGMRODOTEC',
                                                                                                           DECODE(A.TIPOSASSIM,0,'PSE SEM CUSTO','ALTERACAO CLIENTE'))
                                                                                   ,'PSE COBRADO')) TIPO_SOLICITACAO,
                          FC_HORAS_DESENV_TESTE_SAS(A.CODIGOSAS,A.TIPOSASSIM) HORAS,
                          NVL(DECODE(NVL(A.HRS_PREVISTAS_DESENV,0),0,A.TEMPOEXECUCAOSAS,A.HRS_PREVISTAS_DESENV),1)/10000 HORAS_PREVISTAS,
                          FC_HORAS_DESENVOLVIMENTO_SAS(A.CODIGOSAS,A.TIPOSASSIM) HORAS_DEV,
                          FC_HORAS_RETRABALHO_SAS(A.CODIGOSAS,A.TIPOSASSIM) HORAS_RETRABALHO,
                          FC_HORAS_TESTE_SAS(A.CODIGOSAS,A.TIPOSASSIM) HORAS_TESTE,
                          DECODE(A.TIPOSOLICITACAOSAS,'E' ,0,
                                                      'EA',0,
                                                      FC_HORAS_DESENVOLVIMENTO_SAS(A.CODIGOSAS,A.TIPOSASSIM)-FC_HORAS_RETRABALHO_SAS(A.CODIGOSAS,A.TIPOSASSIM))HORAS_PRODUTIVAS
                        FROM
                          SII_SAS A ,
                          SII_ENTREGA B,
                          SII_MODULO C,
                          SII_CLIENTE CL,
                          SII_GRUPOEMPRESA GR
                        WHERE
                              A.CODIGOSAS  = B.CODIGOSAS
                          AND A.TIPOSASSIM = B.TIPOSASSIM
                          AND A.CODMODULO <> 'AMB'
                          AND A.CODATIVIDADE <> '32'
                          AND A.TIPOSOLICITACAOSAS IN ('A','EA','E','CV')
                          AND A.STATUSSAS = '12'
                          AND A.CODMODULO  = C.CODMODULO
                          AND A.CODSISTEMA = C.CODSISTEMA
                          AND A.CODCLIENTE = CL.CODCLIENTE
                          AND CL.CODGRUPOEMP = GR.CODGRUPOEMP
                          AND TRUNC(B.DTEFETIVAENTREGA) > TO_DATE('01/01/2014','DD/MM/YYYY')
                        ";

    }
}

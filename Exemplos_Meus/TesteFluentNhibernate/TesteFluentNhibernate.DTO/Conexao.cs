using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

namespace TesteFluentNhibernate.DTO
{
    public class Conexao
    {
        // http://www.leandroprado.com.br/2012/01/comecando-com-fluent-nhibernate/
        // http://nhforge.org/blogs/nhibernate/archive/2009/12/17/queryover-in-nh-3-0.aspx
        
        public static void CriarTabelasBanco()
        {
            FluentConfiguration config = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
                    .ConnectionString(con => con.Database("f_nh_teste").Server("localhost").Username("root").Password("admin")).ShowSql())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .Mappings(mps => mps.FluentMappings.AddFromAssemblyOf<Usuario>());

            config.BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionFactory()
        {
            FluentConfiguration config = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.OracleClientConfiguration.Oracle10
                    .ConnectionString("Data Source=ORA10G;Persist Security Info=True;User ID=GRANELEIRO120801;Password=GRANELEIRO120801;Unicode=True"))
                .Mappings(mps => mps.FluentMappings.AddFromAssemblyOf<Usuario>());

            return config.BuildSessionFactory();
        }

        void Xto_Salva()
        {
            var sessao = CreateSessionFactory().OpenSession();
            var transacao = sessao.BeginTransaction();

            sessao.SaveOrUpdate(new Usuario() { Nome = "Joauquim Batista", Email = "jb@localhost.com.br", Status = Status.Inativo });

            transacao.Commit();
        }

        void Xto_Retorna()
        {
            var sessao = CreateSessionFactory().OpenSession();
            var transacao = sessao.BeginTransaction();

            //Pais usu = sessao.Get<Pais>("AF");

            //ICriteria crit = sessao.CreateCriteria<Usuario>()
            //    .Add(Expression.Eq("Nome", "Ericc Antunesssss"));
            //Usuario[] usu = crit.List<Usuario>().ToArray();

            var usu = sessao.QueryOver<Pais>()               
                .Where(w => w.SiglaPais == "AF")                
                .List();

            if (usu != null)
            {

            }

            transacao.Commit();
        }

    }
}

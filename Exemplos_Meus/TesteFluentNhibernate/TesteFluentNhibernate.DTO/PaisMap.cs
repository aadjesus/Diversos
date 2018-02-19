using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace TesteFluentNhibernate.DTO
{
    class PaisMap : ClassMap<Pais>
    {
        public PaisMap()
        {
            Table("CGS_PAIS");

            Id(i => i.SiglaPais, "SIGLA_PAIS");

            Map(m => m.nome_pais,"NOME_PAIS");
        }
    }
}

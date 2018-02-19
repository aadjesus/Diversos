using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace TesteFluentNhibernate.DTO
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(i => i.Id, "id").GeneratedBy.Identity();

            Map(m => m.Nome, "nome").Not.Nullable().Length(300);
            Map(m => m.Email, "email").Length(200);
            Map(m => m.StatusAux, "status").Not.Nullable().Length(1);            
        }
    }
}

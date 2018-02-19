using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerDalNameSpace;

namespace MiddleLayer
{
    public class Customer : INSCustomer.ICustomer
    {
        public int CustomerCode { get; set; }
        public string CustomerName { get; set; }

        public void Add() 
        {
            CustomerDal oDal = new CustomerDal();
            oDal.Add(this);        }


        public Projeto.DTO.ClassDTO RetornarDTO()
        {
            return new Projeto.DTO.ClassDTO()
            {
                Codigo = 1,
                Descricao = "aaaa"
            };
        }
    }
}

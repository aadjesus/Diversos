using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projeto.DTO;

namespace INSCustomer
{
    public interface ICustomer
    {
        int CustomerCode { get; set; }
        string CustomerName { get; set; }
        ClassDTO RetornarDTO();
    }
}

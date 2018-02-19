using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Validando_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidacaoXML validadorXML = new ValidacaoXML();
            Console.WriteLine("Validando o  arquivo CatalogoProdutos.xml pelo esquema Produtos.xsd.");

            //bool ok = validadorXML.ValidarXml(@"C:\dados\CatalogoProdutos.xml",@"c:\dados\Produtos.xsd");
            bool ok = validadorXML.ValidarDocumentoXML(@"C:\dados\CatalogoProdutos.xml", @"c:\dados\Produtos.xsd");
            
            if (!ok)
               Console.WriteLine("A validação falhou.");
            else
               Console.WriteLine("A validação foi realizada com sucesso.");

               Console.ReadLine();
        }

       


    }
}

using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace Validando_XML
{
    public class ValidacaoXML
    {
        private bool falhou;
        public bool Falhou
        {
            get { return falhou; }
        }

        public bool ValidarXml(string xmlFilename, string schemaFilename)
        {
            // Define o tipo de validação
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            // Carrega o arquivo de esquema
            XmlSchemaSet schemas = new XmlSchemaSet();
            settings.Schemas = schemas;
            // Quando carregar o escham, especicar o namespace que ele valida
            // e a localização do arquivo 
            schemas.Add(null, schemaFilename);
            // Especifica o tratamento de evento para os erros de validacao
            settings.ValidationEventHandler += ValidationEventHandler;
            // cria um leitor para validação
            XmlReader validator = XmlReader.Create(xmlFilename, settings);
            falhou = false;
            try
            {
                // Faz a leitura de todos os dados XML
                while (validator.Read()) 
                {}
            }
            catch (XmlException err)
            {
                // Um erro ocorre se o documento XML inclui caracteres ilegais
                // ou tags que não estão aninhadas corretamente
                Console.WriteLine("Ocorreu um erro critico durante a validacao XML.");
                Console.WriteLine(err.Message);
                falhou = true;
            }
            finally
            {
                validator.Close();
            }
            return !falhou;
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            falhou = true;
            // Exibe o erro da validação
            Console.WriteLine("Erros da validação : " + args.Message);
            Console.WriteLine();
        }

        public bool ValidarDocumentoXML(string xmlPath, string xsdPath)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsdPath);
                settings.ValidationType = ValidationType.Schema;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlPath);
                XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocument.InnerXml), settings);
                while (xmlReader.Read()) { }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}

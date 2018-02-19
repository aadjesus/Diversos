using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;

namespace Servicos
{
    /// <summary>
    /// Summary description for Loga
    /// </summary>
    [WebService(Namespace = "http://schemas.microsoft.com/TeamFoundation/2005/06/Services/Notification/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Loga : System.Web.Services.WebService
    {

        [SoapDocumentMethod(Action = "http://schemas.microsoft.com/TeamFoundation/2005/06/Services/Notification/03/Notify", RequestNamespace = "http://schemas.microsoft.com/TeamFoundation/2005/06/Services/Notification/03")]
        [WebMethod(MessageName = "Notify")]
        public void Notify(string eventXml)
        {
            string ArquivoLog = @"c:\temp\log.log";
            using (StreamWriter sw = new StreamWriter(ArquivoLog, true))
            {
                sw.WriteLine(eventXml);
                sw.Close();
            }            
        }
    }
}

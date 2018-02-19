using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using DevExpress.Xpf.Printing.Service;

namespace SilverlightApplicationImpressao.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReportService" in code, svc and config file together.
    [SilverlightFaultBehavior]
    public class ReportService : DevExpress.Xpf.Printing.Service.ExportService
    {
    }
}

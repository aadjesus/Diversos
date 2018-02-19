using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;

namespace GridSample.Web
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DBService
    {
        [OperationContract]
        public List<SelectClientResult> GetClientList()
        {
            using (LinqToDBDataContext db = new LinqToDBDataContext())
            {
                return db.SelectClient().ToList();
            }
        }
    }
}

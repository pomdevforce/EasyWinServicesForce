using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.Salesforce
{
    using global::Salesforce.Force;



    public interface ISalesforceManager
    {
        Task<IForceClient> LoginAndGetForceApi();
    }
}

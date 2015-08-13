using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.Configuration
{
    public interface ISalesforceApiConfig
    {

        Boolean IsSandbox { get; }
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string Username { get; }
        string Password { get; }
        string SecurityToken { get; }
    }
}

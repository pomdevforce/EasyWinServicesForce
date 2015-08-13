using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.Configuration
{
    public class SalesforceApiConfig : ISalesforceApiConfig
    {
        public Boolean IsSandbox
        {
            get
            {
                return ConfigurationManager.AppSettings["IsSandboxUser"].Equals(
                    "true",
                    StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public string ConsumerKey
        {
            get { return ConfigurationManager.AppSettings["ConsumerKey"]; }
        }

        public string ConsumerSecret
        {
            get { return ConfigurationManager.AppSettings["ConsumerSecret"]; }
        }

        public string Username
        {
            get { return ConfigurationManager.AppSettings["Username"]; }
        }

        public string Password
        {
            get { return ConfigurationManager.AppSettings["Password"] + SecurityToken; }
        }

        public string SecurityToken
        {
            get { return ConfigurationManager.AppSettings["SecurityToken"]; }
        }
    }
}

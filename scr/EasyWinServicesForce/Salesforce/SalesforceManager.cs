using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.Salesforce
{
    using EasyWinServicesForce.Configuration;

    using global::Salesforce.Common;
    using global::Salesforce.Force;
    using System.Configuration;

    public class SalesforceManager : ISalesforceManager
    {
        #region Property
        private readonly AuthenticationClient auth;
        private readonly ISalesforceApiConfig salesforceApiConfig;
        #endregion
        public SalesforceManager(ISalesforceApiConfig salesforceApiConfig)
        {
            this.salesforceApiConfig = salesforceApiConfig;
            this.auth = new AuthenticationClient();
        }
        public async Task<IForceClient> LoginAndGetForceApi()
        {
            IForceClient forceclient = null;
            try
            {
                await Login();
                forceclient = new ForceClient(this.auth.InstanceUrl, this.auth.AccessToken, this.auth.ApiVersion);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not login and get force api", ex);
            }
            return forceclient;
        }
        public async Task Login()
        {
            try
            {
                var url = this.SalesforceLoginUrl(this.salesforceApiConfig.IsSandbox);
                await this.auth.UsernamePasswordAsync(this.salesforceApiConfig.ConsumerKey
                    , this.salesforceApiConfig.ConsumerSecret
                    , this.salesforceApiConfig.Username
                    , this.salesforceApiConfig.Password
                    , url);
            }
            catch (Exception ex)
            {
                throw new Exception("Login to salesforce fail", ex);
            }

        }


        public string SalesforceLoginUrl(Boolean isSandbox)
        {
            var url = isSandbox ? "https://test.salesforce.com/services/oauth2/token" : "https://login.salesforce.com/services/oauth2/token";
            return url;
        }


    }
}

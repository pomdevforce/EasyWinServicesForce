namespace EasyWinServicesForce.LogicLayer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using EasyWinServicesForce.Salesforce;
    using EasyWinServicesForce.SObject;
    using Common.Logging;

    using CsvHelper;

    using global::Salesforce.Force;
    public class BusinessProcess : IBusinessProcess
    {
        private readonly ILog log;
        public ISalesforceManager SalesforceManager { get; set; }
        public IForceClient ForceClient { get; set; }
        public BusinessProcess(ISalesforceManager salesforceManager)
        {
            this.log = LogManager.GetLogger(this.GetType());
            SalesforceManager = salesforceManager;
        }
        /// <summary>
        /// Runs the business logic here.
        /// </summary>
        public async Task Run()
        {

            this.log.Info("BusinessProcess ID : " + SalesforceManager.GetHashCode());
            this.log.Info("This has been run");
            ForceClient = await this.SalesforceManager.LoginAndGetForceApi();
            var lstUsers = await QueryUserFromSalesforce(ForceClient);
            string csv = "";
            //using (var memoryStream = new MemoryStream())
            //using (var streamWriter = new StreamWriter(memoryStream))
            //using (var streamReader = new StreamReader(memoryStream))
            //using (var writer = new CsvWriter(streamWriter))
            //{

            //    writer.Configuration.BufferSize = 2048;
            //    writer.Configuration.QuoteAllFields = true;
            //    writer.WriteHeader<UserPoCo>();
            //    foreach (var record in lstUsers)
            //    {
            //        writer.WriteRecord(record);
            //    }

            //    memoryStream.Position = 0;
            //    memoryStream.Flush();
            //    csv = streamReader.ReadToEnd();


            //}



        }
        public async Task<List<UserPoCo>> QueryUserFromSalesforce(IForceClient client)
        {
            var lstUsers = new List<UserPoCo>();
            var nextUrl = await GetUser(client, lstUsers);
            await GetNextUser(client, nextUrl, lstUsers);
            return lstUsers;
        }
        public async Task<String> GetUser(IForceClient client, List<UserPoCo> lstUsers)
        {
            string restQuery = "select Id , Employee_ID__c , Email , Alias , Zone__c , Zone_Code__c , Region__c , Region_Code__c ,  IsActive from 	User where 	IsActive = true and	Employee_ID__c != null ";
            var results = await client.QueryAsync<UserPoCo>(restQuery);
            lstUsers.AddRange(results.Records);
            return results.NextRecordsUrl;
        }
        public async Task GetNextUser(IForceClient client, string nextRecordsUrl, List<UserPoCo> lstUsers)
        {

            if (!string.IsNullOrEmpty(nextRecordsUrl))
            {
                while (true)
                {
                    var continuationResults = await client.QueryContinuationAsync<UserPoCo>(nextRecordsUrl);
                    lstUsers.AddRange(continuationResults.Records);
                    if (string.IsNullOrEmpty(continuationResults.NextRecordsUrl)) break;
                    nextRecordsUrl = continuationResults.NextRecordsUrl;
                }
            }
        }
        /// <summary>
        /// Disposes resources not being used any more.
        /// </summary>
        public void Dispose()
        {
            if (ForceClient != null)
                ForceClient.Dispose();
        }
    }
}

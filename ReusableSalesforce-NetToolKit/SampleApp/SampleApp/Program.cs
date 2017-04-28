using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Salesforce.Common;
using Salesforce.Force;
using System.Threading.Tasks;
using System.Net;

namespace SampleApp
{
    class Program
    {
        private static readonly string SecurityToken = ConfigurationManager.AppSettings["SecurityToken"];
        private static readonly string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private static readonly string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static readonly string Username = ConfigurationManager.AppSettings["Username"];
        private static readonly string Password = ConfigurationManager.AppSettings["Password"] + SecurityToken;
        private static readonly string IsSandboxUser = ConfigurationManager.AppSettings["IsSandboxUser"];

        static void Main()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                var task = RunSample();
                task.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                var innerException = e.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine(innerException.Message);
                    Console.WriteLine(innerException.StackTrace);

                    innerException = innerException.InnerException;
                }

                Console.ReadLine(); // Prevent the app from closing so the error can be seen
            }
        }

        private static async Task RunSample()
        {
            var auth = new AuthenticationClient();

            // Authenticate with Salesforce
            Console.WriteLine("Authenticating with Salesforce");
            var url = IsSandboxUser.Equals("true", StringComparison.CurrentCultureIgnoreCase)
                ? "https://test.salesforce.com/services/oauth2/token"
                : "https://login.salesforce.com/services/oauth2/token";

            await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);
            Console.WriteLine("Connected to Salesforce");

            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);

            // retrieve all accounts
            Console.WriteLine("Get Accounts");

            const string qry = "SELECT ID, Name FROM Account";
            var accts = new List<Account>();
            var results = await client.QueryAsync<Account>(qry);
            var totalSize = results.TotalSize;

            Console.WriteLine("Queried " + totalSize + " records.");

            accts.AddRange(results.Records);
            var nextRecordsUrl = results.NextRecordsUrl;

            if (!string.IsNullOrEmpty(nextRecordsUrl))
            {
                Console.WriteLine("Found nextRecordsUrl.");

                while (true)
                {
                    var continuationResults = await client.QueryContinuationAsync<Account>(nextRecordsUrl);
                    totalSize = continuationResults.TotalSize;
                    Console.WriteLine("Queried an additional " + totalSize + " records.");

                    accts.AddRange(continuationResults.Records);
                    if (string.IsNullOrEmpty(continuationResults.NextRecordsUrl)) break;

                    //pass nextRecordsUrl back to client.QueryAsync to request next set of records
                    nextRecordsUrl = continuationResults.NextRecordsUrl;
                }
            }
            Console.WriteLine("Retrieved accounts = " + accts.Count() + ", expected size = " + totalSize);

            foreach (dynamic acct in accts)
            {
                Console.WriteLine("Account - " + acct.Name);
            }

            Console.Read();

        }

        private class Account
        {
            public const String SObjectTypeName = "Account";

            public String Id { get; set; }
            public String Name { get; set; }
        }

    }
}

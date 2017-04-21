using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using System.Text;
using System.IO;

namespace Salesforce_oAuth_UsernamePasswordFlow_Demo.Common
{
    public class SalesforceUtility
    {
        public string InstanceUrl { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Id { get; set; }
        public string ApiVersion { get; set; }

        private const string UserAgent = "salesforce-authentication-demo";
        private const string TokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";
        private readonly HttpClient _httpClient;

        public string ConnectToSalesforce()
        {
            string SecurityToken = ConfigurationManager.AppSettings["SecurityToken"];
            string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
            string Username = ConfigurationManager.AppSettings["Username"];
            string Password = ConfigurationManager.AppSettings["Password"] + SecurityToken;
            string TokenRequestEndpoint = ConfigurationManager.AppSettings["TokenRequestEndpoint"];

            var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", ConsumerKey),
                    new KeyValuePair<string, string>("client_secret", ConsumerSecret),
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                });

/*
            StringBuilder postData = new StringBuilder();
            postData.Append("grant_type=password" +  "&");
            postData.Append("client_id=" + ConsumerKey + "&");
            postData.Append("client_secret=" + ConsumerSecret + "&");
            postData.Append("username=" + HttpUtility.UrlEncode(Username) + "&");
            postData.Append("password=" + HttpUtility.UrlEncode(Password));
            */

            var request = (HttpWebRequest)WebRequest.Create(TokenRequestEndpoint);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Referer = "http://localhost:55728/"; // optional
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            var data = Encoding.ASCII.GetBytes(postData.ToString());

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, postData.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            return responseString;
        }

        //private async Task UsernamePasswordAsync(string clientId, string clientSecret, string username, string password, string tokenRequestEndpointUrl)
        //{
        //    if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("clientId");
        //    if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentNullException("clientSecret");
        //    if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
        //    if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
        //    if (string.IsNullOrEmpty(tokenRequestEndpointUrl)) throw new ArgumentNullException("tokenRequestEndpointUrl");
        //    if (!Uri.IsWellFormedUriString(tokenRequestEndpointUrl, UriKind.Absolute)) throw new FormatException("tokenRequestEndpointUrl");

        //    var content = new FormUrlEncodedContent(new[]
        //        {
        //            new KeyValuePair<string, string>("grant_type", "password"),
        //            new KeyValuePair<string, string>("client_id", clientId),
        //            new KeyValuePair<string, string>("client_secret", clientSecret),
        //            new KeyValuePair<string, string>("username", username),
        //            new KeyValuePair<string, string>("password", password)
        //        });

        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Post,
        //        RequestUri = new Uri(tokenRequestEndpointUrl),
        //        Content = content
        //    };


        //    request.Headers.UserAgent.ParseAdd(string.Concat(UserAgent, "/", ApiVersion));

        //    var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);
        //    var response =  await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var authToken = JsonConvert.DeserializeObject<AuthToken>(response);

        //        AccessToken = authToken.AccessToken;
        //        InstanceUrl = authToken.InstanceUrl;
        //        Id = authToken.Id;
        //    }
        //    else
        //    {
        //        var errorResponse = JsonConvert.DeserializeObject<AuthErrorResponse>(response);
        //        throw new Exception(string.Concat("Error:" + errorResponse.Error + "|Description:" + errorResponse.ErrorDescription + "|StatusCode:" + responseMessage.StatusCode));
        //    }
        //}
    }

    //public class AuthErrorResponse
    //{
    //    [JsonProperty(PropertyName = "error_description")]
    //    public string ErrorDescription;

    //    [JsonProperty(PropertyName = "error")]
    //    public string Error;
    //}

    //public class AuthToken
    //{
    //    [JsonProperty(PropertyName = "id")]
    //    public string Id;

    //    [JsonProperty(PropertyName = "issued_at")]
    //    public string IssuedAt;

    //    [JsonProperty(PropertyName = "instance_url")]
    //    public string InstanceUrl;

    //    [JsonProperty(PropertyName = "signature")]
    //    public string Signature;

    //    [JsonProperty(PropertyName = "access_token")]
    //    public string AccessToken;

    //    [JsonProperty(PropertyName = "refresh_token")]
    //    public string RefreshToken;
    //}


}
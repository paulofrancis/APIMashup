using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIsMashup.WebApi;
using Newtonsoft.Json.Linq;

namespace APIsMashup.API
{
    public class Twitter : IApi
    {
        public string ConsumerKey { get => "ConsumerKey"; }
        public string ConsumerSecret { get => "ConsumerSecret"; }

        public List<string> Search(string what)
        {
            var webRequest = new WebApiClient();
            webRequest.Uri = "https://api.twitter.com/oauth2/token";
            webRequest.Content = new StringContent("grant_type=client_credentials", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            
            var consumer = System.Convert.ToBase64String(new System.Text.UTF8Encoding()
                                .GetBytes(this.ConsumerKey + ":" + this.ConsumerSecret));
            webRequest.Headers.Add("Authorization", "Basic " + consumer);

            string response = webRequest.PostStringJsonRequest();
            JObject json = JObject.Parse(response);
            
            string access_token = (string)json["access_token"];
            
            webRequest.Uri = string.Format("https://api.twitter.com/1.1/search/tweets.json?q={0}&count=10", what);
            webRequest.Headers.Clear();
            webRequest.Headers.Add("Authorization", "Bearer " + access_token);

            response = webRequest.GetStringJsonRequest();

            JObject tt = JObject.Parse(response);
                
            var tweets =
                    from p in tt["statuses"]
                    select (string)p["text"];
            
            var listTweets = new List<string>();
            foreach (var tweet in tweets)
            {
                listTweets.Add(tweet);
            }

            return listTweets;
        }
    }
}

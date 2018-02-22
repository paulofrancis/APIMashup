using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using APIsMashup.WebApi;
using Newtonsoft.Json.Linq;

namespace APIsMashup.API
{
    public class GitHub : IApi
    {
        public string ConsumerKey => "";

        public string ConsumerSecret => "";

        public List<string> Search(string what)
        {
            var webRequest = new WebApiClient();
            webRequest.Uri = string.Format("https://api.github.com/search/repositories?q={0}", what);
            webRequest.Headers.Add("User-Agent", ".NET Foundation Repository Reporter");
            
            string response = webRequest.GetStringJsonRequest();

            JObject json = JObject.Parse(response);

            var names =
                from p in json["items"]
                select (string)p["name"];

            var repositories = new List<string>();
            foreach (var item in names)
            {
                repositories.Add(item);
            }

            return repositories;
        }
    }
}
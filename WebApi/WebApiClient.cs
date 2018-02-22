using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APIsMashup.WebApi
{
    public class WebApiClient : IWebApiClient
    {
        private string _uri = "";
        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        private StringContent _content = null;
        public string Uri { get => this._uri; set => this._uri = value; }
        public Dictionary<string, string> Headers { get => this._headers; set => this._headers = value; }
        public StringContent Content { get => this._content; set => this._content = value; }

        public string GetStringJsonRequest()
        {
            var request = WebRequest.Create(this._uri) as HttpWebRequest;
            request.Method = "GET";

            foreach (var header in this._headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            using (var resp = request.GetResponse().GetResponseStream())
            {
                var respR = new System.IO.StreamReader(resp);
                return respR.ReadToEnd();
            }
        }

        public string PostStringJsonRequest()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, this._uri);

            request.Content = this._content;

            foreach (var header in this._headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            
            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            using (HttpContent content = response.Content)
            {
                Task<string> result =  content.ReadAsStringAsync();
                return result.Result;
            }
        }
    }
}
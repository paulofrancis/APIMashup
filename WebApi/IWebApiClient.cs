using System.Collections.Generic;
using System.Net.Http;

namespace APIsMashup.WebApi
{
    public interface IWebApiClient
    {
        string Uri { get; set; }
        Dictionary<string, string> Headers { get; set; }
        StringContent Content { get; set; }
        string GetStringJsonRequest();
        string PostStringJsonRequest();

    }
}
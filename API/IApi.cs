using System.Collections.Generic;

namespace APIsMashup.API
{
    public interface IApi
    {
        string ConsumerKey { get; }
        string ConsumerSecret{ get; }
        List<string> Search(string what);
    }
}
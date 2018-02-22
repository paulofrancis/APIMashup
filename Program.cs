using System;
using System.Collections.Generic;

namespace APIsMashup
{
    class Program
    {
        static void Main(string[] args)
        {
            var gitApi = new API.GitHub();
            var twitterApi = new API.Twitter();

            Console.WriteLine("Type a project to search: ");
            var search = Console.ReadLine();

            var repositories = gitApi.Search(search);

            var dontRepeat = new List<string>();
            foreach (var repository in repositories)
            {
                if (dontRepeat.Contains(repository))
                    continue;

                dontRepeat.Add(repository);
                Console.WriteLine("Repository: " + repository);

                foreach (var tweet in twitterApi.Search(repository))
                {
                    Console.WriteLine("- " + tweet);
                }
                Console.WriteLine();
            }
        }
    }
}

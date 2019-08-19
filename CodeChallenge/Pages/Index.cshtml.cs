namespace CodeChallenge.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JokeLibrary;
    using JokeLibrary.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;

    public class IndexModel : PageModel
    {
        private IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public bool IsSearch { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var jokeEndpoint = _configuration.GetValue<string>("JokeEndpoint");
            if (!IsSearch)
            {
                AbstractJoke joke = new RandomJoke(new RestSharpClient(jokeEndpoint));
                var response = joke.TellMeAJoke<RandomResponse, string>("");
                return new JsonResult(response);
            }
            else
            {
                AbstractJoke joke = new SearchJoke(new RestSharpClient(jokeEndpoint));
                var response = joke.TellMeAJoke<SearchResponse, SearchRequest>(new SearchRequest { term = SearchText, limit = 30 });
                if (!string.IsNullOrEmpty(SearchText))
                    foreach (var item in response.results)
                    {
                        item.joke = item.joke.Replace(SearchText, $"<b><i>{SearchText}</i></b>", StringComparison.OrdinalIgnoreCase);
                    }
                var jokeGroupsResponse = from jokeDto in response.results
                             let wordCount = jokeDto.joke.Split().Length
                             let jokeGroup = wordCount < 10 ? "Short" : wordCount < 20 ? "Medium" : "Long"
                             orderby wordCount descending
                             group jokeDto by jokeGroup into g
                             select new KeyValuePair<string, IEnumerable<string>>(g.Key, g.Select(x=> x.joke));
                return new JsonResult(jokeGroupsResponse);
            }
        }
    }
}
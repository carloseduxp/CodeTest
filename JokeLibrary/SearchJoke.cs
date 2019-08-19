namespace JokeLibrary
{
    using JokeLibrary.Models;
    using System.Collections.Generic;

    public class SearchJoke : AbstractJoke
    {
        public SearchJoke(IRestClient restClient) : base(restClient)
        {

        }

        public override void BuildRequest()
        {            
            _restClient.CreateRequest("search");
        }

        public override void SetRequestParams<T>(T requestDto)
        {
            _parameters = new List<KeyValuePair<string, string>>();
            SearchRequest request = requestDto as SearchRequest;
            if (requestDto != null)
            {
                if (request.page.HasValue)
                    _parameters.Add(new KeyValuePair<string, string>(nameof(SearchRequest.page), request.page.ToString()));
                if (request.limit.HasValue)
                    _parameters.Add(new KeyValuePair<string, string>(nameof(SearchRequest.limit), request.limit.ToString()));
                if (!string.IsNullOrEmpty(request.term))
                    _parameters.Add(new KeyValuePair<string, string>(nameof(SearchRequest.term), request.term));
            }
            _restClient.AddParameters(_parameters);
        }
    }
}

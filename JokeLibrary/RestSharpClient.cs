namespace JokeLibrary
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RestSharpClient : IRestClient
    {
        private RestClient _restClient;
        private RestRequest _restRequest;

        public RestSharpClient(string url)
        {
            _restClient = new RestClient(url);
        }

        public void CreateRequest(string uri)
        {
            _restRequest = new RestRequest(uri);
        }

        public void AddParameters(IEnumerable<KeyValuePair<string, string>> values)
        {
            if (_restRequest != null)
            foreach (var keyValue in values)
            {
                    _restRequest.AddParameter(keyValue.Key, keyValue.Value);
            }            
        }

        public T Get<T>() where T : class
        {
            _restRequest.Method = Method.GET;
            IRestResponse response = _restClient.Execute(_restRequest);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public string Post<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

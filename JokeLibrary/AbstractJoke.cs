namespace JokeLibrary
{
    using System.Collections.Generic;

    public abstract class AbstractJoke
    {
        protected readonly IRestClient _restClient;
        protected IList<KeyValuePair<string, string>> _parameters;
        public AbstractJoke(IRestClient restClient)
        {
            _restClient = restClient;
        }
        

        public abstract void BuildRequest();
        public abstract void SetRequestParams<T>(T requestDto);
        public TResponse TellMeAJoke<TResponse, TRequest>(TRequest requestDto) where TResponse : class where TRequest : class
        {
            BuildRequest();
            SetRequestParams(requestDto);
            return _restClient.Get<TResponse>();
        }

    }
}

namespace JokeLibrary
{
    public class RandomJoke : AbstractJoke
    {
        public RandomJoke(IRestClient restClient) : base(restClient)
        {

        }

        public override void BuildRequest()
        {
            _restClient.CreateRequest("");
        }

        public override void SetRequestParams<T>(T requestDto)
        {

        }
    }
}

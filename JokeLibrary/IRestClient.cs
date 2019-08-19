namespace JokeLibrary
{
    using System.Collections.Generic;

    public interface IRestClient
    {
        void CreateRequest(string uri);
        void AddParameters(IEnumerable<KeyValuePair<string, string>> values);
        T Get<T>() where T : class;
    }
}

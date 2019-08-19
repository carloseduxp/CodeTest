namespace JokeLibrary.Models
{
    public class SearchRequest
    {
        public int? page { get; set; }
        public int? limit { get; set; }
        public string term { get; set; }
    }
}

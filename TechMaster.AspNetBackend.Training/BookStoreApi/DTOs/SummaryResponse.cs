namespace BookStoreApi.DTOs
{
    public class SummaryResponse
    {
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int OutOfStock {  get; set; }
        public Dictionary<string, int> BooksPerCategory { get; set; } = new();
        public Dictionary<string,int> BooksPerAuthor { get; set; }= new();
        public decimal TotalInvetoryValue { get; set; }
    }
}

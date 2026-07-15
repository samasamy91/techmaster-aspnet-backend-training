namespace BookStoreApi.DTOs
{
    public class BookResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuatity { get; set; }
        public string AuthorName { get; set; } 
        public string CategoryName { get; set; } 
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

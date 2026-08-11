namespace TrainingCenter.Api.Common
{
    public class PagedRequest
    {
        private const int MaxPageSize = 100;
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set {
                if (value < 1)
                    pageSize = 1;
                else if (value > MaxPageSize)
                    pageSize = MaxPageSize;
                else
                    pageSize = value;
            } 
        }
    }
}

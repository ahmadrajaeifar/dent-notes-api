namespace DentalClinic.Api.DTOs.Common
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        
        private int _pageSize = 10;

        public int PageSize { 
            get => _pageSize;
            set => _pageSize = 
                (value > MaxPageSize) ? 
                MaxPageSize : value;
        }

        public class PagedResult<T>
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public IEnumerable<T> Items { get; set; } = new List<T>();
        }

        public string? Search { get; set; }
        public string? SortBy { get; set; } = "id";
        public string? SortOrder { get; set; } = "desc";
    }
}

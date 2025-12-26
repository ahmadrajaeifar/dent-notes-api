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

        public string? Search { get; set; }
        public string? SortBy { get; set; } = "id";
        public string? SortOrder { get; set; } = "desc";
    }
}

namespace ChattingApp.Resource.Pagination
{
    public class BasePaginationDto
    {
        private const int MaxPageSize= 5000;
        public int PageNumber { get; set; } = 1;

        private int _PageSize = 50;

        public int PageSize
        {
            get => _PageSize;
            set =>_PageSize= (value > MaxPageSize) ?  MaxPageSize: value;
        }

    }
}

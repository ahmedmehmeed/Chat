namespace ChattingApp.Helper.Pagination
{
    public class PagedList<T>:List<T>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items,int pageSize, int PageNumber, int totalCount)
        {
            PageSize = pageSize;
            CurrentPage = PageNumber;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize);
            AddRange(items);
        }


        public static PagedList<T> ToPagedList(IQueryable<T> source,int pageSize , int pageNumber)
        {
            var count =  source.Count();
            //var items =  source.Take(((pageNumber - 1) * pageSize)..(pageNumber * pageSize)).ToList();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items,  pageSize, pageNumber,count);
        }

    }
}

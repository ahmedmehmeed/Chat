using ChattingApp.Resource.Pagination;

namespace ChattingApp.Resource.User
{
    public class UserReqDto: BasePaginationDto
    {
        public string? userName { get; set; }
        public string? Gender { get; set; }
        public bool lastActive { get; set; }
        public bool CreateDate { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }

    }
}

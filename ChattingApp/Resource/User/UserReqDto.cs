using ChattingApp.Resource.Pagination;

namespace ChattingApp.Resource.User
{
    public class UserReqDto: BasePaginationDto
    {
        public string? userName { get; set; }
        public string? Gender { get; set; }


    }
}

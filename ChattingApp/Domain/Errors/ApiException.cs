namespace ChattingApp.Domain.Errors
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public String? Message { get; set; }
        public String? Details { get; set; }

        public ApiException(int _StatusCode,string _Message, string _Details)
        {
            StatusCode = _StatusCode;
            Message = _Message;
            Details = _Details;
        }
    }
}

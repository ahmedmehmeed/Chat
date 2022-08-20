using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Helper.Security.Tokens
{
    [Owned]
    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsEXpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsActive => RevokedOn is not null && !IsEXpired;
    }
}

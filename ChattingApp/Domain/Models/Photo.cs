using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingApp.Domain.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        [ForeignKey("appUsersId")]
        public AppUsers appUsers { get; set; }

        public string appUsersId { get; set; }


    }
}
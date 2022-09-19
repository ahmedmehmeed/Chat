

namespace ChattingApp.Extensions
{
    public static class DateTimeExtension 
    {
        public static int CalculateAge(this DateTime datebirth)
        {
            var today = DateTime.Now;
            var age = today.Year - datebirth.Year;
            return age;
        }

    }
}

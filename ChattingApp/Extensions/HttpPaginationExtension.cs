using ChattingApp.Helper.Pagination;
using System.Text.Json;

namespace ChattingApp.Extensions
{
    public static class HttpPaginationExtension
    {

        public static void AddPaginationToHeader(this HttpResponse httpResponse, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            httpResponse.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, option));
            httpResponse.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }

    }
}

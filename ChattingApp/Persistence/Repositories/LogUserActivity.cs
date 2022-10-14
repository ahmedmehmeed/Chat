using ChattingApp.Persistence.IRepositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ChattingApp.Persistence.Repositories
{
    public class LogUserActivity : ILogUserActivity
    {
 
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
            var username = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var repo=  resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repo.GetUserByNameAsync(username);
            user.LastActive = DateTime.Now;
            await repo.SaveChangesAsync();
        }
    }
}

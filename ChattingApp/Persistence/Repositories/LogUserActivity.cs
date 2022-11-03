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
            var uow=  resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await uow.UserRepository.GetUserByNameAsync(username);
            user.LastActive = DateTime.UtcNow;
            await uow.Commit();
        }
    }
}

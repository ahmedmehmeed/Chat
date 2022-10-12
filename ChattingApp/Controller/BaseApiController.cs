using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChattingApp.Controller
{   
    [ServiceFilter(typeof(ILogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}

using ChattingApp.Domain.Models;
using ChattingApp.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChattingApp.Controller
{

    public class ErrorsController : BaseApiController
    {
        private readonly AppDbContext appDbContext;

        public ErrorsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSectret()
        {
            return  "Secret Text";

        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
                // find(-1) will return null 
                var nullReturn = appDbContext.Users.Find("-1");
                var fireNullException = nullReturn.ToString();
                return fireNullException;
        }
          
        [HttpGet("not-found")]
        public ActionResult<AppUsers> GetNotFounded()
        {
            // find(-1) will return null 
           var nullReturn =   appDbContext.Users.Find("-1");
            if (nullReturn is null) return NotFound();
              
            return Ok(nullReturn); 
              
        }


        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return  BadRequest("this was bad request");

        }
 



    }
}

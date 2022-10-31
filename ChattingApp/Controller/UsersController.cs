using ChattingApp.Domain;
using ChattingApp.Domain.Models;
using ChattingApp.Extensions;
using ChattingApp.Helper.Pagination;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.IServices;
using ChattingApp.Resource.Messager;
using ChattingApp.Resource.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChattingApp.Controller
{
    [Authorize]
    public class UsersController : BaseApiController 
    {
        private readonly IUserRepository userRepository;
        private readonly IUploadPhotoService uploadPhotoService;
        private readonly IMessagerService messagerService;

        public UsersController(IUserRepository userRepository,IUploadPhotoService uploadPhotservice,IMessagerService messagerService )
        {
            this.userRepository = userRepository;
            this.uploadPhotoService = uploadPhotservice;
            this.messagerService = messagerService;
        }

        [HttpPost("Send-Mail")]
        public async Task<ActionResult> SendMail([FromForm] MailReqDto mailReqDto)
        {
            await messagerService.SendMailAsync(mailReqDto.MailTo, mailReqDto.Subject, mailReqDto.Body, mailReqDto.Attachments);
            return Ok();
        }

        [HttpPost("Send-Welcome-Mail")]
        public async Task<ActionResult> SendWelcomeMail([FromForm] MailReqDto mailReqDto)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Templates\\Emails\\ConfirmationEmail.html";
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            await messagerService.SendMailAsync(mailReqDto.MailTo, mailReqDto.Subject,mailText, mailReqDto.Attachments);
            return Ok();
        }


        // GET: api/<UsersController>
        [HttpPost("GetAllUsers")]
 
        public async Task<ActionResult>  Get(UserReqDto userReqDto)
        {
               var Users = await  userRepository.GetUsersAsync(userReqDto);
               Response.AddPaginationToHeader(Users.CurrentPage, Users.PageSize, Users.TotalCount, Users.TotalPages);
                return Ok(Users);          
        }


        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public async Task<ActionResult<AppUsers>> Get(string id)
        {
           
                var User = await userRepository.GetUserByIdAsync(id);
            if (User is not null)
                return Ok(User);
            else
                return NotFound();
        }

  

        // PUT api/<UsersController>/5
        [HttpPost("UpdateUser")]
        public  async Task< ActionResult<BaseResponse>> Post([FromBody] UserUpdateDto userUpdateDto)
        {
            if(userUpdateDto is not null)
            {
             var rowsAffected = await userRepository.UpdateUserAsync(userUpdateDto);
                if (rowsAffected > 0) return Ok(new BaseResponse(true, 200, "User Updated Successfully"));
                else return BadRequest(" Some thing went wrong failed to update !");
            }
            else
            {
                return BadRequest();
            }
        }


        // PUT api/<UsersController>/5
        [HttpPost("Add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto([FromForm] Uploadphoto uploadphoto)
        {
            if (uploadphoto is null) return BadRequest();

            var rowsEffected = await uploadPhotoService.UploadPhotoAsync(uploadphoto);
                if (rowsEffected > 0) return Ok();  
            return BadRequest();
        }


        // DELETE api/<UsersController>/5
        [HttpDelete("Delete-photo")]
        public async Task<ActionResult> Delete(string PublicId)
        {
            if (string.IsNullOrEmpty(PublicId)) return BadRequest();

            var rowsEffected = await uploadPhotoService.DeletePhotoAsync(PublicId);
                        if (rowsEffected > 0) return Ok();
            return BadRequest();
        }
    }
}

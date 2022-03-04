using Data.Intefaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProducerBusiniess.ProducerBusiness;
using Utils.Services;

namespace MailProducer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("GetAuthorize")]
        public IActionResult GetAuthorize(User user)
        {
            MongoBusiness MB = new MongoBusiness();
            User usr = MB.GetUserById(user._id);
            if (user.UserName == usr.UserName && user.Password == usr.Password)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Data.Models.Token token = tokenHandler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                return Ok(token);
            }
            return BadRequest();
        }
        [HttpPost("AddUser")]
        public void AddUser(User user)
        {
            MongoBusiness MB = new MongoBusiness();
            MB.AddUser(user);
        }

        [Authorize]
        [HttpGet("AuthTest")]
        public IActionResult AuthtTest()
        {
            return Ok();
        }
    }
}

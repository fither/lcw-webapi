using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using DataAccess.Abstract;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class UsersController: ControllerBase
    {

        private IUserServiceRepository _userService;

        public UsersController(IUserServiceRepository userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] Auth userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
            {
                return BadRequest(new Response(
                        400,
                        false,
                        "Username or password is wrong!"
                    ));
            }
            return Ok(new Response(
                    200,
                    true,
                    user
                ));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(new Response(
                    200,
                    true,
                    users
                ));
        }

    }
}

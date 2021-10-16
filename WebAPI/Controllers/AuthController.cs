using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Entities.DataTransferObjects;
using AutoMapper;
using DataAccess.Abstract;
using Business.Abstract;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;

        public AuthController(IMapper mapper, IRepositoryWrapper wrapper)
        {
            _mapper = mapper;
            _wrapper = wrapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthDto auth)
        {
            var user = _wrapper.Auth.Authenticate(auth);

            if (user == null)
            {
                return BadRequest("Username or password is wrong!");
            }
            if (!user.Confirmed)
            {
                return BadRequest("User is not confirmed");
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            if(userId == null || userId == "")
            {
                return NotFound("User not found");
            }

            var user = _wrapper.User.GetById(int.Parse(userId));

            return Ok(user);
        }
    }
}

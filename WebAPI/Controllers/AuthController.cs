using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Entities.DataTransferObjects;
using AutoMapper;
using DataAccess.Abstract;
using Business.Abstract;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IAuthRepository _auth;
        private IMapper _mapper;

        public AuthController(IAuthRepository auth, IMapper mapper, IRepositoryWrapper wrapper)
        {
            _auth = auth;
            _mapper = mapper;
            _wrapper = wrapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthDto auth)
        {
            var user = _auth.Authenticate(auth);

            if (user == null)
            {
                return BadRequest("Username or password is wrong!");
            }
            return Ok(user);
        }
    }
}

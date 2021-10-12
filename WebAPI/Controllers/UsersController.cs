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
    public class UsersController: ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IUserServiceRepository _userService;
        //private IUserRepository _userRepository;
        private IMapper _mapper;

        public UsersController(IUserServiceRepository userService, IMapper mapper, IRepositoryWrapper wrapper)
        {
            _userService = userService;
            //_userRepository = userRepository;
            _mapper = mapper;
            _wrapper = wrapper;
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
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(new Response(
                    200,
                    true,
                    users
                ));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Add([FromBody] User user)
        {
            _wrapper.User.Create(user);
      
            _wrapper.Save();
            return Ok();
        }

    }
}

using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController: ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManagerRepository _logger;
        public UsersController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManagerRepository logger)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _wrapper.User.GetAll();
                var usersResult = _mapper.Map<List<UserDto>>(users);

                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult Create(UserAddDto user)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Form is not valid");
                }

                var existingUser = _wrapper.User.GetByEmail(user.EmailAddress);

                if(existingUser != null)
                {
                    return BadRequest("User exist!!");
                }

                var newUser = _mapper.Map<User>(user);

                _wrapper.User.Create(newUser);
                _wrapper.Save();

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

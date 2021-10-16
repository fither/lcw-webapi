using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(UserAddDto user)
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

                await _wrapper.User.CreateAsync(newUser);

                _wrapper.Save();

                return Ok("Registered Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccount account)
        {
            try
            {
                User user = _wrapper.User.GetByEmail(account.EmailAddress);

                if(user == null)
                {
                    return BadRequest("User not exist");
                }
                if(user.Confirmed == true)
                {
                    return BadRequest("User confirmed already");
                }

                if(user.ConfirmCode != account.ConfirmCode)
                {
                    return BadRequest("Confirm code is wrong!!");
                }

                User newUser = user;
                newUser.ConfirmCode = "";
                newUser.Confirmed = true;

                _wrapper.User.Update(newUser);
                _wrapper.Save();

                return Ok("Confirmed Successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

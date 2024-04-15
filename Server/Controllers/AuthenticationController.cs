using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAccount _userAccount;

        public AuthenticationController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(Register user)
        {
            if(user is null)
            {
                return BadRequest("Model is empty");
            }
            var result  = await _userAccount.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Login user)
        {
            if(user == null) return BadRequest("Model is empty!");
            var result = await _userAccount.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if(token == null) return BadRequest("Model is empty!");
            var result = await _userAccount.RefreshTokenAsync(token);
            return Ok(result);
        }
    }
}
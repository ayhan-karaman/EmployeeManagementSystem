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

        [HttpPost]
        public async Task<IActionResult> Register(Register user)
        {
            if(user is null)
            {
                return BadRequest("Model is empty");
            }
            var result  = await _userAccount.CreateAsync(user);
            return Ok(result);
        }
    }
}
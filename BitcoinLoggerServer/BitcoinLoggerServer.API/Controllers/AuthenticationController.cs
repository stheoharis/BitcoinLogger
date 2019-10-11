using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BitcoinLoggerServer.API.Controllers
{
    [ApiController]    
    [Authorize]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
       
        private readonly AuthenticationService _AuthenticationService;

        public AuthenticationController(AuthenticationService securityService) 
        {

            _AuthenticationService = securityService;
        }

        [AllowAnonymous]
        [HttpPost]        
        public ActionResult<UserSession> login([FromBody] User user)
        {
            var result = this._AuthenticationService.Login(user);

            if (result != null) return result;
            else return Unauthorized();

        }
        
    }
}

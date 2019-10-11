using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BitcoinLoggerServer.API.Controllers
{
    [ApiController]    
    [Route("[controller]")]    
    public class UserController : BaseController<User>
    {
      
        public UserController(UserService userService) : base(userService)
        {            
        }
        
    }
}

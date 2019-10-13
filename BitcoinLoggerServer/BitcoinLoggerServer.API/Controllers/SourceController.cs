using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BitcoinLoggerServer.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SourceController : BaseController<Source>
    {

        public SourceController(SourceService sourceService) : base(sourceService)
        {
        }
    }
}

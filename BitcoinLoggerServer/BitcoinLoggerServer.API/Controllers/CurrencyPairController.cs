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
    [Route("[controller]")]
    public class CurrencyPairController : ControllerBase
    {
        private readonly CurrencyPairService _CurrencyPairService;

        public CurrencyPairController(CurrencyPairService currencyPairService)
        {
            _CurrencyPairService = currencyPairService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<CurrencyPair> Get()
        {
            return _CurrencyPairService.Get();
        }
    }
}

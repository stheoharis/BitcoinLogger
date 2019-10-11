using BitcoinLoggerServer.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Contracts.Models
{
    public class JWTSettings : IJWTSettings
    {
        public string PrivateKey { get; set; }
        public int ExpirationValue { get; set; }
        public string ExpirationMode { get; set; }
    }
}

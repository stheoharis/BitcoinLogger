using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Framework.Interfaces
{
    public interface IJWTSettings
    {
        string PrivateKey { get; set; }
        int ExpirationValue { get; set; }
        string ExpirationMode { get; set; }
    }
}

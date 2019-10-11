using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Security.Interfaces
{
    public interface IUser
    {
        long Id { get; set; }
        string Email { get; set; }
    }
}

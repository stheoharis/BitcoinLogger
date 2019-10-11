using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Framework.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Email { get; set; }
    }
}

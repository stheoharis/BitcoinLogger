using BitcoinLoggerServer.Framework.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Contracts.Models
{
    public class UserSession : BaseEntity
    {
        public int UserId { get; set; }
        public string SessionKey { get; set; }
    }
}

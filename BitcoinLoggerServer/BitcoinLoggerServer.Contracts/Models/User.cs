using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Contracts.Models
{
    public class User : BaseEntity, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
    }
}

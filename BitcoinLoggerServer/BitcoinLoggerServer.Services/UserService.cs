using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(UserRepository userRepository) : base(userRepository) { }
    }
}

using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace BitcoinLoggerServer.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext) { }

    }
}

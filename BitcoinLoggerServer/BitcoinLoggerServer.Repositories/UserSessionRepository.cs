using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Repositories
{
    public class UserSessionRepository : BaseRepository<UserSession>
    {
        public UserSessionRepository(DbContext dbContext) : base(dbContext) { }
    }
}

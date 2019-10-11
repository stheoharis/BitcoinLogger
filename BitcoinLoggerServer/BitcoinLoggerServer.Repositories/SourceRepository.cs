using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Repositories.DBContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace BitcoinLoggerServer.Repositories
{
    public class SourceRepository : BaseRepository<Source>
    {
        public SourceRepository(DbContext dbContext) : base(dbContext) { }

    }
}

using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Services
{
    public class SourceService : BaseService<Source>
    {
        public SourceService(SourceRepository sourceRepository) : base(sourceRepository) { }
                 
    }
}

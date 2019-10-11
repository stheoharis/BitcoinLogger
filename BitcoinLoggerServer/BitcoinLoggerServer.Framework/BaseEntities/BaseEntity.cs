using BitcoinLoggerServer.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Framework.BaseEntities
{
    public abstract class BaseEntity : SimpleEntity
    {
        public int Id { get; set; }        
    }
}

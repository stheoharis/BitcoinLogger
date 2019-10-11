using BitcoinLoggerServer.Framework.BaseEntities;
using BitcoinLoggerServer.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Contracts.Models
{
    public class Source : BaseEntity
    {
        public string Name { get; set; }
        public string Endpoint { get; set; }        
    }
}

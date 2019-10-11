using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Framework.Models
{
    public abstract class SimpleEntity
    {       
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}

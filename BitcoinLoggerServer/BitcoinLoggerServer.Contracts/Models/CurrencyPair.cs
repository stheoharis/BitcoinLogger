using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Contracts.Models
{
    public class CurrencyPair
    {
        public decimal? Price { get; set; }
        public DateTime TimeStamp { get; set; }
        public Source Source { get; set; }
    }
}

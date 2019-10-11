using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Framework.Models
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {

        }
    }
}

using BitcoinLoggerServer.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Services
{
    internal static class Mapper
    {
        internal static CurrencyPair ConvertJSONToCurrecyPair(string json, Source source)
        {

            if (source.Endpoint == "https://www.bitstamp.net/api/ticker/")
            {
                CurrencyPair result = new CurrencyPair();

                result.Source = source;

                dynamic tempCurrencyPair = JsonConvert.DeserializeObject(json);
                
                if (tempCurrencyPair == null) return null;

                result.Price = tempCurrencyPair.last;
                if (tempCurrencyPair.timestamp != null)
                {
                    double unixTimeStamp = double.Parse(tempCurrencyPair.timestamp.ToString());
                    result.TimeStamp = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    result.TimeStamp = result.TimeStamp.AddSeconds(unixTimeStamp).ToLocalTime();
                }                   

                return result;
            }
            else
            if (source.Endpoint == "https://api.gdax.com/products/BTC-USD/ticker")
            {
                CurrencyPair result = new CurrencyPair();
                result.Source = source;

                dynamic tempCurrencyPair = JsonConvert.DeserializeObject(json);

                if (tempCurrencyPair == null) return null;

                result.Price = tempCurrencyPair.price;
                
                if (tempCurrencyPair.time != null)
                    result.TimeStamp = DateTime.Parse(tempCurrencyPair.time.ToString().Replace('/', '-'));

                return result;
            }

            throw new Exception("ConvertJSONToCurrecyPair: Could not find source url.");
        }
    }
}

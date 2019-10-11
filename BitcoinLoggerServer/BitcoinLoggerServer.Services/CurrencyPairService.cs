using BitcoinLoggerServer.Contracts.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLoggerServer.Services
{
    public class CurrencyPairService
    {
        private HttpClient _HttpClient { get; set; }
        private List<string> _Endpoints;

        public CurrencyPairService(HttpClient httpClient, List<string> endpoints)
        {
            _HttpClient = httpClient;
            _Endpoints = endpoints;
        }

        public List<CurrencyPair> Get()
        {
            return GetCurrencyPairsAsync(_Endpoints);
        }

        private List<CurrencyPair> GetCurrencyPairsAsync(List<string> endpoints)
        {
            if (endpoints == null || endpoints.Count == 0) throw new Exception("No endpoints were specified.");

            List<CurrencyPair> currencyPairs = new List<CurrencyPair>();

            Dictionary<string, HttpResponseMessage> sourceResponsesKeyValues = new Dictionary<string, HttpResponseMessage>();

            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

            _HttpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            Parallel.ForEach(endpoints, endpoint =>
            {
                try
                {
                    //if (endpoint == "https://www.bitstamp.net/api/ticker/")
                    //    System.Threading.Thread.Sleep(1000);
                    sourceResponsesKeyValues.Add(endpoint, _HttpClient.GetAsync(endpoint).Result);
                }
                catch (Exception ex)
                {
                    //LOG RESPONSE HERE MAYBE??
                    //THIS BLOCK IS USED FOR BAD RESPONSES					
                    exceptions.Enqueue(ex);
                }
            });


            foreach (var keyValue in sourceResponsesKeyValues)
            {
                try
                {
                    string jsonCurrencyPair = keyValue.Value.Content.ReadAsStringAsync().Result;

                    CurrencyPair currencyPair = Mapper.ConvertJSONToCurrecyPair(jsonCurrencyPair, keyValue.Key);

                    currencyPairs.Add(currencyPair);
                }
                catch (Exception ex)
                {
                    exceptions.Enqueue(ex);
                }
            }

            return currencyPairs;

        }

    }

}

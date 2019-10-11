using BitcoinLoggerServer.Contracts.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLoggerServer.Services
{
    public class CurrencyPairService
    {
        private HttpClient _HttpClient { get; set; }

        private readonly SourceService _SourceService;
        
        public CurrencyPairService(HttpClient httpClient, SourceService sourceService)
        {
            _HttpClient = httpClient;
            _SourceService = sourceService;            
        }

        public List<CurrencyPair> Get()
        {
            return GetCurrencyPairsAsync(_SourceService.Get().ToList());            
        }

        public List<CurrencyPair> GetBySources(List<Source> sources)
        {
            return GetCurrencyPairsAsync(sources);
        }

        private List<CurrencyPair> GetCurrencyPairsAsync(List<Source> sources)
        {
            if (sources == null || sources.Count == 0) throw new Exception("No endpoints were specified.");

            List<CurrencyPair> currencyPairs = new List<CurrencyPair>();

            Dictionary<Source, HttpResponseMessage> sourceResponsesKeyValues = new Dictionary<Source, HttpResponseMessage>();

            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

            _HttpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            //THERES A BETTER METHOD!!
            Parallel.ForEach(sources, source =>
            {
                try
                {                   
                    sourceResponsesKeyValues.Add(source, _HttpClient.GetAsync(source.Endpoint).Result);
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

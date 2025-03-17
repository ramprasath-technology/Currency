using CurrencyConverter.DataModel;
using Microsoft.Extensions.Configuration;
using NetworkCalls.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Service
{
    public class FixerAPICurrencyConverterService : ICurrencyConverterService
    {
        private readonly IGetCalls _getCalls;
        private readonly IConfiguration _configurator;

        public FixerAPICurrencyConverterService(IGetCalls getCalls,
            IConfiguration configurator)
        {
            _getCalls = getCalls;
            _configurator = configurator;
        }

        public async Task<CurrencyConversion> GetExchangeRate()
        {
            var currencyConversionUrl = GetCurrencyCoversionUrl();
            CurrencyConversion? conversionRate = await _getCalls.Get<CurrencyConversion>(currencyConversionUrl);
            if (conversionRate == null)
            {
                throw new Exception($"The currency conversion call returned null");
            }
            return conversionRate;
        }

        private string GetCurrencyCoversionUrl()
        {
            var fixerAPIConfiguration = _configurator.GetSection("FixerAPI");
            var baseUrl = fixerAPIConfiguration?.GetValue<string>("BaseUrl") ?? string.Empty;
            var apiKey = fixerAPIConfiguration?.GetValue<string>("AccessKey") ?? string.Empty;

            if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(apiKey))
            {
                throw new Exception($"Error retrieving the configuration for FixerAPI call");
            }

            var baseUrlWithKey =  $"{baseUrl}?access_key={apiKey}";

            return baseUrlWithKey;
        }
    }
}

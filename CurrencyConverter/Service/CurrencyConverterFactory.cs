using Microsoft.Extensions.Configuration;
using NetworkCalls.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Service
{
    public class CurrencyConverterFactory : ICurrencyConverterFactory
    {
        private readonly IConfiguration _configurator;
        private readonly IGetCalls _getCalls;

        public CurrencyConverterFactory(IConfiguration configurator,
            IGetCalls getCalls)
        {
            _configurator = configurator;
            _getCalls = getCalls;   
        }

        public ICurrencyConverterService GetCurrencyConverterService() 
        {
            var serviceTypeSection = _configurator.GetSection("ServiceType");
            var serviceProviderName = serviceTypeSection?.GetValue<string>("Name") ?? string.Empty;

            if (serviceProviderName == "FixerAPI")
            {
                return new FixerAPICurrencyConverterService(_getCalls, _configurator);
            }

            return new FixerAPICurrencyConverterService(_getCalls, _configurator);
        }

    }
}

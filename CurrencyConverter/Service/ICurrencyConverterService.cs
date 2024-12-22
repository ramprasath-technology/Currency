using CurrencyConverter.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Service
{
    public interface ICurrencyConverterService
    {
        Task<CurrencyConversion> GetExchangeRate(string baseCurrency);
    }
}

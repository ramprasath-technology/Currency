using Asp.Versioning;
using CurrencyConverter.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Currency.Controllers.CurrencyConversion
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}[controller]")]
    [ApiController]
    public class CurrencyConversionV1Controller : ControllerBase
    {
        private readonly ICurrencyConverterFactory _currencyConverterFactory;

        public CurrencyConversionV1Controller(ICurrencyConverterFactory currencyConverterFactory)
        {
            _currencyConverterFactory = currencyConverterFactory;
        }

        [HttpGet("conversionrates")]
        public async Task<ActionResult> GetConversionRates()
        {
            try
            {
                var currencyConvesionService = _currencyConverterFactory.GetCurrencyConverterService();
                var exchangeRates = await currencyConvesionService.GetExchangeRate();
                return Ok(exchangeRates);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

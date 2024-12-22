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
        private readonly ICurrencyConverterService _currencyConversionService;

        public CurrencyConversionV1Controller(ICurrencyConverterService currencyConverterService)
        {
            _currencyConversionService = currencyConverterService;
        }

        [HttpGet("conversionrates/{baseCurrency}")]
        public async Task<ActionResult> GetConversionRates(string baseCurrency)
        {
            try
            {
                var exchangeRates = await _currencyConversionService.GetExchangeRate(baseCurrency);
                return Ok(exchangeRates);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

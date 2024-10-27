using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.CurrencySpace;

namespace RetrieveCurrencyInformationApi.Controllers
{
    [ApiController]
    [Route("api/currency")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyHandlers _currencyHandlers;

        public CurrencyController(CurrencyHandlers currencyHandlers)
        {
            _currencyHandlers = currencyHandlers ?? throw new ArgumentNullException(nameof(currencyHandlers));
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestCurrencyExchange() { 
            var result = await _currencyHandlers.GetLatestCurrencyExchange();
            if (!result.IsSuccess)
            {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }
    }
}

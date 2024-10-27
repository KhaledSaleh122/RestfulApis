using RestfulApis_Application.Utilities;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_Application.CurrencySpace
{
    public class CurrencyHandlers
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyHandlers(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        public async Task<Result<Currency>> GetLatestCurrencyExchange() { 
            var currency = await _currencyRepository.GetLatestCurrencyExchange();
            if (currency is null) {
                var errors = new List<Error>() {
                    new Error() { Name = "", Message="Failed to retrieve data from the external service. Please try again later." }
                };
                return new Result<Currency>(new ErrorResult(502, errors));
            }
            return new Result<Currency>(currency);
        } 

    }
}

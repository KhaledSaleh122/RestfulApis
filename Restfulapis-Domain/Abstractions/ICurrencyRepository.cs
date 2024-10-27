using Restfulapis_Domain.Entities;

namespace Restfulapis_Domain.Abstractions
{
    public interface ICurrencyRepository
    {
        public Task<Currency?> GetLatestCurrencyExchange();
    }
}

using Restfulapis_Domain.Entities;

namespace Restfulapis_Domain.Abstractions
{
    public interface ITokenService
    {
        public string GenerateUserToken(User user);
    }
}

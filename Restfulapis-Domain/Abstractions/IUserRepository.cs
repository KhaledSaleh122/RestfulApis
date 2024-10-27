using Restfulapis_Domain.Entities;

namespace Restfulapis_Domain.Abstractions
{
    public interface IUserRepository
    {
        public Task<User?> FindByNameAsync(string name);
        public Task<bool> CheckPasswordSignInAsync(User user, string password);
    }
}

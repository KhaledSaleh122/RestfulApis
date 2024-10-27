using Microsoft.EntityFrameworkCore;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;
namespace RestfulApis_Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public Task<bool> CheckPasswordSignInAsync(User user, string password)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(password,user.Hash));
        }

        public Task<User?> FindByNameAsync(string name)
        {
            return _dbContext.Users.FirstOrDefaultAsync(user => user.Username == name);
        }
    }
}

using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TopicRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task CreateTopicAsync(Topic topic)
        {
            await _dbContext.Topics.AddAsync(topic);
            await _dbContext.SaveChangesAsync();
        }
    }
}

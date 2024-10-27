using Microsoft.EntityFrameworkCore;
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

        public async Task<Topic?> FindTopicByIdAsync(int id)
        {
            return await _dbContext.Topics.FindAsync(id);
        }

        public async Task<(IEnumerable<Topic> Topics ,int TotalRecords)> FindTopics(int page = 1, int pageSize = 10)
        {
            var query = _dbContext.Topics;
            var totalRecords = await query.CountAsync();
            var topics = await query
                .Take(page * pageSize)
                .Skip((page - 1) * pageSize)
                .ToListAsync();
            return (topics,totalRecords);
        }
    }
}

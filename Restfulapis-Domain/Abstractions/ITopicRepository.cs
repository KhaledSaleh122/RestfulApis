using Restfulapis_Domain.Entities;

namespace Restfulapis_Domain.Abstractions
{
    public interface ITopicRepository
    {
        public Task CreateTopicAsync(Topic topic);
        public Task<Topic?> FindTopicByIdAsync(int id);
        public Task<(IEnumerable<Topic> Topics, int TotalRecords)> FindTopics(int page = 1,int pageSize=10);
    }
}

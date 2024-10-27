using Restfulapis_Domain.Entities;

namespace Restfulapis_Domain.Abstractions
{
    public interface ITopicRepository
    {
        public Task CreateTopic(Topic topic);
    }
}

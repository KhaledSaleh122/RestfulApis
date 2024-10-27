using AutoMapper;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_Application.TopicSpace
{
    public class TopicHandlers
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicHandlers(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TopicDto, Topic>();
            });
            _mapper = configuration.CreateMapper();
        }

        public async Task<Result<Topic>> CreateTopicHandler(TopicDto topicDto) {
            var topic = _mapper.Map<Topic>(topicDto);
            await _topicRepository.CreateTopicAsync(topic);
            return new Result<Topic>(topic);
        }
    }
}

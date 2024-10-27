using AutoMapper;
using FluentValidation;
using Restfulapis_Domain.Abstractions;
using Restfulapis_Domain.Entities;

namespace RestfulApis_Application.TopicSpace
{
    public class TopicHandlers
    {
        private readonly ITopicRepository _topicRepository;
        private IValidator<TopicDto> _validator;
        private readonly IMapper _mapper;

        public TopicHandlers(ITopicRepository topicRepository, IValidator<TopicDto> validator)
        {
            _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TopicDto, Topic>();
            });
            _validator = validator;
            _mapper = configuration.CreateMapper();
        }

        public async Task<Result<Topic>> CreateTopicHandler(TopicDto topicDto)
        {
            var result = await _validator.ValidateAsync(topicDto);
            if (!result.IsValid) {
                var errors = result.Errors.Select(x => new Error() { Message = x.ErrorMessage, Name = x.PropertyName }).ToList();
                return new Result<Topic>(
                    new ErrorResult(400,errors)
                );
            }
            var topic = _mapper.Map<Topic>(topicDto);
            await _topicRepository.CreateTopicAsync(topic);
            return new Result<Topic>(topic);
        }

        public async Task<Result<Topic>> GetTopicByIdHandler(int id) {
            var topic = await _topicRepository.FindTopicByIdAsync(id);
            if (topic is null) {
                var errors = new List<Error>() {
                    new Error() { Name = "Id", Message = "The provided topic ID does not exist." }
                };
                return new Result<Topic>(new ErrorResult(404, errors));
            }
            return new Result<Topic>(topic);
        }
    }
}

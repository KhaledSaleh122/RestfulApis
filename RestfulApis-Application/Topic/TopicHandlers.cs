using AutoMapper;
using FluentValidation;
using RestfulApis_Application.Utilities;
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

        public async Task<Result<PageResult<Topic>>> GetTopicsHandler(int page = 1, int pageSize = 10) {
            var pagination = new PaginationParameters(page,pageSize);
            var (topics,totalRecords) = await _topicRepository.FindTopics(pagination.Page, pagination.pageSize);
            return new Result<PageResult<Topic>>(
                new PageResult<Topic>() { 
                    Page = pagination.Page, 
                    PageSize = pagination.pageSize, 
                    Results = topics.ToList(), 
                    TotalRecords = totalRecords 
                }
            );
        }
    }
}

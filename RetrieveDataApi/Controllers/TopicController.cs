using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.TopicSpace;
using Restfulapis_Domain.Abstractions;

namespace RetrieveDataApi.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        private IValidator<TopicDto> _validator;
        public TopicController(ITopicRepository topicRepository, IValidator<TopicDto> validator)
        {
            _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet("topicId")]
        public async Task<IActionResult> GetTopicById(int topicId)
        {
            var handler = new TopicHandlers(_topicRepository, _validator);
            var result = await handler.GetTopicByIdHandler(topicId);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }
    }
}

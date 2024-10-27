using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.TopicSpace;
using Restfulapis_Domain.Abstractions;

namespace DataInsertApi.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IActionResult> CreateTopic(TopicDto topic) {
            var handler = new TopicHandlers(_topicRepository);
            var result = await handler.CreateTopicHandler(topic);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }
    }
}

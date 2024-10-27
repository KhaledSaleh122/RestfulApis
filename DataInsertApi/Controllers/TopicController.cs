using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.TopicSpace;
using Restfulapis_Domain.Abstractions;

namespace DataInsertApi.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly TopicHandlers _topicHandlers;

        public TopicController(TopicHandlers topicHandlers)
        {
            _topicHandlers = topicHandlers ?? throw new ArgumentNullException(nameof(topicHandlers));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTopic(TopicDto topic) {
            var result = await _topicHandlers.CreateTopicHandler(topic);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }
    }
}

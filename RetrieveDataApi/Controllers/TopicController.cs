using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfulApis_Application.TopicSpace;
using Restfulapis_Domain.Abstractions;

namespace RetrieveDataApi.Controllers
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

        [HttpGet("topicId")]
        [Authorize]
        public async Task<IActionResult> GetTopicById(int topicId)
        {
            var result = await _topicHandlers.GetTopicByIdHandler(topicId);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ErrorResult!.StatusCode, result.ErrorResult);
            }
            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTopics([FromQuery] int page = 1, [FromQuery] int pageSize = 10) {
            var result = await _topicHandlers.GetTopicsHandler(page, pageSize);
            return Ok(result.Value);
        }
    }
}

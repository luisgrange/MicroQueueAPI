using MicroQueueAPI.Event;
using Microsoft.AspNetCore.Mvc;

namespace MicroQueueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {

        private readonly RabbitMQEvent _event;

        public QueueController(RabbitMQEvent rabbitMqEvent)
        {
            _event = rabbitMqEvent;
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be null or empty.");
            }

            _event.PublishMessage("microqueue", message);
            return Ok("message sent to RabbitMQ");
        }

    }
}

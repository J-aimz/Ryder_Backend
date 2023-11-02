using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ryder.Application.Messages.Command.SendMessage;
using Ryder.Application.Messages.Command.UpdateMessage;
using Ryder.Application.Messages.Query.GetTheadMessages;

namespace Ryder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ApiController
    {

        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger) => _logger = logger;  



        [HttpGet("GetMessages/{OrderId}")]   
        public async Task<IActionResult> GetMessagesAsync(string OrderId)
        {
            _logger.LogInformation("Get Messages Invoked");
            return await Initiate(() => Mediator.Send(new GetThreadQuery(OrderId)));
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessageAsync([FromBody] MessageDto messageDto)
        {
            _logger.LogInformation($"Sending message {messageDto}");
            return await Initiate(() => Mediator.Send(new SendMessageCommand(messageDto)));
        }

        [HttpPatch("SendEmoji")]
        public async Task<IActionResult> SendEmojieAsync([FromBody] EmojieDto emojieDto )
        {
            _logger.LogInformation($"Sending emojie orderId: {emojieDto.OrderId}, emojie: {emojieDto.Emojie}");
            return await Initiate(() => Mediator.Send(new UpdateMessageCommand(emojieDto)));
        }
    }
}

using DiscordClone.Models.Dtos;
using DiscordClone.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        // POST: api/message/send
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(MessageDto messageDto)
        {
            try
            {
                await _messageService.SendMessageAsync(messageDto);
                return Ok(new { message = "Message sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("reaction/add")]
        public async Task<IActionResult> AddReaction([FromBody] AddReactionDto addReactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _messageService.AddReactionAsync(addReactionDto);
                return Ok(new { message = "Reaction added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("reaction/remove")]
        public async Task<IActionResult> RemoveReaction([FromBody] RemoveReactionDto removeReactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _messageService.RemoveReactionAsync(removeReactionDto);
                return Ok(new { message = "Reaction removed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("send/private")]
        public async Task<IActionResult> SendPrivateMEssage(PrivateMessageDto messageDto)
        {
            try
            {
                await _messageService.SendPrivateMessage(messageDto);
                return Ok(new { message = "Private message sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
            // GET: api/message/{channelId}
       [HttpGet("{channelId}")]
        public async Task<IActionResult> GetAllMessages(Guid channelId)
        {
            return HandleResult(await _messageService.GetAllMessagesAsync(channelId));

        }
        // GET: api/message/{channelId}/last/{days}
        [HttpGet("{channelId}/last/{days}")]
        public async Task<IActionResult> GetMessagesFromLastNDays(Guid channelId, int days)
        {
            var result = await _messageService.GetMessagesFromLastNDays(channelId, days);
            return HandleResult(result);
        }

        [HttpGet("private/{user1}/{user2}/{n}")]
        public async Task<IActionResult> GetPrivateMessagesFromNDays(Guid user1, Guid user2,int n)
        {
            return HandleResult(await _messageService.GetPrivateMessagesFromLastNDays(user1, user2,n));
        }
    }
}

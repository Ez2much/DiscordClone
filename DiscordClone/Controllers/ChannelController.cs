using DiscordClone.Controllers;
using DiscordClone.Models.Dtos;
using DiscordClone.Services.ServerOperations;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : BaseController
    {
        private readonly IChannelOperationsService _channelService;

        public ChannelController(IChannelOperationsService channelService)
        {
            _channelService = channelService;
        }

        // POST: api/channel/create
        [HttpPost("create/{userId}")]
        public async Task<IActionResult> CreateChannel([FromBody] ChannelCreateDto dto, Guid userId)
        {
            var result = await _channelService.CreateChannelAsync(dto, userId);
            if (!result.IsSuccess)
            {
                return BadRequest("Error creating channel.");
            }
            return Ok(result);
        }

        // GET: api/channel/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelById(Guid id)
        {
            var channel = await _channelService.GetChannelByIdAsync(id);
            if (!channel.IsSuccess)
            {
                return NotFound("Channel not found.");
            }
            return Ok(channel);
        }
        [HttpGet("server/{serverId}")]
        public async Task<IActionResult> GetChannelsByServerIdAsync(Guid serverId)
        {
            return HandleResult(await _channelService.GetChannelsByServerIdAsync(serverId));
        }
        // POST: api/channel/{id}
        [HttpPost("delete/{userId}")]
        public async Task<IActionResult> DeleteChannel(ChannelDto dto, [FromQuery] Guid userId)
        {
            var result = await _channelService.DeleteChannelAsync(dto, userId);
            if (!result.IsSuccess)
            {
                return NotFound("Channel not found.");
            }
            return NoContent();
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserChannels(Guid userId)
        {
            return HandleResult(await _channelService.GetUserChannelsGroupNameByUserIdAsync(userId));
        }
    }
}

using DiscordClone.Models.Dtos;
using DiscordClone.Services.ServerOperations;
using Microsoft.AspNetCore.Mvc;


namespace DiscordClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ServerController : BaseController
    {
        private readonly IServerOperationsService _serverOperationsService;

        public ServerController(IServerOperationsService serverOperationsService)
        {
            _serverOperationsService = serverOperationsService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServer([FromBody] ServerCreateDto serverDto)
        {
            var result = await _serverOperationsService.CreateServerAsync(serverDto);
            return HandleResult(result);
        }

        [HttpPost("join/{userId}/{serverId}")]
        public async Task<IActionResult> JoinServer(Guid userId, Guid serverId)
        {
            var result = await _serverOperationsService.JoinServerAsync(userId, serverId);
            return HandleResult(result);
        }

        [HttpPost("leave/{userId}/{serverId}")]
        public async Task<IActionResult> LeaveServer([FromQuery] Guid userId, [FromQuery] Guid serverId)
        {
            var result = await _serverOperationsService.LeaveServerAsync(userId, serverId);
            return HandleResult(result);
        }

        [HttpDelete("delete/{userId}/{serverId}")]
        public async Task<IActionResult> DeleteServer([FromQuery] Guid userId, [FromQuery] Guid serverId)
        {
            var result = await _serverOperationsService.DeleteServerAsync(userId, serverId);
            return HandleResult(result);
        }

        [HttpPost("ban")]
        public async Task<IActionResult> BanUser(ServerBanDto serverban)
        {
            var result = await _serverOperationsService.BanUserAsync(serverban);
            return HandleResult(result);
        }

        [HttpPost("unban")]
        public async Task<IActionResult> UnbanUser([FromBody] UnbanActionDto unbanAction)
        {
            var result = await _serverOperationsService.RemoveBanAsync(unbanAction.ServerId, unbanAction.RemoverId, unbanAction.BannedUserId);
            return HandleResult(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetServersByUserId(Guid userId)
        {
            var result = await _serverOperationsService.GetServersByUserIdAsync(userId);
            return HandleResult(result);
        }
        [HttpGet("{serverId}")]
        public async Task<IActionResult> GetServerById(Guid serverId)
        {
            var result = await _serverOperationsService.GetServerByIdAsync(serverId);
            return HandleResult(result);
        }
        [HttpGet("members/{serverId}")]
        public async Task<IActionResult> GetServerMembers(Guid serverId)
        {
            var result = await _serverOperationsService.GetServerMembersAsync(serverId);
            return HandleResult(result);
        }
    }
}
using System.Runtime.InteropServices;
using DiscordClone.Db;
using DiscordClone.Models;
using DiscordClone.Models.Dtos;
using DiscordClone.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : BaseController
    {
        private readonly FriendshipService _friendshipService;


        public FriendshipController(FriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        // Endpoint to send a friend request
        [HttpPost("send")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto request)
        {
            return HandleResult(await _friendshipService.SendFriendRequestAsync(request.SenderId, request.ReceiverId));

        }
        [HttpPost("send/userName")]
        public async Task<IActionResult> SendFriendRequestByUserName(FriendsUsernameRequestDto friendsUsernameRequest)
        {
            return HandleResult(await _friendshipService.SendFriendRequestByUserName(friendsUsernameRequest));
        }
        // Endpoint to accept a friend request
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendRequestDto request)
        {
            return HandleResult(await _friendshipService.AcceptFriendRequestAsync(request.SenderId, request.ReceiverId));
        }

        // Endpoint to reject a friend request
        [HttpPost("reject")]
        public async Task<IActionResult> RejectFriendRequest([FromBody] FriendRequestDto request)
        {
            return HandleResult(await _friendshipService.RejectFriendRequestAsync(request.SenderId, request.ReceiverId));

        }

        // Endpoint to get a user's friends by their username
        [HttpGet("friends/{userId}")]
        public async Task<IActionResult> GetUserFriendsById(Guid userId)
        {
            return HandleResult(await _friendshipService.GetUserFriendsAsync(userId));
        }
        [HttpGet("requests/{userId}")]
        public async Task<IActionResult> GetUserFriendRequests(Guid userId)
        {
            return HandleResult(await _friendshipService.GetFriendsRequests(userId));
        }
    }
}

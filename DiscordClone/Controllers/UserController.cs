using DiscordClone.Db;
using DiscordClone.Models;
using DiscordClone.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordClone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // UserManager is used to manage user-related actions like creating, finding users, etc.
        private readonly UserManager<User> _userManager;
        // Logger is used for logging information and errors.
        private readonly ILogger<UserController> _logger;
        // Configuration is used to access app settings.
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;

        // Constructor for dependency injection
        public UserController(UserManager<User> userManager, ILogger<UserController> logger, IConfiguration configuration, ApplicationContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        // Endpoint to create a new user
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto registerDto)
        {
            // Check if the input model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(false, "Invalid input data."));
            }

            try
            {
                // Create a new user instance with the provided details
                var user = new User
                {
                    UserName = registerDto.Username,  // Set the username
                    Email = registerDto.Email, // Set the email address
                    CreatedAt = DateTime.UtcNow // Set the creation date
                };

                // Create a new user with the provided password
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded) // If user creation failed, log and return an error response
                {
                    _logger.LogWarning("Failed to create user {Username}: {Errors}", registerDto.Username, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequest(new ApiResponse(false, "Failed to create user.", result.Errors));
                }

                // Generate an email confirmation token for the new user
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Create a link for email confirmation
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new { userId = user.Id, token }, Request.Scheme);
                // Log the confirmation link (in a real scenario, you would send it via email)
                _logger.LogInformation("Confirmation link: {ConfirmationLink}", confirmationLink);

                // Add the user to the specified role
                await _userManager.AddToRoleAsync(user, registerDto.Role);
                return Ok(new ApiResponse(true, "User created successfully. Please confirm your email address.", new { confirmationLink }));
            }
            catch (Exception ex) // Handle any exceptions that occur during user creation
            {
                _logger.LogError(ex, "An error occurred while creating user {Username}", registerDto.Username);
                return StatusCode(500, new ApiResponse(false, "An error occurred while processing your request."));
            }
        }

        [HttpPost("username")]
        public async Task<IActionResult> GetUserByUserName([FromBody] UserNameDto userName)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName.UserName);
                if (user == null)
                {
                    return NotFound(new ApiResponse(false, "User not found"));
                }

                var userRoleRelation = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == user.Id);
                if (userRoleRelation == null)
                {
                    return NotFound(new ApiResponse(false, "User role not found"));
                }

                var userRole = await _context.Roles.FindAsync(userRoleRelation.RoleId);
                if (userRole == null)
                {
                    return NotFound(new ApiResponse(false, "Role not found"));
                }

                return Ok(new ApiResponse(true, "User found successfully", new UserDto
                {
                    Email = user.Email,
                    Id = user.Id.ToString(),
                    Username = user.UserName,
                    Role = userRole.Name,
                    Image = user.AvatarUrl
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse(false, "An error occurred while processing your request."));
            }
        }


        // Endpoint to confirm a user's email address
        [HttpGet("confirmEmail")]
        [AllowAnonymous] // Allow anyone to confirm email, no authentication required
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            // Validate the userId and token
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(new ApiResponse(false, "Invalid email confirmation request."));
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) // If user is not found, return an error response
            {
                return NotFound(new ApiResponse(false, "User not found."));
            }

            // Confirm the user's email using the provided token
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded) // If confirmation fails, return an error response
            {
                return BadRequest(new ApiResponse(false, "Email confirmation failed.", result.Errors));
            }

            return Ok(new ApiResponse(true, "Email confirmed successfully."));
        }

        // Endpoint to get user details by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")] // Admins and Users can get user details
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                // Find the user by ID
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) // If user is not found, log and return an error response
                {
                    _logger.LogWarning("User not found: {UserId}", id);
                    return NotFound(new ApiResponse(false, "User not found."));
                }

                // Return user details
                return Ok(new ApiResponse(true, "User retrieved successfully.", new UserDto { Id = user.Id.ToString(), Username = user.UserName, Email = user.Email }));
            }
            catch (Exception ex) // Handle any exceptions that occur while retrieving user details
            {
                _logger.LogError(ex, "An error occurred while retrieving user {UserId}", id);
                return StatusCode(500, new ApiResponse(false, "An error occurred while processing your request."));
            }
        }

        // Endpoint to update user details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updateUserDto)
        {
            var id = updateUserDto.Id;
            // Check if the input model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(false, "Invalid input data."));
            }

            try
            {
                // Find the user by ID
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) // If user is not found, log and return an error response
                {
                    _logger.LogWarning("User not found: {UserId}", id);
                    return NotFound(new ApiResponse(false, "User not found."));
                }

                // Update user details if provided
                user.UserName = updateUserDto.Username ?? user.UserName;
                user.Email = updateUserDto.Email ?? user.Email;

                // Update the user in the database
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) // If update fails, log and return an error response
                {
                    _logger.LogWarning("Failed to update user {UserId}: {Errors}", id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequest(new ApiResponse(false, "Failed to update user.", result.Errors));
                }

                return Ok(new ApiResponse(true, "User updated successfully."));
            }
            catch (Exception ex) // Handle any exceptions that occur during user update
            {
                _logger.LogError(ex, "An error occurred while updating user {UserId}", id);
                return StatusCode(500, new ApiResponse(false, "An error occurred while processing your request."));
            }
        }

        // Endpoint to delete a user by ID
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                // Find the user by ID
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) // If user is not found, log and return an error response
                {
                    _logger.LogWarning("User not found: {UserId}", id);
                    return NotFound(new ApiResponse(false, "User not found."));
                }

                // Physically delete the user from the database
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded) // If delete operation fails, log and return an error response
                {
                    _logger.LogWarning("Failed to delete user {UserId}: {Errors}", id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequest(new ApiResponse(false, "Failed to delete user.", result.Errors));
                }

                _logger.LogInformation("User {UserId} deleted successfully.", id);
                return Ok(new ApiResponse(true, "User deleted successfully."));
            }
            catch (Exception ex) // Handle any exceptions that occur during user deletion
            {
                _logger.LogError(ex, "An error occurred while deleting user {UserId}", id);
                return StatusCode(500, new ApiResponse(false, "An error occurred while processing your request."));
            }
        }



        [HttpDelete("self")]
        [Authorize] // Każdy zalogowany użytkownik
        public async Task<IActionResult> DeleteOwnAccount()
        {
            try
            {
                // Znajdź aktualnie zalogowanego użytkownika
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _logger.LogWarning("Current user not found.");
                    return NotFound(new ApiResponse(false, "User not found."));
                }

                // Usuń użytkownika z bazy danych
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to delete user {UserId}: {Errors}", user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return BadRequest(new ApiResponse(false, "Failed to delete account.", result.Errors));
                }

                _logger.LogInformation("User {UserId} deleted their own account.", user.Id);
                return Ok(new ApiResponse(true, "Account deleted successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting own account.");
                return StatusCode(500, new ApiResponse(false, "An error occurred while processing your request."));
            }
        }




        [HttpPost("update-avatar")]
        [Authorize] // Make sure only authenticated users can upload avatars
        public async Task<IActionResult> UpdateAvatar([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new ApiResponse(false, "No file selected."));
            }

            try
            {
                // Logic to save the avatar to storage
                var user = await _userManager.GetUserAsync(User); // Get current user
                if (user == null)
                {
                    return NotFound(new ApiResponse(false, "User not found"));
                }

                // Save file logic here (e.g., save to a directory or cloud storage)
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var avatarUrl = $"{Request.Scheme}://{Request.Host}/avatars/{fileName}"; // Full URL to the avatar

                // Update user with new avatar URL
                user.AvatarUrl = avatarUrl; // Assuming User model has AvatarUrl property
                await _userManager.UpdateAsync(user);

                return Ok(new ApiResponse(true, "Avatar updated successfully.", new { avatarUrl }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating avatar");
                return StatusCode(500, new ApiResponse(false, "Error updating avatar"));
            }
        }
    }
}

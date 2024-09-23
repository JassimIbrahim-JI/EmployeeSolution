using BaseLibrary.Class.DTOs;
using BaseLibrary.Class.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserAccount _userAccount) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }
            var result = await _userAccount.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }
            var result = await _userAccount.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshToken token)
        {
            if (token == null)
            {
                return BadRequest("Invalid Request");
            }
            var result = await _userAccount.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()
        {
            var users = await _userAccount.GetUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPut("update-user")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUserAsync(ManageUser user)
        {
            var result = await _userAccount.UpdateUser(user);
            return Ok(result);
        }

        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetRolesAsync()
        {
            var result = await _userAccount.GetRoles();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete-user/{userId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUserAsync(int userId) 
        {
            var result = await _userAccount.DeleteUser(userId);
            return Ok(result);
        }

        [HttpGet("user-image/{id}")]
        [Authorize]
        public async Task<IActionResult>GetUserImage(int id) 
        {
          var result = await _userAccount.GetUserImage(id);
            return Ok(result);
        }
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult>UpdateUserProfile(UserProfile user) 
        {
            var result = await _userAccount.UpdateProfile(user);
            return Ok(result);
        }
    }
}

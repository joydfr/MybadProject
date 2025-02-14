using Microsoft.AspNetCore.Mvc;
using MvcMyBad.Services;
using MvcMyBad.Models;
using MvcMyBad.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MvcMyBad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [SwaggerResponse(200, "Ok!", typeof(List<GetUserDto>))]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
        [HttpPost]
        [SwaggerResponse(200, "Ok!", typeof(PostUserDto))]
        public async Task<ActionResult<PostUserDto>> CreateUserAsync(PostUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var NewUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUsersAsync), NewUser);
        }

    }
}
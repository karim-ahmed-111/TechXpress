// TechXpress.API/Controllers/Customer/ProfileController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechXpress.Business.DTOs;
using Microsoft.AspNetCore.Identity;
using TechXpress.Domain.Entities;
using System.Security.Claims;

namespace TechXpress.API.Controllers.Customer
{
    [ApiController]
    [Route("api/customer/[controller]")]
    [Authorize(Roles = "Customer")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(new UserDto { Id = user.Id, Email = user.Email, FullName = user.FullName });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDto dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (dto.Id != userId) return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FullName = dto.FullName;
                user.Email = dto.Email;
                user.UserName = dto.Email;
                await _userManager.UpdateAsync(user);
                return NoContent();
            }
            return NotFound();
        }
    }
}

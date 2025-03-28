using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechXpress.Data.Entities;
using TechXpress.Data.Repositories;
using TechXpress.Data.UnitOfWork;
using TechXpress.Web.Models;
using TechXpress.Data.DTOs;

namespace TechXpress.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    //Get All Users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.Users.Select(u => new UserDTO
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber
        }).ToListAsync();

        if (users.Count() > 0)
        {

            return Ok(users);
        }
        return Ok("There Are No Users");
    }

    //Get A Specifc User By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {

        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var userDto = new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return Ok(userDto);
        }
        else
        {
            return NotFound("User Not Found");
        }


    }

    //Delete To Delete A Specific User

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {

        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
            return Ok("User Deleted");
        }
        return NotFound();
    }
}

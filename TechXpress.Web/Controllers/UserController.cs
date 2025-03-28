using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechXpress.Data.Entities;
using TechXpress.Data.Repositories;
using TechXpress.Data.UnitOfWork;
using TechXpress.Web.Models;

namespace TechXpress.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUnitOfWork _unit;
    public UserController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    //Get All Users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unit.Users.GetAllAsync();
        if (users.Count() > 0)
        {

            return Ok(users);
        }
        return Ok("There Are No Users");
    }

    //Get A Specifc User By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {

        var user = await _unit.Users.GetByIdAsync(id);
        if (user != null)
        {
            return Ok(user);
        }
        else
        {
            return NotFound();
        }

        //return BadRequest();
    }



    //Post To add users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _unit.Users.AddAsync(user);
        await _unit.CompleteAsync();
        return Ok("User Created");
    }


    //Put to update user details all 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest("Id Doesnt Match User Id");
        }
        var _user = await _unit.Users.GetByIdAsync(id);
        if (_user != null)
        {
            _user.Name = user.Name;
            _user.Email = user.Email;

            _unit.Users.Update(_user);
            await _unit.CompleteAsync();
            return Ok("Details Updated");
        }
        return NotFound("There Is No User With The Corresponding Id");

    }



    //Delete To Delete A Specific User

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        //if (id > 0)
        //{
        var user = await _unit.Users.GetByIdAsync(id);
        if (user != null)
        {
            _unit.Users.Remove(user);
            await _unit.CompleteAsync();
            return Ok("User Deleted");
        }
        return NotFound();
        //}
        //return BadRequest();
    }
}

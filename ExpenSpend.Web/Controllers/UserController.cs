using System;
using System.Threading.Tasks;
using AutoMapper;
using ExpenSpend.Domain.DTOs.Users;
using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers;

[Route("api/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserAppService _userService;
    private readonly IMapper _mapper;
    private readonly UserManager<ESUser> _userManager;

    public UserController(IUserAppService userRepository, IMapper mapper, UserManager<ESUser> userManager)
    {
        _userService = userRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetLoggedInUser()
    {
        var user = await _userService.GetLoggedInUser();
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(_mapper.Map<GetUserDto>(user));
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(_mapper.Map<GetUserDto>(user));
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto input)
    {
        var result = await _userService.UpdateUserAsync(id, input);
        if (result.StatusCode == 201)
        {
            return Ok(result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (result.StatusCode == 204)
        {
            return Ok(result.Message);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
}
﻿using System.Security.Claims;
using AutoMapper;
using ExpenSpend.Core.Users;
using ExpenSpend.Repository.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers.User;

[Route("api/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetLoggedInUser()
    {
        var user = await _userRepository.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(_mapper.Map<GetUserDto>(user));
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("user")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(_mapper.Map<GetUserDto>(user));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserDto input)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        var result = await _userRepository.UpdateUserAsync(_mapper.Map(input, user));
        
        if (result.Succeeded)
        {
            return Ok("User updated successfully");
        }
        return BadRequest(result.Errors);
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        var result = await _userRepository.DeleteUserAsync(user);
        if (result.Succeeded)
        {
            return Ok("User deleted successfully");
        }
        return BadRequest(result.Errors);
    }
}
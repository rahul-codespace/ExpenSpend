using AutoMapper;
using ExpenSpend.Core.DTOs.Users;
using ExpenSpend.Data.Context;
using ExpenSpend.Domain;
using ExpenSpend.Domain.Helpers;
using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Service;

public class UserAppService : IUserAppService
{
    private readonly UserManager<ESUser> _userManager;
    private readonly IExpenSpendRepository<ESUser> _userRepository;
    private readonly IMapper _mapper;
    private readonly ExpenSpendDbContext _context;
    private readonly IHttpContextAccessor _httpContext;

    public UserAppService(UserManager<ESUser> userManager, IExpenSpendRepository<ESUser> userRepository, IMapper mapper, ExpenSpendDbContext expenSpendDbContext, IHttpContextAccessor httpContext)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _mapper = mapper;
        _context = expenSpendDbContext;
        _httpContext = httpContext;
    }

    public async Task<GetUserDto> GetLoggedInUser()
    {
        var result = await _userManager.FindByNameAsync(_httpContext.HttpContext?.User?.Identity?.Name);
        if (result != null)
        {
            return _mapper.Map<GetUserDto>(result);
        }
        return null;
    }
    public async Task<List<GetUserDto>> GetAllUsersAsync()
    {
        return _mapper.Map<List<GetUserDto>>(await _userRepository.GetAllAsync());
    }
    public async Task<ESUser> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    public async Task<ESUser> GetUserByUserNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
    public async Task<ApiResponse<GetUserDto>> UpdateUserAsync(Guid id, UpdateUserDto user)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(id);
        if (userToUpdate == null)
        {
            return new ApiResponse<GetUserDto>
            {
                Message = "User not found",
                StatusCode = 404
            };
        }
        var result = await _userRepository.UpdateAsync(_mapper.Map<ESUser>(userToUpdate));
        if (result == null)
        {
            return new ApiResponse<GetUserDto>
            {
                Message = "Bad Request",
                StatusCode = 400
            };
        }
        return new ApiResponse<GetUserDto>
        {
            Data = _mapper.Map<GetUserDto>(user),
            StatusCode = 201
        };
    }

    public async Task<ApiResponse<GetUserDto>> DeleteUserAsync(Guid id)
    {
        var result = await _userRepository.DeleteAsync(id);
        if (result)
        {
            return new ApiResponse<GetUserDto>
            {
                StatusCode = 204,
                Message = "No Content"
            };
        }
        return new ApiResponse<GetUserDto>
        {
            Message = "User not found",
            StatusCode = 404
        };
    }

    public async Task<ESUser?> GetUserByEmailAsync(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        if (result != null)
        {
            return result;
        }
        return null;
    }
}

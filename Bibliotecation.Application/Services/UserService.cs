using Bibliotecario.Application.Contracts.Interfaces;
using Bibliotecario.Business.Mappers;
using Bibliotecario.Business.Models;
using Bibliotecario.Common.Enums;
using Bibliotecario.DataAccess.Contracts.Entities;
using Bibliotecario.DataAccess.Contracts.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Bibliotecario.Application.Services;

public class UserService : IUserService
{
    #region internals 
    private readonly IGenericRepository<User> _userRepository;
    #endregion internals

    #region constructor
    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion constructor


    #region public methods
    public async Task<GetUserResponseDto> CreateUser(UserRequestDto createUserRequestDto)
    {
        GetUserResponseDto user = new();
        try
        {
            if (!IsModelValid(createUserRequestDto))
                return null;


            var newUser = await _userRepository.New(createUserRequestDto.Map());
            user = newUser?.Map();
        }
        catch (Exception)
        {
            return null;
        }
        return user;
    }

    public async Task<GetUserResponseDto> GetUser(Guid id)
    {
        GetUserResponseDto response = new();
        try
        {
            var user = await _userRepository.Get(u => u.Id.Equals(id));
            response = user?.Map();
        }
        catch (Exception)
        {
            return null;
        }
        return response;
    }

    public async Task<GetUserResponseDto>? GetUser(UserRequestDto getUserRequestDto)
    {
        GetUserResponseDto response = new();
        try
        {
            if (!IsModelValid(getUserRequestDto))
                return null;
            
            var user = await _userRepository.Get(u => u.Identification.Equals(getUserRequestDto.UserIdentification) && u.TypeUser.Equals(getUserRequestDto.UserType));
            response = user?.Map();
        }
        catch (Exception)
        {
            return null;
        }
        return response;
    }
    #endregion public methods

    #region private methods
    private bool IsModelValid(UserRequestDto userRequestDto)
    {
        if (string.IsNullOrEmpty(userRequestDto.UserIdentification) || !Enum.IsDefined(typeof(EUserType), userRequestDto.UserType))
            return false;

        return true;
    }
    #endregion private methods
}

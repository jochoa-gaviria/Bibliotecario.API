
using Bibliotecario.Business.Models;

namespace Bibliotecario.Application.Contracts.Interfaces;

public interface IUserService
{
    Task<GetUserResponseDto> GetUser(UserRequestDto getUserRequestDto);
    Task<GetUserResponseDto> GetUser(Guid id);
    Task<GetUserResponseDto> CreateUser(UserRequestDto createUserRequestDto);
}

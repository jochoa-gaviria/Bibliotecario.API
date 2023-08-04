using Bibliotecario.Business.Models;
using Bibliotecario.DataAccess.Contracts.Entities;

namespace Bibliotecario.Business.Mappers;

public static class UserMapper
{
    public static GetUserResponseDto Map(this User user) =>
        new()
        {
            Id = user.Id,
            Identification = user.Identification,
            UserType = user.UserType,
        };

    public static User Map(this UserRequestDto createUserRequestDto) =>
        new()
        {
            Identification = createUserRequestDto.UserIdentification,
            UserType = createUserRequestDto.UserType,
        };
}

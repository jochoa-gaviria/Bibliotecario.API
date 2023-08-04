using Bibliotecario.Common.Enums;

namespace Bibliotecario.Business.Models;

public class UserRequestDto
{
    public string? UserIdentification { get; set; }
    public EUserType UserType { get; set; }
}

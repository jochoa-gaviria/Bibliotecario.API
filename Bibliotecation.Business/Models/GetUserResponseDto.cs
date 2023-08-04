using Bibliotecario.Common.Enums;

namespace Bibliotecario.Business.Models;

public class GetUserResponseDto
{
    public Guid Id { get; set; }

    public EUserType UserType { get; set; }

    public string? Identification { get; set; }
}

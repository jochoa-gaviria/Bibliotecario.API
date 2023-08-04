
using Bibliotecario.Common.Enums;

namespace Bibliotecario.Common.Constants;

public static class UserTypeConstants
{
    private const string MemberDescription = "Usuario Afiliado";
    private const string EmployeeDescription = "Usuario Empleado de la Biblioteca";
    private const string GuestDescription = "Usuario Invitado";
    public static Dictionary<EUserType, string> UserTypeDescription => new()
    {
        { EUserType.AFILIADO, MemberDescription },
        { EUserType.EMPLEADO, EmployeeDescription },
        { EUserType.INVITADO, GuestDescription }
    };
}

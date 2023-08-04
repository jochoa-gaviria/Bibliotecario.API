using Bibliotecario.Common.Enums;

namespace Bibliotecario.Common.Formatters;

public static class ReturnDateFormatter
{
    public static DateTime CalculateReturnDate(this EUserType userType)
    {
        var weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
        var returnDate = DateTime.Now;
        int loanDays = userType switch
        {
            EUserType.AFILIADO => 10,
            EUserType.EMPLEADO => 8,
            EUserType.INVITADO => 7,
            _ => -1,
        };

        for (int i = 0; i < loanDays;)
        {
            returnDate = returnDate.AddDays(1);
            i = (!weekend.Contains(returnDate.DayOfWeek)) ? ++i : i;
        }

        return returnDate;
    }
}

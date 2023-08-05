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
            EUserType.Member => 10,
            EUserType.Employee => 8,
            EUserType.Guest => 7,
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

using Bibliotecario.Business.Models;
using Bibliotecario.DataAccess.Contracts.Entities;

namespace Bibliotecario.Business.Mappers;

public static class LoanMapper
{
    public static UserRequestDto Map(this CreateLoanRequestDto createLoanRequestDto) =>
        new()
        {
            UserIdentification = createLoanRequestDto.UserIdentification,
            UserType = createLoanRequestDto.UserType,
        };


    public static LoanUser Map(this CreateLoanRequestDto createLoanRequestDto, Guid userId, DateTime returnDate) =>
        new()
        {
            BookId = createLoanRequestDto.BookId,
            LoanDate = DateTime.Now,
            UserId = userId,
            ReturnDate = returnDate
        };

    public static CreateLoanResponseDto Map(this LoanUser loanUser) =>
        new()
        {
            LoanId = loanUser.Id,
            ReturnDate = loanUser.ReturnDate.ToShortDateString(),
        };

    public static GetLoanResponseDto Map(this LoanUser loanUser, GetUserResponseDto user) =>
        new()
        {
            Id = loanUser.Id,
            BookId = loanUser.BookId,
            UserIdentification = user.Identification,
            UserType = user.UserType,
            ReturnDate = loanUser.ReturnDate
        };
}

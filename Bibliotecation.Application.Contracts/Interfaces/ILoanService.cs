using Bibliotecario.Business.Models;
using Bibliotecario.Common.Models;

namespace Bibliotecario.Application.Contracts.Interfaces;

public interface ILoanService
{
    /// <summary>
    /// Permite crear un presamo de un libro
    /// </summary>
    /// <param name="createLoanRequestDto"></param>
    /// <returns cref="CreateLoanResponseDto"></returns>
    Task<ResponseDto<CreateLoanResponseDto>> CreateLoan(CreateLoanRequestDto createLoanRequestDto);

    /// <summary>
    /// Permite obtener los datos de un prestamo
    /// </summary>
    /// <param name="loanId"></param>
    /// <returns cref="GetLoanResponseDto"></returns>
    Task<ResponseDto<GetLoanResponseDto>> GetLoan(string loanId);

}

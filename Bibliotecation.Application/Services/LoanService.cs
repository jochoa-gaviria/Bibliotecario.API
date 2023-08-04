using Bibliotecario.Application.Contracts.Interfaces;
using Bibliotecario.Business.Mappers;
using Bibliotecario.Business.Models;
using Bibliotecario.Common.Enums;
using Bibliotecario.Common.Formatters;
using Bibliotecario.Common.Models;
using Bibliotecario.Common.Resources;
using Bibliotecario.DataAccess.Contracts.Entities;
using Bibliotecario.DataAccess.Contracts.Interfaces;
using System.Net;

namespace Bibliotecario.Application.Services;

public class LoanService : ILoanService
{
    #region internals 
    private readonly IUserService _userService;
    private readonly IGenericRepository<LoanUser> _loanRepository;
    #endregion internals

    #region constructor
    public LoanService(IUserService userService, IGenericRepository<LoanUser> loanRepository)
    {
        _userService = userService;
        _loanRepository = loanRepository;
    }
    #endregion constructor


    #region public methods
    /// <summary>
    /// Permite crear un presamo de un libro
    /// </summary>
    /// <param name="createLoanRequestDto"></param>
    /// <returns cref="CreateLoanResponseDto"></returns>
    public async Task<ResponseDto<CreateLoanResponseDto>> CreateLoan(CreateLoanRequestDto createLoanRequestDto)
    {
        ResponseDto<CreateLoanResponseDto> response = new();
        try
        {
            if (!Enum.IsDefined(typeof(EUserType), createLoanRequestDto.UserType))
            {
                response.SetResponse(HttpStatusCode.BadRequest, MessageResource.ErrorInvalidUserType);
                return response;
            }

            var user = await _userService.GetUser(createLoanRequestDto.Map());
            if (user == null)
            {
                user = await _userService.CreateUser(createLoanRequestDto.Map());
            }

            int loansCountByUser = await _loanRepository.Count(l => l.UserId.Equals(user.Id));
            if (user.UserType == EUserType.INVITADO && loansCountByUser >= 1)
            {
                response.SetResponse(HttpStatusCode.BadRequest, string.Format(MessageResource.ErrorUserAlreadyHasLeading, user.Identification));
                return response;
            }

            DateTime returnDate = CalculateReturnDate(user.UserType);
            var loanResult = await _loanRepository.New(createLoanRequestDto.Map(user.Id, returnDate));

            response.Data = loanResult.Map();
        }
        catch (Exception)
        {
            response.SetResponse(HttpStatusCode.InternalServerError, MessageResource.InternalServerError);
            return response;
        }

        return response;
    }

    /// <summary>
    /// Permite obtener los datos de un prestamo
    /// </summary>
    /// <param name="loanId"></param>
    /// <returns cref="GetLoanResponseDto"></returns>
    public async Task<ResponseDto<GetLoanResponseDto>> GetLoan(string loanId)
    {
        ResponseDto<GetLoanResponseDto> response = new();
        try
        {
            if (!Guid.TryParse(loanId, out Guid id))
            {
                response.SetResponse(HttpStatusCode.BadRequest, MessageResource.BadRequestMessage);
                return response;
            }

            var loan = await _loanRepository.Get(l => l.Id.Equals(id));
            if (loan == null)
            {
                response.SetResponse(HttpStatusCode.NotFound, string.Format(MessageResource.ErrorNotFoundLoan, id));
                return response;
            }

            var user = await _userService.GetUser(loan.UserId);
            if (user == null)
            {
                response.SetResponse(HttpStatusCode.BadRequest, string.Format(MessageResource.ErrorUserNotFound, loan.UserId));
                return response;
            }

            response.Data = loan.Map(user);
        }
        catch (Exception ex)
        {
            response.SetResponse(HttpStatusCode.InternalServerError, MessageResource.InternalServerError);
            return response;
        }
        return response;
    }
    #endregion public methods

    #region private methods

    private DateTime CalculateReturnDate(EUserType userType)
    {
        return ReturnDateFormatter.CalculateReturnDate(userType);
    }
    #endregion private methods
}

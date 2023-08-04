using Bibliotecario.Application.Contracts.Interfaces;
using Bibliotecario.Business.Models;
using Bibliotecario.Common.Extensions;
using Bibliotecario.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PrestamoController : ControllerBase
    {
        #region internals
        private readonly ILoanService _loanService;
        #endregion internals

        #region constructor
        public PrestamoController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        #endregion constructor

        #region Api's

        [HttpPost]
        [ProducesResponseType(typeof(CreateLoanResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateLoanRequestDto createLoanRequestDto)
        {
            var response = await _loanService.CreateLoan(createLoanRequestDto);
            return Result.Execute(response);
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetLoanResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
        [Route("{loanId}")]
        public async Task<IActionResult> Get([FromRoute] string loanId)
        {
            var response = await _loanService.GetLoan(loanId);
            return Result.Execute(response);
        }

        #endregion Api's

    }
}

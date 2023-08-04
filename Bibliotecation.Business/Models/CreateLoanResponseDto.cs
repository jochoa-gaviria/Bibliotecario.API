using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Bibliotecario.Business.Models;

public class CreateLoanResponseDto
{
    [JsonPropertyName("id")]
    public Guid LoanId { get; set; }

    [JsonPropertyName("fechaMaximaDevolucion")]
    public string? ReturnDate { get; set; }
}

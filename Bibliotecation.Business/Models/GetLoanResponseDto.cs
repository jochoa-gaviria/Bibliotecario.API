using Bibliotecario.Common.Enums;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Bibliotecario.Business.Models;

public class GetLoanResponseDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("isbn")]
    public Guid BookId { get; set; }

    [JsonPropertyName("identificacionUsuario")]
    public string? UserIdentification { get; set; }

    [JsonPropertyName("tipoUsuario")]
    public EUserType UserType { get; set; }

    [JsonPropertyName("fechaMaximaDevolucion")]
    public DateTime ReturnDate { get; set; }
}

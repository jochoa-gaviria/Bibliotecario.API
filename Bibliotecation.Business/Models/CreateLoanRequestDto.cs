using Bibliotecario.Common.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bibliotecario.Business.Models;

public class CreateLoanRequestDto
{
    [Required]
    [JsonPropertyName("Isbn")]
    public Guid BookId { get; set; }

    [Required]
    [JsonPropertyName("IdentificacionUsuario")]
    [MaxLength(10)]
    public string? UserIdentification { get; set; }

    [Required]
    [JsonPropertyName("TipoUsuario")]
    [Range(1, 9)]
    public EUserType UserType { get; set; }
}

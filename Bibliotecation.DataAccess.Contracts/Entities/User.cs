using Bibliotecario.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotecario.DataAccess.Contracts.Entities;

[Table("TBL_USERS")]
public class User
{
    #region properties
    [Key, Column("GUID")]
    public Guid Id { get; set; }

    [Column("USER_TYPE"), ForeignKey(nameof(TypeUser))]
    public EUserType UserType { get; set; }

    [Column("IDENTIFICATION")]
    public string? Identification { get; set; }
    #endregion properties

    #region relationships
    public UserType? TypeUser { get; set; }
    public ICollection<LoanUser>? LoanUsers { get; set; }
    #endregion relationships

}

using Bibliotecario.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotecario.DataAccess.Contracts.Entities;

[Table("TBL_TYPE_USERS")]
public class UserType
{
    #region properties
    [Key, Column("TYPE")]
    public EUserType Type { get; set; }

    [Column("DESCRIPTION")]
    public string? Description { get; set; }
    #endregion properties

    #region relationships
    public virtual ICollection<User>? Users { get; set; }
    #endregion relationships
}

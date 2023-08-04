using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotecario.DataAccess.Contracts.Entities;

[Table("TBL_LOAN_USER")]
public class LoanUser
{
    #region properties
    [Key, Column("GUID")]
    public Guid Id { get; set; }

    [Column("USER_ID"), ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [Column("DATE_LOAN")]
    public DateTime LoanDate { get; set; }

    [Column("BOOK_ID")]
    public Guid BookId { get; set; }

    [Column("DATE_RETURN")]
    public DateTime ReturnDate { get; set; }
    #endregion properties

    #region relationships
    public User User { get; set; }
    #endregion relationships
}


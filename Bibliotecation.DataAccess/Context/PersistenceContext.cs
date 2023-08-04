using Bibliotecario.Common.Constants;
using Bibliotecario.Common.Enums;
using Bibliotecario.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bibliotecario.DataAccess.Context;

public class PersistenceContext : DbContext
{
    #region internals
    private readonly IConfiguration Config;

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserType> UserTypes { get; set; }
    public virtual DbSet<LoanUser> Loans { get; set; }

    #endregion internals

    #region constructor
    public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
    {
        Config = config;
    }
    #endregion constructor

    #region methods

    public async Task CommitAsync()
    {
        await SaveChangesAsync().ConfigureAwait(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Config.GetSection("SchemaName").Value);
        modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<LoanUser>().Property(p => p.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<UserType>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserType>().HasData(GenerateUserTypeData());

        base.OnModelCreating(modelBuilder);
    }


    private List<UserType> GenerateUserTypeData()
    {
        return new List<UserType>
        {
            new UserType
            {
                Type = EUserType.Member,
                Description = UserTypeConstants.UserTypeDescription[EUserType.Member]
            },
            new UserType
            {
                Type = EUserType.Employee,
                Description = UserTypeConstants.UserTypeDescription[EUserType.Employee]
            },
            new UserType
            {
                Type = EUserType.Guest,
                Description = UserTypeConstants.UserTypeDescription[EUserType.Guest]
            }
        };
    }
    #endregion methods
}
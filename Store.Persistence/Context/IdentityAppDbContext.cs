using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.User;

namespace Store.Persistence.Context;

public class IdentityAppDbContext : IdentityDbContext<User>
{
    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<IdentityUser<string>>().ToTable("Users", "identity");
        builder.Entity<IdentityRole<string>>().ToTable("Roles", "identity");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "identity");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "identity");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "identity");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "identity");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "identity");
        
        builder.Entity<IdentityUserToken<string>>()
            .HasKey(p => new { p.UserId, p.LoginProvider, p.Name });
        builder.Entity<IdentityUserLogin<string>>()
            .HasKey(p => new{p.LoginProvider,p.ProviderKey});
        builder.Entity<IdentityUserRole<string>>()
            .HasKey(p => new { p.UserId, p.RoleId });
        // base.OnModelCreating(builder);
    }
}
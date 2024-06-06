using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.AppDbContextInterfaces;
using Store.Domain.Entities.User;

namespace Store.Persistence.Context;

public class AppDbContext : DbContext,IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
}
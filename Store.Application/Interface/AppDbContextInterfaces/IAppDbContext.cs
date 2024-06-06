using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.User;

namespace Store.Application.Interface.AppDbContextInterfaces;

public interface IAppDbContext
{
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken());

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    
}
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Services;

public interface IVMMDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task RunInTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);
}
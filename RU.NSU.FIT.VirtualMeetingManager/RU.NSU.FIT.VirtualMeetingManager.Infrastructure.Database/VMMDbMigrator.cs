using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager;

public class VMMDbMigrator : IVMMDbMigrator
{
    private readonly VMMDbContext _dbContext;
    
    public VMMDbMigrator(VMMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Migrate()
    {
        _dbContext.Database.Migrate();
    }

    public void EnsureDatabaseDeleted()
    {
        _dbContext.Database.EnsureDeleted();
    }
}
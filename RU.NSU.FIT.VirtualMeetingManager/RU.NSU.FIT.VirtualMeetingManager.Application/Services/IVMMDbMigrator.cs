namespace RU.NSU.FIT.VirtualMeetingManager.Application.Services;

public interface IVMMDbMigrator
{
    void Migrate();
    void EnsureDatabaseDeleted();
}
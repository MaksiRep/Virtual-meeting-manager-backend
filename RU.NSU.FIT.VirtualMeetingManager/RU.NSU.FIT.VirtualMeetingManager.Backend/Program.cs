using RU.NSU.FIT.VirtualMeetingManager;
using RU.NSU.FIT.VirtualMeetingManager.Backend;

var app = WebApplication
    .CreateBuilder(args)
    .AddServices()
    .Build();

app.MigrateDatabase();

app.ConfigureApp();

app.Run();
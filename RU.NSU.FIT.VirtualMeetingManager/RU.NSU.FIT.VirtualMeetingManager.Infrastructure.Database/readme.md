# Работа с миграциями

## Как добавить миграцию

cd `.\RU.NSU.FIT.VirtualMeetingManager.Infrastructure.Database`


run `dotnet ef migrations add MigrationName -s ..\RU.NSU.FIT.VirtualMeetingManager.Backend --context VMMDbContext`

## Как удалить миграцию

cd `.\RU.NSU.FIT.VirtualMeetingManager.Infrastructure.Database`

run `dotnet ef migrations remove -s ..\RU.NSU.FIT.VirtualMeetingManager.Backend --context VMMDbContext`

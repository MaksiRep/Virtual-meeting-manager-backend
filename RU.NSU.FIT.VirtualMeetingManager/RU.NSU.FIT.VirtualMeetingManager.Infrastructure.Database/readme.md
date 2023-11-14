# Работа с миграциями

## Как установить Entity Framework

1) Скачать dotnet
2) Установить `dotnet tool install --global dotnet-ef`

## Как добавить миграцию

cd `.\RU.NSU.FIT.VirtualMeetingManager.Infrastructure.Database`

run `dotnet ef migrations add MigrationName -s ..\RU.NSU.FIT.VirtualMeetingManager.Backend --context VMMDbContext`

## Как удалить миграцию

cd `.\RU.NSU.FIT.VirtualMeetingManager.Infrastructure.Database`

run `dotnet ef migrations remove -s ..\RU.NSU.FIT.VirtualMeetingManager.Backend --context VMMDbContext`

## Как применить миграцию

run `dotnet ef database update  -s ..\RU.NSU.FIT.VirtualMeetingManager.Backend --context VMMDbContext`
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore RU.NSU.FIT.VirtualMeetingManager.sln
WORKDIR /src/RU.NSU.FIT.VirtualMeetingManager.Backend
RUN dotnet build -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
ENV ASPNETCORE_URLS="http://+:8080"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RU.NSU.FIT.VirtualMeetingManager.Backend.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ScheduleManager.Api/*.csproj ./ScheduleManager.Api/
COPY ScheduleManager.Data/*.csproj ./ScheduleManager.Data/
COPY ScheduleManager.Domain/*.csproj ./ScheduleManager.Domain/
COPY ScheduleManager.Localizations/*.csproj ./ScheduleManager.Localizations/
COPY ScheduleManager.Authentication/*.csproj ./ScheduleManager.Authentication/
RUN dotnet restore

# copy everything else and build app
COPY ScheduleManager.Api/. ./ScheduleManager.Api/
COPY ScheduleManager.Data/. ./ScheduleManager.Data/
COPY ScheduleManager.Domain/. ./ScheduleManager.Domain/
COPY ScheduleManager.Localizations/. ./ScheduleManager.Localizations/
COPY ScheduleManager.Authentication/. ./ScheduleManager.Authentication/
WORKDIR /app/ScheduleManager.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/ScheduleManager.Api/out ./
ENTRYPOINT ["dotnet", "ScheduleManager.Api.dll"]
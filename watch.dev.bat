@echo off
setlocal

cd ScheduleManager.Api
set ASPNETCORE_ENVIRONMENT=Development
explorer "http://localhost:5000/"
dotnet watch run

endlocal
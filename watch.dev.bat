@echo off
setlocal

cd ScheduleManager.Api
set ASPNETCORE_ENVIRONMENT="Development"
dotnet watch run

endlocal
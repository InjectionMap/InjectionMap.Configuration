@echo off

echo Begin create nuget for InjectionMap.Configuration

NuGet.exe pack ..\src\InjectionMap.Configuration.nuspec

echo End create nuget for InjectionMap.Configuration

pause
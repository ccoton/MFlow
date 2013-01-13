@echo off
powershell.exe -ExecutionPolicy ByPass -File buildscripts\set-build-version.ps1
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe buildscripts\build.proj %*
rd /s /q .build
pause
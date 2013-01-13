@echo off
powershell.exe -ExecutionPolicy ByPass -File buildscripts\set-release-version.ps1
path=%path%;C:\Program Files (x86)\Git\cmd
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe buildscripts\release.build.proj %*
rd /s /q .build
pause
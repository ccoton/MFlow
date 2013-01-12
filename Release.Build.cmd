@echo off

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe buildscripts\release.build.proj %*
pause
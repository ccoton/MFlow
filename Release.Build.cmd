@echo off
rd /s /q .build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe buildscripts\release.build.proj %*
pause
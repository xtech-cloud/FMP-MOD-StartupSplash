
@echo off

REM !!! Generated by the fmp-cli 1.83.0.  DO NOT EDIT!

md StartupSplash\Assets\3rd\fmp-xtc-startupsplash

cd ..\vs2022
dotnet build -c Release

copy fmp-xtc-startupsplash-lib-mvcs\bin\Release\netstandard2.1\*.dll ..\unity2021\StartupSplash\Assets\3rd\fmp-xtc-startupsplash\

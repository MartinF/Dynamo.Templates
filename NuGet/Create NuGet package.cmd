NuGet.exe update -self

msbuild.exe ..\Dynamo.Templates.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

NuGet.exe pack Dynamo.Templates.Core.nuspec
NuGet.exe pack Dynamo.Templates.nuspec

pause
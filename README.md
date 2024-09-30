# fullstack-dotnet
dotnet --list-sdks

dotnet new globaljson

dotnet new classlib -o Dima.core
dotnet new web -o Dima.Api     
dotnet sln add ./Dima.Api

dotnet add package Microsoft.AspNetCore.OpenApi

dotnet add package Swashbuckle.AspNetCore

dotnet add reference ../Dima.Core/


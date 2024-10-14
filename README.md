# fullstack-dotnet
dotnet --list-sdks

dotnet new globaljson

dotnet new classlib -o Dima.core
dotnet new web -o Dima.Api     
dotnet sln add ./Dima.Api

dotnet add package Microsoft.AspNetCore.OpenApi

dotnet add package Swashbuckle.AspNetCore

dotnet add reference ../Dima.Core/

### Migrations
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef


dotnet add package  Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add v1

dotnet ef database update

### db context
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Design

### Identiy Framework
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    
### RBAC -> Role-based access control
`Role`: perfil

`Claims`: afirmações/ política de acesso

### secrets

dotnet user-secrets init
dotnet user-secrets set "" ""
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=dima-dev;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True"

user password: P@$$w0rd!;


## image docker Mac M1 SQL Server
docker pull mcr.microsoft.com/azure-sql-edge

https://balta.io/blog/sql-server-docker

docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sqlserver mcr.microsoft.com/azure-sql-edge


### Denpendency Injection
`Scoped`: Garante que só existe uma instancia por requisição, reutilizando em vários handlers
`Transient`: Sempre cria uma nova instancia da depedência
`Singleton`: Somente uma instancia por aplicação
``

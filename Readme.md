## Dotnet commands

_Create project:_

        dotnet new angular -o project-name

_Run project:_
        
        dotnet watch run

_Install all the basic dependences:_

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    dotnet add package System.IdentityModel.Tokens.Jwt
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Npgsql
    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
    dotnet add package EFCore.NamingConventions
    dotnet add package Microsoft.IdentityModel.Tokens
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    dotnet add package Microsoft.AspNetCore.SignalR
    
_Install all the required dependences for GraphQL:_

    dotnet add package GraphQl --version 2.4.0
    dotnet add package GraphQL.Server.Transports.AspNetCore --version 3.4.0
    dotnet add package GraphQL.Server.Ui.Playground
    
_Start GraphQL PlayGround in:_
    
    https://localhost:5001/ui/playground
    
    
## Migrations commands

_Install migrations tool:_ 

        dotnet tool install --global dotnet-ef

_Create a new migration with the unmigrated changes:_ 

        dotnet ef migrations add InitialCreate

 _Update migrations in the DB:_ 

        dotnet ef database update

## Angular commands

_Install angular:_ 

        npm install -g @angular/cli en la carpeta ClientApp

_Create a new angular component:_ 

        ng g component component-name --skip-import

_Create a new angular service:_ 

        ng g component service-name

## Apollo commands

_Setup Apollo running these commands in the ClientApp directory:_

    npm install -g npm-check-updates
    ncu -u
    npm install
    npm install typescript@">=4.0.0 and <4.1.0" --save-dev
    ng add apollo-angular
    
## Git commands

_Create a new branch:_

        git checkout -b branch-name

_Stage all changes:_

        git add --all

_Commit the staged changes:_
 
        git commit -am "message"

_Push to the repository:_

        git push -u origin branch-name

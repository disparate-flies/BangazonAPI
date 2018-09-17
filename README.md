# BangazonAPI
clone repo
create a new repo
git init
install dotnet
install dapper
dotnet new webapi -o [project name]
dotnet new sln -o [project name]
cd [project name]
dotnet sln [project name].sln add [project name].csproj
dotnet add package Dapper

open SQL Server Managment Studio
Steps for Setting Up a Database in SQL Management Server: 

Right click Databases and select New Database...
Enter the name of your database Bangazon
With database clicked and highlighted, click New Query and copy and paset SQL code from DFBangazon.sql
In appsettings.json in C# file, copy and paste the following code: 
"ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-U33P79O\\SQLEXPRESS;Database=nssexercises;Trusted_Connection=True;"
}

Open Postman and test following commands:


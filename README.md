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

Open Postman and test following commands
type dotnet run in your powershell to run the program
you can replace product with any other table in the database. product is being used as an example.

to get:
type http://localhost:5000/api/product
select Get method
hit run
this should return a list of products.
to post:
type http://localhost:5000/api/product
select post method
click "body" and "raw"
paste product object

{
    "price": 4,
    "title":" banana", 
    "proddesc": "food",
    "quantity": 5,
    "productTypeId": 1,
    "sellerId": 1
}
hit run
run get again to make sure the post went through.
to put:
select a product to edit
type http://localhost:5000/api/product/{id}
select put
click "body" and "raw"
paste object to edit in this section

{
    "price": 4,
    "title":" apple", 
    "proddesc": "food",
    "quantity": 5,
    "productTypeId": 1,
    "sellerId": 1
}
hit run
run get again to make sure the put went through.
to delete:
type http://localhost:5000/api/product/{id}
select delete
hit run
run get again to make sure the delete went through

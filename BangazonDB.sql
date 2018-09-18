--ALTER TABLE Employee DROP CONSTRAINT [FK_Department];
--ALTER TABLE Product DROP CONSTRAINT [FK_ProductType];
--ALTER TABLE Product DROP CONSTRAINT [FK_Seller];
--ALTER TABLE PaymentType DROP CONSTRAINT [FK_Customer];
--ALTER TABLE EmployeeTraining DROP CONSTRAINT [FK_Employee];
--ALTER TABLE EmployeeTraining DROP CONSTRAINT [FK_TrainingProgram];
--ALTER TABLE EmployeeComputer DROP CONSTRAINT [FK_Employee_Computer];
--ALTER TABLE EmployeeComputer DROP CONSTRAINT [FK_Computer];
--ALTER TABLE ProductOrder DROP CONSTRAINT [FK_Product];
--ALTER TABLE ProductOrder DROP CONSTRAINT [FK_Orders];
--ALTER TABLE Orders DROP CONSTRAINT [FK_Customer_Order];
--ALTER TABLE Orders DROP CONSTRAINT [FK_PaymentType];

--delete from ProductOrder;
--delete from Orders;
--delete from PaymentType;
--delete from Customer;
--delete from Product;
--delete from ProductType;
--delete from EmployeeComputer;
--delete from Computer;
--delete from EmployeeTraining;
--delete from TrainingProgram;
--delete from Employee;
--delete from Department;

--drop table if exists Department;
--drop table if exists Employee;
--drop table if exists TrainingProgram;
--drop table if exists EmployeeTraining;
--drop table if exists Computer;
--drop table if exists EmployeeComputer;
--drop table if exists ProductType;
--drop table if exists Product;
--drop table if exists Customer;
--drop table if exists PaymentType;
--drop table if exists Orders;
--drop table if exists ProductOrder;
	
CREATE TABLE Department (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
DeptName	varchar(80) NOT NULL,
ExpenseBudget integer not null
);
					
Insert into Department 
(DeptName, ExpenseBudget)
select 'Alpha',
400000;
		
Insert into Department
(DeptName, ExpenseBudget)
select 'Bravo',
342000;

Insert into Department
(DeptName, ExpenseBudget)
select 'Charlie',
538000;

Insert into Department
(DeptName, ExpenseBudget)
select 'Delta',
619000;
	
CREATE TABLE Employee (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
FirstName	varchar(80) NOT NULL,
LastName	varchar(80) NOT NULL,
IsSupervisor	bit not null,
DepartmentId integer not null,
IsActive	bit not null
Constraint FK_Department FOREIGN KEY(DepartmentId) REFERENCES Department(Id)
);
					
Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Emp',
'One',
1,
d.Id,
1
from Department d where d.DeptName = 'Alpha';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Person',
'Two',
0,
d.Id,
1
from Department d where d.DeptName = 'Alpha';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select'Man',
'Three',
0,
d.Id,
1
from Department d where d.DeptName = 'Alpha';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Boss',
'Woman',
1,
d.Id,
1
from Department d where d.DeptName = 'Bravo';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Lady',
'Four',
0,
d.Id,
0
from Department d where d.DeptName = 'Bravo';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'John',
'Doe',
0,
d.Id,
1
from Department d where d.DeptName = 'Bravo';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Man',
'Five',
1,
d.Id,
1
from Department d where d.DeptName = 'Charlie';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Dude',
'Six',
0,
d.Id,
0
from Department d where d.DeptName = 'Charlie';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Jane',
'Doe',
0,
d.Id,
1
from Department d where d.DeptName = 'Charlie';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Chick',
'Seven',
1,
d.Id,
1
from Department d where d.DeptName = 'Delta';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Mister',
'Eight',
0,
d.Id,
1
from Department d where d.DeptName = 'Delta';

Insert into Employee
(FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
select 'Madam',
'Nine',
0,
d.Id,
1
from Department d where d.DeptName = 'Delta';
					
CREATE TABLE TrainingProgram (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
ProgName	varchar(80) NOT NULL,
StartDate	varchar(80) NOT NULL,
EndDate	varchar(80) not null,
MaxAttendees integer not null
);

Insert into TrainingProgram
(ProgName, StartDate, EndDate, MaxAttendees)
select 'New Hire', '9/1/18', '9/15/18', 20
;

Insert into TrainingProgram
(ProgName, StartDate, EndDate, MaxAttendees)
select 'Sales', '8/1/18', '8/15/18', 8
;

Insert into TrainingProgram
(ProgName, StartDate, EndDate, MaxAttendees)
select 'Six Sigma', '7/1/18', '8/15/18', 15
;

Insert into TrainingProgram
(ProgName, StartDate, EndDate, MaxAttendees)
select 'Diversity', '10/1/18', '10/15/18', 50
;
					
CREATE TABLE EmployeeTraining (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
EmployeeId	integer NOT NULL,
TrainingProgramId	integer NOT NULL,
Constraint FK_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
Constraint FK_TrainingProgram FOREIGN KEY(TrainingProgramId) REFERENCES TrainingProgram(Id)
);

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Mister' and tp.ProgName = 'Diversity'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Chick' and tp.ProgName = 'Diversity'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Dude' and tp.ProgName = 'Diversity'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Lady' and tp.ProgName = 'New Hire'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Man' and tp.ProgName = 'New Hire'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Boss' and tp.ProgName = 'Sales'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'John' and tp.ProgName = 'Six Sigma'
;

Insert into EmployeeTraining
(EmployeeId, TrainingProgramId)
select e.Id, tp.Id
from Employee e, TrainingProgram tp
where e.FirstName = 'Madam' and tp.ProgName = 'Six Sigma'
;
					
CREATE TABLE Computer (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
PurchaseDate	varchar(80) NOT NULL,
Model	varchar(80) not null,
DecommissionDate	varchar(80),
Condition	varchar(80) not null
);
					
Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '7/1/18', 'Apple', null, 'new';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '7/1/18', 'Apple', null, 'new';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '7/1/18', 'Apple', null, 'new';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '3/3/16', 'Apple', null, 'good';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '3/3/16', 'Apple', null, 'good';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '3/3/16', 'Apple', null, 'good';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '9/13/17', 'HP', null, 'good';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '9/13/17', 'HP', null, 'good';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '9/13/17', 'HP', null, 'good';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '2/13/15', 'Dell', null, 'fair';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '2/13/15', 'Dell', null, 'fair';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '2/13/15', 'Dell', null, 'fair';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '12/12/12', 'Dell', '6/21/17', 'decommissioned';

Insert into Computer
(PurchaseDate, Model, DecommissionDate, Condition)
select '1/22/13', 'HP', '5/18/17', 'decommissioned';
					
CREATE TABLE EmployeeComputer (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
EmployeeId	integer NOT NULL,
ComputerId	integer NOT NULL,
DateAssigned		varchar(80) not null,
DateTurnedIn		varchar(80),
Constraint FK_Employee_Computer FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
Constraint FK_Computer FOREIGN KEY(ComputerId) REFERENCES Computer(Id)
);
					
Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 1, 1, c.PurchaseDate, null
from Computer c where c.Id = 1;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 2, 2, c.PurchaseDate, null
from Computer c where c.Id = 2;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 3, 3, c.PurchaseDate, null
from Computer c where c.Id = 3;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 4, 4, c.PurchaseDate, null
from Computer c where c.Id = 4;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 5, 5, c.PurchaseDate, null
from Computer c where c.Id = 5;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 6, 6, c.PurchaseDate, null
from Computer c where c.Id = 6;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 7, 7, c.PurchaseDate, null
from Computer c where c.Id = 7;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 8, 8, c.PurchaseDate, null
from Computer c where c.Id = 8;

Insert into EmployeeComputer
(EmployeeId, ComputerId, DateAssigned, DateTurnedIn)
select 9, 9, c.PurchaseDate, null
from Computer c where c.Id = 9;

Create table Customer (
Id		integer not null primary key IDENTITY,
FirstName		varchar(80) not null,
LastName		varchar(80) not null,
AccountCreated		varchar(80) not null,
LastLogin		varchar(80)
);

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'April', 'Watson', '1/19/18', null;

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'Larry', 'King', '7/25/18', null;

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'Kenya', 'Stevens', '2/13/17', null;

Insert into Customer
(FirstName, LastName, AccountCreated, LastLogin)
select 'Kenneth', 'Burnett', '6/13/17', null;
					
Create table ProductType (
Id	integer not null primary key IDENTITY,
Name	varchar(80) not null
);

Insert into ProductType
(Name)
select 'Toys';

Insert into ProductType 
(Name)
select 'Home & Garden';

Insert into ProductType
(Name) 
select 'Health & Beauty';

Insert into ProductType
(Name) 
select 'Food';

CREATE TABLE Product (
Id	integer NOT NULL PRIMARY KEY IDENTITY,
ProductTypeId	integer NOT NULL,
SellerId	integer not null,	
Price	integer NOT NULL,
Title		varchar(80) not null,
ProdDesc		varchar(80) not null,
Quantity		integer not null,
Constraint FK_ProductType FOREIGN KEY(ProductTypeId) REFERENCES ProductType(Id),
Constraint FK_Seller Foreign Key(SellerId) References Customer(Id)
);
					
Insert into Product
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity) 
select pt.Id, 2, 5, 'Teddy Bear', 'fluffy & cuddly', 582
from ProductType pt where pt.Name = 'Toys';

Insert into Product
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity) 
select pt.Id, 2, 3, 'Dove Beauty Bar', 'gentle cleanser for skin', 1980
from ProductType pt where pt.Name = 'Health & Beauty';

Insert into Product 
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity)
select pt.Id, 2, 8, 'Pantene Gold Series Shampoo', 'hair nourishment', 750
from ProductType pt where pt.Name = 'Health & Beauty';

Insert into Product 
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity) 
select pt.Id, 2, 2, 'Chiquita Bananas', 'imported from Mexico', 750
from ProductType pt where pt.Name = 'Food';

Insert into Product 
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity)
select pt.Id, 2, 15, 'Jumbo Frozen Shrimp', 'Pacific Wild Caught, 2lb bag', 827
from ProductType pt where pt.Name = 'Food';

Insert into Product 
(ProductTypeId, SellerId, Price, Title, ProdDesc, Quantity) 
select pt.Id, 2, 4, 'Lysol Wipes', 'bathroom and kitchen cleaner', 1769
from ProductType pt where pt.Name = 'Home & Garden';
					
Create table PaymentType (
Id		integer not null primary key IDENTITY,
AccountNo		integer not null,
AccType		varchar(80) not null,
Nickname		varchar(80) not null,
IsActive bit not null,
CustomerId	integer not null,
Constraint FK_Customer foreign key(CustomerId) references Customer(Id)
);

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 10000, 'CC', 'Larry Visa', 1, c.Id
from Customer c where c.FirstName = 'Larry' and c.LastName = 'King';

insert into PaymentType
(AccountNo, AccType, Nickname, IsActive, CustomerId) 
select 20000, 'CC', 'Kenya Visa', 1, c.Id
from Customer c where c.FirstName = 'Kenya' and c.LastName = 'Stevens';

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 30000, 'CC', 'Kenya MasterCard', 1, c.Id
from Customer c where c.FirstName = 'Kenya' and c.LastName = 'Stevens';

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 40000, 'CC', 'Ken Discover', 1, c.Id
from Customer c where c.FirstName = 'Kenneth' and c.LastName = 'Burnett';

insert into PaymentType 
(AccountNo, AccType, Nickname, IsActive, CustomerId)
select 50000, 'CC', 'April Visa', 1, c.Id
from Customer c where c.FirstName = 'April' and c.LastName = 'Watson';

insert into PaymentType
(AccountNo, AccType, Nickname, IsActive, CustomerId) 
select 60000, 'CC', 'April MasterCard', 1, c.Id
from Customer c where c.FirstName = 'April' and c.LastName = 'Watson';

Create table Orders (
Id		integer not null primary key IDENTITY,
OrderDate		varchar(80) not null,
CustomerId 		integer not null,
PaymentTypeId	integer ,
Constraint FK_Customer_Order foreign key(CustomerId) references Customer(Id),
Constraint FK_PaymentType foreign key(PaymentTypeId) references PaymentType(Id)
);

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '7/25/18', c.Id, pt.Id
from Customer c, PaymentType pt where pt.Nickname = 'Kenya Visa' and c.Id = pt.CustomerId;

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '4/19/18', c.Id, pt.Id
from Customer c, PaymentType pt where pt.Nickname = 'April Visa' and c.Id = pt.CustomerId;

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '9/12/18', 4, null;

Insert into Orders 
(OrderDate, CustomerId, PaymentTypeId)
select '9/12/18', 4, null;


Create table ProductOrder (
Id	integer not null primary key IDENTITY,
ProductId	integer not null,
OrderId		integer not null,
Constraint FK_Product foreign key(ProductId) references Product(Id),
Constraint FK_Orders foreign key(OrderId) references Orders(Id)
);

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, 1
from Product p
where p.Title = 'Lysol Wipes';

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, 2
from Product p
where p.Title = 'Jumbo Frozen Shrimp';

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, 2
from Product p
where p.Title = 'Chiquita Bananas';

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, 3
from Product p
where p.Title = 'Teddy Bear';

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, 4
from Product p
where p.Title = 'Dove Beauty Bar';

Insert into ProductOrder 
(ProductId, OrderId)
select p.Id, 4
from Product p
where p.Title = 'Pantene Gold Series Shampoo';
# CleanArchitectureWIthCQRSAndMediator
Clean architecture in asp.net core web api | CQRS | Mediator

---- Create DataBase 

Create database CQRS
Use CQRS

create table Blog(
Id int primary key identity(1,1),
Name varchar(50),
Description varchar(50),
Author varchar(50)
)

insert into Blog(Name,Description,Author) values ('C#','C# Blog','Peter')
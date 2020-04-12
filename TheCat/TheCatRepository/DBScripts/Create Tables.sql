/*
    Script para criação das tabelas em um banco de dados SQL Server
*/

-- Caso as tabelas já existam, exclui
--------------------------------------------------------------------

if object_id('ImageUrlCategory', 'u') is not null begin
  drop table ImageUrlCategory
end
go

if object_id('ImageUrlBreeds', 'u') is not null begin
  drop table ImageUrlBreeds
end
go

if object_id('ImageUrl', 'u') is not null begin
  drop table ImageUrl
end
go

if object_id('Category', 'u') is not null begin
  drop table Category
end
go

if object_id('Breeds', 'u') is not null begin
  drop table Breeds
end
go

-- Cria as tabelas necessárias
--------------------------------------------------------------------

create table Breeds
(
    BreedsId varchar(80) not null,
    Name varchar(255) not null,
    Origin varchar(255),
    Temperament varchar(255),
    Description varchar(1024)
)
go

alter table Breeds add
    Constraint PK_Breeds
    Primary Key Clustered (BreedsId)
go

create table Category
(
    CategoryId int not null,
    Name varchar(255) not null
)
go

alter table Category add
    Constraint PK_Category
    Primary Key Clustered (CategoryId)
go

create table ImageUrl
(
    ImageUrlId varchar(80) not null,
    Url varchar(512) not null,
    Width int,
    Height int
)
go

alter table ImageUrl add
    Constraint PK_ImageUrl
    Primary Key Clustered (ImageUrlId)
go

create table ImageUrlBreeds
(
    ImageUrlId varchar(80) not null,
    BreedsId varchar(80) not null
)
go

alter table ImageUrlBreeds add
    Constraint PK_ImageUrlBreeds
    Primary Key Clustered (ImageUrlId, BreedsId)
go

alter table ImageUrlBreeds add
    Constraint FK_ImageUrlBreeds_ImageUrl
    Foreign Key (ImageUrlId)
    References ImageUrl (ImageUrlId)
go

alter table ImageUrlBreeds add
    Constraint FK_ImageUrlBreeds_Breeds
    Foreign Key (BreedsId)
    References Breeds (BreedsId)
go

create table ImageUrlCategory
(
    ImageUrlId varchar(80) not null,
    CategoryId int not null
)
go

alter table ImageUrlCategory add
    Constraint PK_ImageUrlCategory
    Primary Key Clustered (ImageUrlId, CategoryId)
go

alter table ImageUrlCategory add
    Constraint FK_ImageUrlCategory_ImageUrl
    Foreign Key (ImageUrlId)
    References ImageUrl (ImageUrlId)
go

alter table ImageUrlCategory add
    Constraint FK_ImageUrlCategory_Category
    Foreign Key (CategoryId)
    References Category (CategoryId)
go

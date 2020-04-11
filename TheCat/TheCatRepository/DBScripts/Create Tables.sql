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
    Height int,
    BreedsId varchar(80),
    CategoryId int
)
go

alter table ImageUrl add
    Constraint PK_ImageUrl
    Primary Key Clustered (ImageUrlId)
go

alter table ImageUrl add
    Constraint FK_ImageUrl_Breeds
    Foreign Key (BreedsId)
    References Breeds (BreedsId)
go

alter table ImageUrl add
    Constraint FK_ImageUrl_Category
    Foreign Key (CategoryId)
    References Category (CategoryId)
go

create index IDX_AK_ImageUrl_Breeds on ImageUrl (BreedsId)
go

create index IDX_AK_ImageUrl_Category on ImageUrl (CategoryId)
go

use Promt;
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
	RoleId int not null
);

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    [Description] NVARCHAR(MAX) NULL
);
create table Roles(
RoleId int identity(1,1) primary key,
RoleName nvarchar(50) not null unique
);
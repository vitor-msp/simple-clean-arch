CREATE TABLE IF NOT EXISTS Products (
    Id int not null primary key,
    CreatedAt datetime not null,
    Name varchar(10) not null,
    Price decimal(5, 2) not null,
    Description varchar(100),
    Category varchar(10) 
);
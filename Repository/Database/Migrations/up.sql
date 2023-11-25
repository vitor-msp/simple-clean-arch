CREATE TABLE Products (
    Id int not null primary key,
    Description varchar(255) not null,
    Price decimal(10, 2) not null,
    CreatedAt datetime not null,
    Category varchar(255) not null
);
CREATE SCHEMA [FantasyTravel];
GO

CREATE TABLE [FantasyTravel].[Places]
(
    id int PRIMARY KEY,
    name varchar(100) NOT NULL,
    description varchar(1000),
    language int NOT NULL,
    biomType int NOT NULL
); 
GO
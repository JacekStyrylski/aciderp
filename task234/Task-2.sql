-- Create a new database called 'DimensionsDB'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'DimensionsDB'
)
CREATE DATABASE DimensionsDB
GO

USE DimensionsDB
-- Create a new table called 'Staging' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Staging', 'U') IS NOT NULL
DROP TABLE dbo.Staging
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Staging
(
    Name [NVARCHAR](200) NOT NULL,
    Value INT
    -- specify more columns here
);
GO

-- Create a new table called 'Dimensions' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Dimensions', 'U') IS NOT NULL
DROP TABLE dbo.Dimensions
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Dimensions
(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    Name [NVARCHAR](200) NOT NULL UNIQUE,
    Value INT,
    IsDeleted BIT DEFAULT 0 NOT NULL
    -- specify more columns here
);
GO

-- Insert rows into table 'dbo.Staging'
INSERT INTO dbo.Staging
( -- columns to insert data into
 [Name], [Value]
)
VALUES
( -- first row: values for the columns in the list above
 'Lorem', 10
),
( -- second row: values for the columns in the list above
 'Ipsum', 12
),
( -- second row: values for the columns in the list above
 'Ipsum', 12
),
( -- second row: values for the columns in the list above
 'Ipsum', 12
),
( -- second row: values for the columns in the list above
 'Ipsum', 12
),
( -- second row: values for the columns in the list above
 'Ipsum', 12
),
( -- second row: values for the columns in the list above
 'Ipsum', 12
)

-- add more rows here
GO

-- Insert rows into table 'dbo.Dimensions'
INSERT INTO dbo.Dimensions
( -- columns to insert data into
 Name, Value
)
VALUES
( -- first row: values for the columns in the list above
'Kowalski', 1),
( -- second row: values for the columns in the list above
'Szczepan', 2
)
-- add more rows here
GO
-- Step 1: Create the database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'TestDb')
BEGIN
    CREATE DATABASE TestDb;
END
GO

-- Step 2: Use the new database
USE TestDb;
GO

-- Step 3: Create the table
IF OBJECT_ID('dbo.TestTable', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.TestTable;
END

CREATE TABLE dbo.TestTable (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETUTCDATE()
);
GO

-- Step 4: Insert test data
INSERT INTO dbo.TestTable (Name) VALUES
('Roman'),
('Nazar'),
('Ivan');
GO

-- Step 5: Verify
SELECT * FROM dbo.TestTable;
GO
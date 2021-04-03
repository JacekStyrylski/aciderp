USE DimensionsDB;

EXEC tSQLt.NewTestClass 'testDimensionsDb';
GO

CREATE PROCEDURE testDimensionsDb.[test that LoadDimensions will copy values from Staging to Dimensions]
AS
BEGIN
    EXEC tSQLt.FakeTable 'dbo.Staging';
    EXEC tSQLt.FakeTable 'dbo.Dimensions';
    INSERT INTO dbo.Staging ([Name]) VALUES ('Lorem');
    INSERT INTO dbo.Staging ([Name]) VALUES ('Ipsum');
    INSERT INTO dbo.Staging ([Name]) VALUES ('printing');
    INSERT INTO dbo.Staging ([Name]) VALUES ('typesetting');
    INSERT INTO dbo.Staging ([Name]) VALUES ('specimen');

    EXEC dbo.LoadDimensions;
    EXEC tSQLt.AssertEqualsTable 'dbo.Staging', 'dbo.Dimensions'
END;
GO

CREATE PROCEDURE testDimensionsDb.[test that LoadDimensions will update values in Dimensions based on Staging]
AS
BEGIN
    EXEC tSQLt.FakeTable 'dbo.Staging';
    EXEC tSQLt.FakeTable 'dbo.Dimensions';

    INSERT INTO dbo.Staging ([Name], [Value]) VALUES ('Lorem', 1);
    INSERT INTO dbo.Staging ([Name], [Value]) VALUES ('Ipsum', 1);
    INSERT INTO dbo.Staging ([Name], [Value]) VALUES ('printing', 1);
    INSERT INTO dbo.Staging ([Name], [Value]) VALUES ('typesetting', 1);
    INSERT INTO dbo.Staging ([Name], [Value]) VALUES ('specimen', 1);

    INSERT INTO dbo.Dimensions ([Name], [Value]) VALUES ('Lorem', 0);
    INSERT INTO dbo.Dimensions ([Name], [Value]) VALUES ('Ipsum', 0);
    INSERT INTO dbo.Dimensions ([Name], [Value]) VALUES ('printing', 0);
    INSERT INTO dbo.Dimensions ([Name], [Value]) VALUES ('typesetting', 0);
    INSERT INTO dbo.Dimensions ([Name], [Value]) VALUES ('specimen', 0);

    EXEC dbo.LoadDimensions;

    EXEC tSQLt.AssertEqualsTable 'dbo.Staging', 'dbo.Dimensions'
END;
GO

CREATE PROCEDURE testDimensionsDb.[test that LoadDimensions will set IsDelete to 1 in Dimensions entries if it does not exists in Staging]
AS
BEGIN
    EXEC tSQLt.FakeTable 'dbo.Staging';
    EXEC tSQLt.FakeTable 'dbo.Dimensions';
    INSERT INTO dbo.Dimensions ([Name]) VALUES ('Lorem');
    INSERT INTO dbo.Dimensions ([Name]) VALUES ('Ipsum');
    INSERT INTO dbo.Dimensions ([Name]) VALUES ('printing');
    INSERT INTO dbo.Dimensions ([Name]) VALUES ('typesetting');
    INSERT INTO dbo.Dimensions ([Name]) VALUES ('specimen');

    CREATE TABLE expectedDimensions (
	    Id INT,
	    [Name] NVARCHAR(200),
	    [Value] INT,
	    IsDeleted BIT
    );

    INSERT INTO expectedDimensions ([Name], [IsDeleted]) VALUES ('Lorem', 1);
    INSERT INTO expectedDimensions ([Name], [IsDeleted]) VALUES ('Ipsum', 1);
    INSERT INTO expectedDimensions ([Name], [IsDeleted]) VALUES ('printing', 1);
    INSERT INTO expectedDimensions ([Name], [IsDeleted]) VALUES ('typesetting', 1);
    INSERT INTO expectedDimensions ([Name], [IsDeleted]) VALUES ('specimen', 1);

    EXEC dbo.LoadDimensions;

    EXEC tSQLt.AssertEqualsTable 'dbo.Dimensions', 'expectedDimensions'
END;
GO

EXEC tSQLt.RunAll
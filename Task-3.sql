USE DimensionsDB
-- Create a new stored procedure called 'LoadDimensions' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'LoadDimensions'
)
DROP PROCEDURE dbo.LoadDimensions
GO

CREATE PROCEDURE dbo.LoadDimensions
AS
    BEGIN TRAN
        BEGIN TRY
            MERGE dbo.Dimensions AS T
            USING (SELECT DISTINCT Name, Value FROM dbo.Staging) AS S
            ON (T.Name = S.Name) 
            WHEN NOT MATCHED BY TARGET
                THEN INSERT(Name, Value) VALUES(S.Name, S.Value)
            WHEN MATCHED 
                THEN UPDATE SET T.Value = S.Value
            WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET T.IsDeleted = 1 
            OUTPUT $action, inserted.*, deleted.*;
        END TRY
        BEGIN CATCH
            ROLLBACK TRAN
        END CATCH
    COMMIT TRAN
GO

-- example to execute the stored procedure we just created
EXECUTE dbo.LoadDimensions
GO

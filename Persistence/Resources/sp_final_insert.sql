CREATE OR ALTER PROCEDURE dbo.SP_Final_Insert
AS
BEGIN
	insert into [dbo].[FinalTable]
(
[Company]
      ,[Year]
      ,[Value]
)
select 
[Company]
      ,[Year]
      ,[Value]

	  from [dbo].[StagingTable]
END
CREATE OR ALTER PROCEDURE MovieInfo.RetrieveTheaters
AS

SELECT T.Name, T.Address, T.TheaterID
FROM MovieInfo.Theater T
WHERE T.isActive = TRUE;
GO
CREATE OR ALTER PROCEDURE MovieInfo.RetrieveShowtimesByTheaterAndDateAndMovie
   @TheaterID INT
   @Date INT
   @MovieID INT
AS

SELECT 
	MS.StartTime
FROM MovieInfo.MovieShowtime MS
	INNER JOIN MovieInfo.Movie M ON M.MovieID = MS.MovieID
	INNER JOIN MovieInfo.Screen S ON S.ScreenID = MS.ScreenID
WHERE S.TheaterID = @TheaterID
	AND DATE(StartTime) = @Date
	AND M.MovieID = @MovieID;
GO
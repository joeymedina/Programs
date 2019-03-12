CREATE OR ALTER PROCEDURE MovieInfo.RetrieveMoviesByTheaterAndDate
   @TheaterID INT
   @Date INT
AS

SELECT M.Name, M.Director, M.Genre, M.RunTime, 
FROM MovieInfo.MovieShowtime MS
	INNER JOIN MovieInfo.Movie M ON M.MovieID = MS.MovieID
	INNER JOIN MovieInfo.Screen S ON S.ScreenID = MS.ScreenID
	INNER JOIN MovieInfo.Theater T ON T.TheaterID = S.TheaterID
WHERE T.TheaterID = @TheaterID
	AND DATE(MS.StartTime) = @Date;
GO
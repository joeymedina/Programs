CREATE OR ALTER PROCEDURE MovieInfo.CreateMovie
   @Name NVARCHAR(32),
   @Genre NVARCHAR(32),
   @Runtime Time,
   @MovieId INT OUTPUT
AS

INSERT MovieInfo.Movie(Name, Genre, Runtime)
VALUES(@Name, @Genre, @Runtime);

SET @PersonId = SCOPE_IDENTITY();
GO

SELECT DISTINCT( @DirectorID = D.DirectorID)
	FROM MovieInfo.Director D
	WHERE D.Name = @Director
	
IF @@ROWCOUNT < 0
BEGIN
	INSERT MovieInfo.Director(Name)
	VALUES(@Director);
	SET @DirectorID = SCOPE_IDENTITY();
END

INSERT MovieInfo.MovieDirector(DirectorID, MovieID)
VALUES(@DirectorID, @MovieID)

GO
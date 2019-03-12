DROP SCHEMA IF EXISTS MovieInfo;

GO

CREATE SCHEMA MovieInfo;

GO

CREATE TABLE MovieInfo.Director (
	DirectorId bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(256) NOT NULL
)

CREATE TABLE MovieInfo.Movie (
	MovieId bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(256) NOT NULL,
	Genre nvarchar(256) NULL,
	Runtime Time(0) NOT NULL
)

CREATE TABLE MovieInfo.MovieDirector (
	MovieId bigint NOT NULL,
	DirectorId bigint NOT NULL,

	UNIQUE(MovieId, DirectorId),

	FOREIGN KEY(MovieId) REFERENCES MovieInfo.Movie(MovieId),
	FOREIGN KEY(DirectorId) REFERENCES MovieInfo.Director(DirectorId)
)

CREATE TABLE MovieInfo.Theatre (
	TheatreId bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(256) NOT NULL,
	Address nvarchar(450) NOT NULL UNIQUE
)

CREATE TABLE MovieInfo.Screen (
	ScreenId bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
	TheatreId bigint NOT NULL,
	Seats int NULL,
	RoomNumber nvarchar(10) NOT NULL,

	UNIQUE(TheatreId, RoomNumber),

	FOREIGN KEY(TheatreId) REFERENCES MovieInfo.Theatre(TheatreId)
)

CREATE TABLE MovieInfo.MovieShowtime (
	MovieId bigint NOT NULL,
	ScreenId bigint NOT NULL,
	StartTime DateTime NOT NULL,

	UNIQUE(MovieId, ScreenId),

	FOREIGN KEY(MovieId) REFERENCES MovieInfo.Movie(MovieId),
	FOREIGN KEY(ScreenId) REFERENCES MovieInfo.Screen(ScreenId)
)
USE IMDB
GO
SET NOCOUNT ON
GO
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE movie.Tmp_titlebasics
	(
	tconst varchar(12) NOT NULL,
	titleType nvarchar(20) NULL,
	primaryTitle nvarchar(1000) NULL,
	originalTitle nvarchar(1000) NULL,
	isAdult bit NULL,
	startYear smallint NULL,
	endYear smallint NULL,
	runtimeMinutes int NULL,
	genres nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE movie.Tmp_titlebasics SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM movie.titlebasics)
	 EXEC('INSERT INTO movie.Tmp_titlebasics (tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear, runtimeMinutes, genres)
		SELECT tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear, runtimeMinutes, genres FROM movie.titlebasics WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE movie.titlebasics
GO
EXECUTE sp_rename N'movie.Tmp_titlebasics', N'titlebasics', 'OBJECT' 
GO
ALTER TABLE movie.titlebasics ADD CONSTRAINT
	PK_titlebasics PRIMARY KEY CLUSTERED 
	(
	tconst
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
GO
UPDATE [movie].titlebasics
SET [genres] = ''
WHERE [genres] IS NULL
GO
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE movie.Tmp_titlebasics
	(
	tconst varchar(12) NOT NULL,
	titleType nvarchar(20) NOT NULL,
	primaryTitle nvarchar(1000) NOT NULL,
	originalTitle nvarchar(1000) NOT NULL,
	isAdult bit NOT NULL,
	startYear smallint NULL,
	endYear smallint NULL,
	runtimeMinutes int NULL,
	genres nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE movie.Tmp_titlebasics SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM movie.titlebasics)
	 EXEC('INSERT INTO movie.Tmp_titlebasics (tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear, runtimeMinutes, genres)
		SELECT tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear, runtimeMinutes, genres FROM movie.titlebasics WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE movie.titlebasics
GO
EXECUTE sp_rename N'movie.Tmp_titlebasics', N'titlebasics', 'OBJECT' 
GO
ALTER TABLE movie.titlebasics ADD CONSTRAINT
	PK_titlebasics PRIMARY KEY CLUSTERED 
	(
	tconst
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
GO
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'movie.titlebasics', N'Titles', 'OBJECT' 
GO
EXECUTE sp_rename N'movie.Titles.tconst', N'Tmp_Code', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.titleType', N'Tmp_TitleType_1', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.primaryTitle', N'Tmp_PrimaryTitle_2', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.originalTitle', N'Tmp_OriginalTitle_3', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.isAdult', N'Tmp_IsAdult_4', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.startYear', N'Tmp_StartYear_5', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.endYear', N'Tmp_EndYear_6', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.runtimeMinutes', N'Tmp_RuntimeMinutes_7', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.genres', N'Tmp_Genres_8', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_Code', N'Code', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_TitleType_1', N'TitleType', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_PrimaryTitle_2', N'PrimaryTitle', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_OriginalTitle_3', N'OriginalTitle', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_IsAdult_4', N'IsAdult', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_StartYear_5', N'StartYear', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_EndYear_6', N'EndYear', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_RuntimeMinutes_7', N'RuntimeMinutes', 'COLUMN' 
GO
EXECUTE sp_rename N'movie.Titles.Tmp_Genres_8', N'Genres', 'COLUMN' 
GO
ALTER TABLE movie.Titles
	DROP CONSTRAINT PK_titlebasics
GO
ALTER TABLE movie.Titles ADD CONSTRAINT
	PK_Titles PRIMARY KEY CLUSTERED 
	(
	Code
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE movie.Titles SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO

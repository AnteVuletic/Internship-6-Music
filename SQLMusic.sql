CREATE DATABASE Music

BEGIN TRANSACTION

CREATE TABLE Artist(
	ID_Artist INT IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(50),
	Nationality nvarchar(40)
)

CREATE TABLE Album(
	ID_Album INT IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(50),
	YearOfRelease datetime2
)

CREATE TABLE Song(
	ID_Song INT IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(50),
	Duration time
)

COMMIT

BEGIN TRANSACTION

CREATE TABLE Album_Song(
	FK_Album INT,
	FK_Song INT,
	FOREIGN KEY (FK_Album) REFERENCES dbo.Album(ID_Album),
	FOREIGN KEY (FK_Song) REFERENCES dbo.Song(ID_Song)
)

ALTER TABLE Album
ADD FK_Artist INT

ALTER TABLE Album
ADD CONSTRAINT FK_Album_Artist FOREIGN KEY (FK_Artist) REFERENCES Artist(ID_Artist)

COMMIT

INSERT INTO Artist
VALUES ('Kid Cudi','American'),
		('Linkin Park','American'),
		('Pink Floyd','English')

BEGIN TRANSACTION
INSERT INTO Song
VALUES	('Mojo So Deep','00:03:31'),
		('Dont Play this Song','00:03:43'),
		('Trapped in My Mind','00:03:34'),
		('Ghost!','00:04:49'),
		('All Along','00:03:23'),
		('Up Up & Away','00:03:47'),
		('Cudi Zone','00:04:19'),
		('Sky Might Fall','00:03:41'),
		('Solo Dolo','00:04:26'),
		('Soundtrack 2 My Life','00:03:56'),
		('Somewhere I Belong','00:03:33'),
		('Lying from You','00:02:55'),
		('Hit the Floor','00:02:44'),
		('Faint','00:02:42'),
		('Easier to Run','00:03:24'),
		('Wake','00:01:40'),
		('Given Up','00:03:09'),
		('Leave Out All The Rest','00:03:29'),
		('Bleed It Out','00:02:44'),
		('Speak To Me','00:01:30'),
		('Breathe','00:02:43'),
		('On The Run','00:03:30'),
		('Time','00:06:53'),
		('The Great Gig in the Sky','00:04:15'),
		('The Thin Ice','00:02:27'),
		('Another Brick in the Wall(Part 1)','00:03:21'),
		('The Happiest Days of Our Lives','00:01:46'),
		('Another Brick in the Wall(Part 2)','00:03:59')

INSERT INTO Album
VALUES  ('Man on the Moon II',CONVERT(datetime2,'09/11/2010 12:00',103),1),
		('Man on the Moon: The End of Day',CONVERT(datetime2,'15/09/2009 12:00',103),1),
		('Meteora',CONVERT(datetime2,'25/03/2003 12:00',103),2),
		('Minutes to Midnight',CONVERT(datetime2,'14/05/2007 12:00',103),2),
		('The Dark Side of the Moon',CONVERT(datetime2,'01/03/1973 12:00',103),3),
		('The Wall',CONVERT(datetime2,'30/11/1979 12:00',103),3)

COMMIT

BEGIN TRANSACTION

INSERT INTO Album_Song
VALUES (1,1),
		(1,2),
		(1,3),
		(1,4),
		(1,5),
		(2,6),
		(2,7),
		(2,8),
		(2,9),
		(2,10),
		(3,11),
		(3,12),
		(3,13),
		(3,14),
		(3,15),
		(4,15),
		(4,16),
		(4,17),
		(4,18),
		(4,19),
		(5,20),
		(5,21),
		(5,22),
		(5,23),
		(5,24),
		(6,24),
		(6,25),
		(6,26),
		(6,27),
		(6,28)

COMMIT
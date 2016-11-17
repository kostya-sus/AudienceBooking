CREATE TABLE [dbo].[Audience]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[SeatsCount] INT NOT NULL,
	[BoardsCount] INT NOT NULL,
	[LaptopsCount] INT NOT NULL,
	[PrintersCount] INT NOT NULL,
	[ProjectorsCount] INT NOT NULL,
	[IsBookingAvailable] BIT NOT NULL
)

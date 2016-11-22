CREATE TABLE [dbo].[Audience] (
    [Id]                 INT            NOT NULL,
    [SeatsCount]         INT            NOT NULL,
    [BoardsCount]        INT            NOT NULL,
    [LaptopsCount]       INT            NOT NULL,
    [PrintersCount]      INT            NOT NULL,
    [ProjectorsCount]    INT            NOT NULL,
    [IsBookingAvailable] BIT            NOT NULL,
    [Name]               NVARCHAR (30) NULL,
    CONSTRAINT [PK_dbo.Audience] PRIMARY KEY CLUSTERED ([Id] ASC)
);


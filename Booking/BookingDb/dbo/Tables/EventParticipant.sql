CREATE TABLE [dbo].[EventParticipant]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	[EventId] UNIQUEIDENTIFIER NOT NULL,
	[ParticipantEmail] VARCHAR(254) NOT NULL,
	CONSTRAINT [FK_dbo.EventParticipant_dbo.Event_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Event] ([Id]) ON DELETE CASCADE
)

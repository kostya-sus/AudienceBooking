CREATE TABLE [dbo].[EventParticipant] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [EventId]          UNIQUEIDENTIFIER NOT NULL,
    [ParticipantEmail] VARCHAR (254)    NOT NULL,
    CONSTRAINT [PK_dbo.EventParticipant] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.EventParticipant_dbo.Event_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Event] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_EventId]
    ON [dbo].[EventParticipant]([EventId] ASC);


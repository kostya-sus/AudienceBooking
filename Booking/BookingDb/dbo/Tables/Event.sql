CREATE TABLE [dbo].[Event] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [EventDateTime]   SMALLDATETIME    NOT NULL,
    [Duration]        INT              NOT NULL,
    [Title]           NVARCHAR (50)    NOT NULL,
    [AuthorId]        NVARCHAR (128)   NOT NULL,
    [IsPublic]        BIT              NOT NULL,
    [AudienceId]      INT              NOT NULL,
    [IsAuthorShown]   BIT              NOT NULL,
    [AuthorName]      NVARCHAR (30)    NULL,
    [AdditionalInfo]  NVARCHAR (600)   NULL,
    [IsJoinAvailable] BIT              NOT NULL,
    CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Event_dbo.AspNetUsers_UserId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Event_dbo.Audience_AudienceId] FOREIGN KEY ([AudienceId]) REFERENCES [dbo].[Audience] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AudienceId]
    ON [dbo].[Event]([AudienceId] ASC);


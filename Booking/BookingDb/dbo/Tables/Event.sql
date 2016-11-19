CREATE TABLE [dbo].[Event]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	[EventDateTime] SMALLDATETIME NOT NULL,
	[Duration] INT NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[AuthorId] NVARCHAR(128) NOT NULL,	
	[IsPublic] BIT NOT NULL,
	[AudienceId] INT NOT NULL,
	[IsAuthorShown] BIT NOT NULL,
	[AuthorName] NVARCHAR(30),
	[AdditionalInfo] NVARCHAR(600),
    [IsJoinAvailable] BIT              DEFAULT ((0)) NOT NULL,
	CONSTRAINT [FK_dbo.Event_dbo.AspNetUsers_UserId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Event_dbo.Audience_AudienceId] FOREIGN KEY ([AudienceId]) REFERENCES [dbo].[Audience] ([Id]) ON DELETE CASCADE

)

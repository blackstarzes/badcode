CREATE TABLE [dbo].[Post]
(
	[PostId] BIGINT NOT NULL IDENTITY, 
    [AccountId] BIGINT NOT NULL, 
    [Content] NVARCHAR(140) NOT NULL, 
    [DateTimeCreatedUtc] DATETIME NOT NULL, 
    CONSTRAINT [PK_Post] PRIMARY KEY ([PostId]) 
)

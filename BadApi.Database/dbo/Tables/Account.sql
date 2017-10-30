CREATE TABLE [dbo].[Account]
(
	[AccountId] BIGINT NOT NULL IDENTITY, 
    [EmailAddress] VARCHAR(200) NOT NULL, 
    [Password] VARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_Account] PRIMARY KEY ([AccountId])
)

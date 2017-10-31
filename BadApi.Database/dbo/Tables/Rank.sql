CREATE TABLE [dbo].[Rank]
(
	[PostId] BIGINT NOT NULL , 
    [AccountId] BIGINT NOT NULL, 
    [IsPositive] BIT NOT NULL, 
    CONSTRAINT [PK_Rank] PRIMARY KEY ([PostId], [AccountId]), 
    CONSTRAINT [FK_Rank_Post] FOREIGN KEY ([PostId]) REFERENCES dbo.Post([PostId]), 
    CONSTRAINT [FK_Rank_Account] FOREIGN KEY ([AccountId]) REFERENCES dbo.Account([AccountId])
)

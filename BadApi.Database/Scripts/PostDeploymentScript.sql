/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE
	dbo.Account t
USING
	(
		VALUES
			('user@api.com', 'Password123')
	) s(EmailAddress, Password)
	ON t.EmailAddress = s.EmailAddress
WHEN MATCHED THEN
	UPDATE SET Password = s.Password
WHEN NOT MATCHED BY TARGET THEN
	INSERT (EmailAddress, Password) VALUES (s.EmailAddress, s.Password);
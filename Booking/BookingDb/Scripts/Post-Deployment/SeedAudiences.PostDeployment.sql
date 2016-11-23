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
INSERT INTO [dbo].[Audience](Id,SeatsCount,BoardsCount,LaptopsCount,PrintersCount,ProjectorsCount,IsBookingAvailable, Name)
VALUES (0, 10, 0, 5, 1, 0, 0, 'HR office'),
	   (1, 15, 1, 10, 0, 1, 1, 'Einstein Classroom'),
       (2, 10, 0, 5, 1, 0, 0, 'Info center'),
	   (3, 10, 0, 5, 1, 0, 0, 'Web & Marketing'),
       (4, 10, 0, 5, 1, 0, 0, 'Web & Marketing'),
       (5, 10, 0, 5, 1, 0, 0, 'Web & Marketing'),
       (6, 5, 0, 3, 1, 0, 0, 'English'),
       (7, 15, 1, 10, 0, 1, 1, 'Tesla Classroom'),
       (8, 15, 1, 10, 0, 1, 1, 'Newton Classroom');

GO

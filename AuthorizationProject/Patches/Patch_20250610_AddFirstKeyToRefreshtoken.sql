BEGIN TRANSACTION;
ALTER TABLE [dbo].[RefreshToken] DROP CONSTRAINT [PK_RefreshToken];

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[RefreshToken]') AND [c].[name] = N'Guid');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [dbo].[RefreshToken] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [dbo].[RefreshToken] DROP COLUMN [Guid];

ALTER TABLE [dbo].[RefreshToken] ADD [Id] int NOT NULL IDENTITY;

ALTER TABLE [dbo].[RefreshToken] ADD CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250610193015_AddFirstKeyToRefreshtoken', N'9.0.5');

COMMIT;
GO


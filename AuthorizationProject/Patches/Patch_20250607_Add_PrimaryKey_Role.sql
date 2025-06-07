BEGIN TRANSACTION;
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role_RoleCode];

DROP INDEX [IX_UserRole_RoleCode] ON [dbo].[UserRole];

ALTER TABLE [dict].[Role] DROP CONSTRAINT [PK_Role];

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[UserRole]') AND [c].[name] = N'RoleCode');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [dbo].[UserRole] DROP COLUMN [RoleCode];

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dict].[Role]') AND [c].[name] = N'RoleCode');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [dict].[Role] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [dict].[Role] ALTER COLUMN [RoleCode] nvarchar(max) NOT NULL;

ALTER TABLE [dict].[Role] ADD [Id] int NOT NULL IDENTITY;

ALTER TABLE [dict].[Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY ([Id]);

CREATE INDEX [IX_UserRole_RoleId] ON [dbo].[UserRole] ([RoleId]);

ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dict].[Role] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250607112742_Add_PrimaryKey_Role', N'9.0.5');

COMMIT;
GO


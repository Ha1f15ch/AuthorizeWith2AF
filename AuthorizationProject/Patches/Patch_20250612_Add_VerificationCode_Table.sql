BEGIN TRANSACTION;
DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[User]') AND [c].[name] = N'UserPassword');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [dbo].[User] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [dbo].[User] ALTER COLUMN [UserPassword] nvarchar(max) NULL;

ALTER TABLE [dbo].[RefreshToken] ADD [DateCreated] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

CREATE TABLE [dbo].[VerificationCode] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Code] nvarchar(8) NOT NULL,
    [UserEmail] nvarchar(100) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [IsUsed] bit NOT NULL,
    CONSTRAINT [PK_VerificationCode] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_VerificationCode_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_VerificationCode_UserId] ON [dbo].[VerificationCode] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250612140237_Add_VerificationCode_Table', N'9.0.5');

COMMIT;
GO


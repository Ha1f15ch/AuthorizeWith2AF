IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'dict') IS NULL EXEC(N'CREATE SCHEMA [dict];');

CREATE TABLE [dict].[Role] (
    [RoleCode] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [DeletedDate] datetime2 NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleCode])
);

CREATE TABLE [dbo].[User] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [UserEmail] nvarchar(max) NOT NULL,
    [UserPassword] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [DeleteDate] datetime2 NULL,
    [IsActive] bit NOT NULL,
    [IsApprove] bit NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[RefreshToken] (
    [Guid] uniqueidentifier NOT NULL,
    [UserId] int NOT NULL,
    [UniqueRefreshToken] nvarchar(max) NOT NULL,
    [DateExpired] datetime2 NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Guid]),
    CONSTRAINT [FK_RefreshToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[UserRole] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    [RoleCode] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserRole_Role_RoleCode] FOREIGN KEY ([RoleCode]) REFERENCES [dict].[Role] ([RoleCode]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_RefreshToken_UserId] ON [dbo].[RefreshToken] ([UserId]);

CREATE INDEX [IX_UserRole_RoleCode] ON [dbo].[UserRole] ([RoleCode]);

CREATE INDEX [IX_UserRole_UserId] ON [dbo].[UserRole] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250604190702_Initialize_Migration', N'9.0.5');

COMMIT;
GO


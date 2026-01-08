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
GO

IF SCHEMA_ID(N'SolutionTemplate') IS NULL EXEC(N'CREATE SCHEMA [SolutionTemplate];');
GO

CREATE TABLE [SolutionTemplate].[Audits] (
    [Id] uniqueidentifier NOT NULL,
    [TableName] nvarchar(50) NOT NULL,
    [EntityName] nvarchar(50) NOT NULL,
    [ActionType] nvarchar(50) NOT NULL,
    [ActionName] nvarchar(100) NOT NULL,
    [EntityId] uniqueidentifier NOT NULL,
    [OldValues] nvarchar(max) NULL,
    [NewValues] nvarchar(max) NOT NULL,
    [ClientApplicationId] nvarchar(50) NOT NULL,
    [FromIpAddress] nvarchar(20) NOT NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [Accuracy] float NULL,
    [Created] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Audits] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Items] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Category] int NOT NULL,
    [TotalStock] int NOT NULL,
    [AvailableStock] int NOT NULL,
    [Unit] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [ExpiryDate] datetime2 NULL,
    [Created] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [Modified] datetimeoffset NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [LoanTransactions] (
    [Id] uniqueidentifier NOT NULL,
    [ItemId] uniqueidentifier NOT NULL,
    [BorrowerName] nvarchar(max) NOT NULL,
    [BorrowerPhone] nvarchar(max) NOT NULL,
    [LoanDate] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [ReturnDate] datetime2 NULL,
    [Status] int NOT NULL,
    [QrCodeToken] nvarchar(max) NOT NULL,
    [Created] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [Modified] datetimeoffset NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_LoanTransactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanTransactions_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RackSlots] (
    [Id] uniqueidentifier NOT NULL,
    [RackCode] nvarchar(max) NOT NULL,
    [PositionX] int NOT NULL,
    [PositionY] int NOT NULL,
    [Status] int NOT NULL,
    [ItemId] uniqueidentifier NULL,
    [Created] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [Modified] datetimeoffset NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_RackSlots] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RackSlots_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id])
);
GO

CREATE INDEX [IX_LoanTransactions_ItemId] ON [LoanTransactions] ([ItemId]);
GO

CREATE INDEX [IX_RackSlots_ItemId] ON [RackSlots] ([ItemId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260108104740_InitialInventory', N'8.0.17');
GO

COMMIT;
GO


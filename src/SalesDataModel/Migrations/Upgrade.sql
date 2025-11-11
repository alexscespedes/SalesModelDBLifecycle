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
CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);

CREATE TABLE [Customers] (
    [CustomerId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
);

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);

CREATE TABLE [Orders] (
    [OrderId] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [CustomerId] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
);

CREATE TABLE [ProductCategories] (
    [ProductId] int NOT NULL,
    [CategoryId] int NOT NULL,
    [AssignedtAt] datetime2 NOT NULL,
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([ProductId], [CategoryId]),
    CONSTRAINT [FK_ProductCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductCategories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);

CREATE TABLE [OrderItems] (
    [OrderItemId] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([OrderItemId]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);

CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);

CREATE INDEX [IX_ProductCategories_CategoryId] ON [ProductCategories] ([CategoryId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251106081226_InitialCreate', N'9.0.10');

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Name])
VALUES (1, N'Electronics'),
(2, N'Books'),
(3, N'Office Suppliers');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [Name], [Price])
VALUES (1, N'Laptop', 1200.0),
(2, N'C# in Depth', 60.0),
(3, N'Desk Chair', 150.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId', N'AssignedtAt') AND [object_id] = OBJECT_ID(N'[ProductCategories]'))
    SET IDENTITY_INSERT [ProductCategories] ON;
INSERT INTO [ProductCategories] ([CategoryId], [ProductId], [AssignedtAt])
VALUES (1, 1, '2025-05-03T12:00:00.0000000Z'),
(2, 2, '2025-05-03T12:00:00.0000000Z');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId', N'AssignedtAt') AND [object_id] = OBJECT_ID(N'[ProductCategories]'))
    SET IDENTITY_INSERT [ProductCategories] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251110234331_SeedInitialData', N'9.0.10');

ALTER TABLE [Products] ADD [StockQuantity] int NOT NULL DEFAULT 0;

UPDATE [Products] SET [StockQuantity] = 0
WHERE [ProductId] = 1;
SELECT @@ROWCOUNT;


UPDATE [Products] SET [StockQuantity] = 0
WHERE [ProductId] = 2;
SELECT @@ROWCOUNT;


UPDATE [Products] SET [StockQuantity] = 0
WHERE [ProductId] = 3;
SELECT @@ROWCOUNT;


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251111075934_AddStockQuantityToProduct', N'9.0.10');

COMMIT;
GO


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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [Categoria] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(128) NOT NULL,
        CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [Cliente] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(30) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [CPF] char(14) NOT NULL,
        [DataNascimento] datetime2 NOT NULL,
        CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [Produto] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(128) NOT NULL,
        [Estoque] int NOT NULL,
        [Preco] float NOT NULL,
        [CategoriaId] int NOT NULL,
        CONSTRAINT [PK_Produto] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Produto_Categoria_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [Endereco] (
        [Id] int NOT NULL IDENTITY,
        [Logradouro] nvarchar(max) NOT NULL,
        [Numero] nvarchar(max) NOT NULL,
        [Complemento] nvarchar(max) NOT NULL,
        [Bairro] nvarchar(max) NOT NULL,
        [Cidade] nvarchar(max) NOT NULL,
        [Estado] char(2) NOT NULL,
        [CEP] char(9) NOT NULL,
        [Referencia] nvarchar(max) NOT NULL,
        [Selecionado] bit NOT NULL,
        [ClienteModelId] int NOT NULL,
        CONSTRAINT [PK_Endereco] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Endereco_Cliente_ClienteModelId] FOREIGN KEY ([ClienteModelId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [NotaFiscal] (
        [Id] int NOT NULL IDENTITY,
        [Numero] nvarchar(max) NOT NULL,
        [DataEmissao] datetime2 NOT NULL,
        [PedidoId] int NOT NULL,
        [ClienteId] int NOT NULL,
        [ValorProdutos] decimal(18,2) NOT NULL,
        [ValorTotal] decimal(18,2) NOT NULL,
        [Observacoes] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_NotaFiscal] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_NotaFiscal_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [Pedido] (
        [Id] int NOT NULL IDENTITY,
        [DataPedido] datetime2 NULL,
        [DataEntrega] datetime2 NULL,
        [ValorTotal] float NULL,
        [ClienteId] int NOT NULL,
        [Nome] nvarchar(128) NOT NULL,
        [Email] nvarchar(128) NOT NULL,
        [Senha] nvarchar(128) NOT NULL,
        CONSTRAINT [PK_Pedido] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pedido_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE TABLE [ItemPedido] (
        [IdPedido] int NOT NULL,
        [IdProduto] int NOT NULL,
        [Id] int NOT NULL,
        [Quantidade] int NOT NULL,
        [ValorUnitario] float NOT NULL,
        [PedidoId] int NOT NULL,
        [ProdutoId] int NOT NULL,
        [NotaFiscalModelId] int NULL,
        CONSTRAINT [PK_ItemPedido] PRIMARY KEY ([IdPedido], [IdProduto]),
        CONSTRAINT [FK_ItemPedido_NotaFiscal_NotaFiscalModelId] FOREIGN KEY ([NotaFiscalModelId]) REFERENCES [NotaFiscal] ([Id]),
        CONSTRAINT [FK_ItemPedido_Pedido_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedido] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ItemPedido_Produto_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_Endereco_ClienteModelId] ON [Endereco] ([ClienteModelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_ItemPedido_NotaFiscalModelId] ON [ItemPedido] ([NotaFiscalModelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_ItemPedido_PedidoId] ON [ItemPedido] ([PedidoId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_ItemPedido_ProdutoId] ON [ItemPedido] ([ProdutoId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_NotaFiscal_ClienteId] ON [NotaFiscal] ([ClienteId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_Pedido_ClienteId] ON [Pedido] ([ClienteId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    CREATE INDEX [IX_Produto_CategoriaId] ON [Produto] ([CategoriaId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240713173149_V1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240713173149_V1', N'8.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240714103344_mssql.local_migration_150'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240714103344_mssql.local_migration_150', N'8.0.4');
END;
GO

COMMIT;
GO


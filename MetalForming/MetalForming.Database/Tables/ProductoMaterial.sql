CREATE TABLE [dbo].[ProductoMaterial] (
    [IdProducto] INT NOT NULL,
    [IdMaterial] INT NOT NULL,
    [Cantidad]   INT NOT NULL,
    CONSTRAINT [PK_ProductoMaterial] PRIMARY KEY CLUSTERED ([IdProducto] ASC, [IdMaterial] ASC),
    CONSTRAINT [FK_ProductoMaterial_Material] FOREIGN KEY ([IdMaterial]) REFERENCES [dbo].[Material] ([Id]),
    CONSTRAINT [FK_ProductoMaterial_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([Id])
);


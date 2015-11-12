CREATE TABLE [dbo].[OrdenVenta] (
    [Id]                      INT          IDENTITY (1, 1) NOT NULL,
    [Numero]                  VARCHAR (50) NOT NULL,
    [Cliente]                 VARCHAR (50) NOT NULL,
    [FechaEntrega]            DATETIME     NOT NULL,
    [Estado]                  VARCHAR (50) NOT NULL,
    [Cantidad]                INT          NOT NULL,
    [IdProducto]              INT          NOT NULL,
    [IdPrograma]              INT          NULL,
    [IdAsistentePlaneamiento] INT          NULL,
    CONSTRAINT [PK_OrdenVenta] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdenVenta_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([Id]),
    CONSTRAINT [FK_OrdenVenta_Programa] FOREIGN KEY ([IdPrograma]) REFERENCES [dbo].[Programa] ([Id]),
    CONSTRAINT [FK_OrdenVenta_Usuario] FOREIGN KEY ([IdAsistentePlaneamiento]) REFERENCES [dbo].[Usuario] ([Id])
);


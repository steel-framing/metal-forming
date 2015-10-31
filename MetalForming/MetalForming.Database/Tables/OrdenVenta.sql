CREATE TABLE [dbo].[OrdenVenta] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Numero]       VARCHAR (50) NOT NULL,
    [Cliente]      VARCHAR (50) NOT NULL,
    [FechaEntrega] DATETIME     NOT NULL,
    [Estado]       VARCHAR (50) NOT NULL,
    [Cantidad]     INT          NOT NULL,
    [IdProducto]   INT          NOT NULL,
    CONSTRAINT [PK_OrdenVenta] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdenVenta_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([Id])
);


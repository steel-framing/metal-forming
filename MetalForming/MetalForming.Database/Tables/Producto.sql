CREATE TABLE [dbo].[Producto] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (50) NOT NULL,
    [Stock]       INT          NOT NULL,
    [StockMinimo] INT          NOT NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED ([Id] ASC)
);


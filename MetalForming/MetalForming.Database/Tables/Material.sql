CREATE TABLE [dbo].[Material] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (50) NOT NULL,
    [Stock]       INT          NOT NULL,
    [StockMinimo] INT          NOT NULL,
    [Reservado]   INT          NOT NULL,
    CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED ([Id] ASC)
);


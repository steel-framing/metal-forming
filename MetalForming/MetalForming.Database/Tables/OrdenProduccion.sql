CREATE TABLE [dbo].[OrdenProduccion] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Numero]           VARCHAR (50)  NOT NULL,
    [IdOrdenVenta]     INT           NOT NULL,
    [Estado]           VARCHAR (50)  NOT NULL,
    [CantidadProducto] INT           NOT NULL,
    [FechaCreacion]    DATETIME      NOT NULL,
    [MotivoRechazo]    VARCHAR (500) NULL,
    CONSTRAINT [PK_OrdenProduccion] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdenProduccion_OrdenVenta] FOREIGN KEY ([IdOrdenVenta]) REFERENCES [dbo].[OrdenVenta] ([Id])
);


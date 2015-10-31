CREATE TABLE [dbo].[OrdenProduccionSecuencia] (
    [Id]                INT      IDENTITY (1, 1) NOT NULL,
    [Secuencia]         INT      NOT NULL,
    [IdOrdenProduccion] INT      NOT NULL,
    [IdMaquina]         INT      NOT NULL,
    [FechaInicio]       DATETIME NOT NULL,
    [FechaFin]          DATETIME NOT NULL,
    CONSTRAINT [PK_OrdenProduccionSecuencia] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdenProduccionSecuencia_Maquina] FOREIGN KEY ([IdMaquina]) REFERENCES [dbo].[Maquina] ([Id]),
    CONSTRAINT [FK_OrdenProduccionSecuencia_OrdenProduccion] FOREIGN KEY ([IdOrdenProduccion]) REFERENCES [dbo].[OrdenProduccion] ([Id])
);


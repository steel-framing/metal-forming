CREATE TABLE [dbo].[OrdenProduccionMaterial] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [IdOrdenProduccion] INT NOT NULL,
    [IdMaterial]        INT NOT NULL,
    [Requerido]         INT NOT NULL,
    [Comprar]           INT NOT NULL,
    CONSTRAINT [PK_OrdenProduccionMaterial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdenProduccionMaterial_Material] FOREIGN KEY ([IdMaterial]) REFERENCES [dbo].[Material] ([Id]),
    CONSTRAINT [FK_OrdenProduccionMaterial_OrdenProduccion] FOREIGN KEY ([IdOrdenProduccion]) REFERENCES [dbo].[OrdenProduccion] ([Id])
);


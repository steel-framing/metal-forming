CREATE TABLE [dbo].[Programa] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Numero]      VARCHAR (50) NOT NULL,
    [FechaInicio] DATE         NOT NULL,
    [FechaFin]    DATE         NOT NULL,
    [CantidadOV]  INT          NOT NULL,
    [Estado]      VARCHAR (50) NOT NULL,
    [IdPlan]      INT          NOT NULL,
    CONSTRAINT [PK_Programa] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Programa_Plan] FOREIGN KEY ([IdPlan]) REFERENCES [dbo].[Plan] ([Id])
);


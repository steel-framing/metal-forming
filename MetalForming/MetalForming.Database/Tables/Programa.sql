CREATE TABLE [dbo].[Programa] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [FechaInicio]        DATE          NOT NULL,
    [FechaFin]           DATE          NOT NULL,
    [CantidadOV]         INT           NOT NULL,
    [Estado]             VARCHAR (50)  NOT NULL,
    [IdPlan]             INT           NOT NULL,
    [Numero]             AS            (replace(str([Id],(5)),space((1)),'0')),
    [MotivoFinalizacion] VARCHAR (500) NULL,
    [FechaFinalizacion]  DATETIME      NULL,
    CONSTRAINT [PK_Programa] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Programa_Plan] FOREIGN KEY ([IdPlan]) REFERENCES [dbo].[Plan] ([Id])
);


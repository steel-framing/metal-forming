CREATE TABLE [dbo].[Plan] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Codigo]      VARCHAR (50) NOT NULL,
    [FechaInicio] DATE         NOT NULL,
    [FechaFin]    DATE         NOT NULL,
    [Estado]      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED ([Id] ASC)
);


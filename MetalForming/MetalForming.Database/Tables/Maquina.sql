CREATE TABLE [dbo].[Maquina] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion]      VARCHAR (50) NULL,
    [Tipo]             VARCHAR (50) NULL,
    [PLD]              VARCHAR (50) NULL,
    [Configuracion]    VARCHAR (50) NULL,
    [Estado]           VARCHAR (50) NULL,
    [ReduacionInicio]  VARCHAR (50) NULL,
    [ReduacionFin]     VARCHAR (50) NULL,
    [CantidadRodillos] VARCHAR (50) NULL,
    [MaximoFrio]       VARCHAR (50) NULL,
    [MaximoCaliente]   VARCHAR (50) NULL,
    [PorcentajeFalla]  VARCHAR (50) NULL,
    [Tiempo]           VARCHAR (50) NULL,
    CONSTRAINT [PK_Maquina] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Usuario] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [IdRol]          INT          NOT NULL,
    [Username]       VARCHAR (50) NOT NULL,
    [NombreCompleto] VARCHAR (50) NOT NULL,
    [Password]       VARCHAR (50) NOT NULL,
    [Estado]         BIT          NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Rol] ([Id])
);


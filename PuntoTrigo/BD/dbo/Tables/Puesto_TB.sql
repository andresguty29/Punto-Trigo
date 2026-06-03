CREATE TABLE [dbo].[Puesto_TB] (
    [Id_Puesto]         UNIQUEIDENTIFIER NOT NULL,
    [Nombre_Puesto]     VARCHAR (MAX)  NOT NULL,
    [Id_Estado]         INT NOT NULL,
    CONSTRAINT [PK_Puesto_TB] PRIMARY KEY CLUSTERED ([Id_Puesto] ASC),
    CONSTRAINT [FK_Puesto_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id])
);
CREATE TABLE [dbo].[Usuario_TB](
    [Id_Usuario]      UNIQUEIDENTIFIER NOT NULL,
	[Nombre_Usuario]  VARCHAR (MAX)  NOT NULL,
	[Contrasena]      VARCHAR (MAX)  NOT NULL,
    [Id_Trabajdor]    INT NOT NULL,
	[Id_Estado]       INT NOT NULL,
    CONSTRAINT [PK_Usuario_TB] PRIMARY KEY CLUSTERED ([Id_Usuario] ASC),
    CONSTRAINT [FK_Usuario_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [FK_Usuario_TB_Trabajador_TB] FOREIGN KEY ([Id_Trabajdor]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajdor])
)
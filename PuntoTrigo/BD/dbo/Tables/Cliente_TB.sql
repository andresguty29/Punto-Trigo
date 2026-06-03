CREATE TABLE [dbo].[Cliente_TB] (
    [Cedula]            VARCHAR (MAX)  NULL,
	[Nombre_Completo]   VARCHAR (MAX)  NOT NULL,
	[Id_Estado]         INT NOT NULL,
    CONSTRAINT [PK_Cliente_TB] PRIMARY KEY CLUSTERED ([Cedula]),
    CONSTRAINT [FK_Cliente_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id])
);
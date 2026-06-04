CREATE TABLE [dbo].[Cliente_TB] (
    [Cedula]            VARCHAR (MAX)  NOT NULL,
	[Nombre_Completo]   VARCHAR (MAX)  NOT NULL,
	[Id_Estado]         INT NOT NULL,
    [Id_Cliente] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Cliente_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]), 
    CONSTRAINT [PK_Cliente_TB] PRIMARY KEY ([Id_Cliente])
);
CREATE TABLE [dbo].[Cliente_TB] (
    [Cedula]            VARCHAR (20)   NOT NULL,
	[Nombre_Completo]   VARCHAR (150)  NOT NULL,
	[Correo_Cliente]    VARCHAR (200)  NULL,
	[Telefono_Cliente]  VARCHAR (20)   NULL,
	[Id_Estado]         INT NOT NULL,
    [Id_Cliente] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_Cliente_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [PK_Cliente_TB] PRIMARY KEY ([Id_Cliente]),
    CONSTRAINT [UQ_Cliente_Cedula] UNIQUE ([Cedula])
);

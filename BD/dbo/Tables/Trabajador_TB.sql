CREATE TABLE [dbo].[Trabajador_TB] (
    [Cedula]            VARCHAR (MAX)  NOT NULL,
	[Nombre_Completo]   VARCHAR (MAX)  NOT NULL,
	[Id_Estado]         INT NOT NULL,
	[Id_Puesto]         uniqueidentifier NOT NULL,
    [Id_Trabajador] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Trabajador_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [FK_Trabajador_TB_Puesto_TB] FOREIGN KEY ([Id_Puesto]) REFERENCES [dbo].[Puesto_TB] ([Id_Puesto]), 
    CONSTRAINT [PK_Trabajador_TB] PRIMARY KEY ([Id_Trabajador]),    
);
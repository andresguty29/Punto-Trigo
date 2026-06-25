CREATE TABLE [dbo].[Usuario_TB] (
    [Id_Usuario]     UNIQUEIDENTIFIER NOT NULL,
    [Nombre_Usuario] VARCHAR (100)    NOT NULL,
    [Contrasena]     VARCHAR (500)    NOT NULL,
    [Id_Trabajador]  UNIQUEIDENTIFIER NOT NULL,
    [Id_Estado]      INT              NOT NULL,
    [Rol]            NVARCHAR (20)    DEFAULT ('Cajas') NOT NULL,
    CONSTRAINT [PK_Usuario_TB] PRIMARY KEY CLUSTERED ([Id_Usuario] ASC),
    CONSTRAINT [CK_Usuario_Rol] CHECK ([Rol]='Panadero' OR [Rol]='Cajas' OR [Rol]='Admin'),
    CONSTRAINT [FK_Usuario_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [FK_Usuario_TB_Trabajador_TB] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [UQ_Usuario_NombreUsuario] UNIQUE NONCLUSTERED ([Nombre_Usuario] ASC),
    CONSTRAINT [UQ_Usuario_Trabajador] UNIQUE NONCLUSTERED ([Id_Trabajador] ASC)
);


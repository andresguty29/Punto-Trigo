CREATE TABLE [dbo].[RegistroAcceso_TB] (
    [Id_Registro]    UNIQUEIDENTIFIER NOT NULL,
    [Id_Usuario]     UNIQUEIDENTIFIER NULL,
    [Nombre_Usuario] VARCHAR (100)    NOT NULL,
    [Fecha_Login]    DATETIME2 (0)    NOT NULL CONSTRAINT [DF_RegistroAcceso_FechaLogin] DEFAULT (SYSDATETIME()),
    [Fecha_Logout]   DATETIME2 (0)    NULL,
    [Exitoso]        BIT              NOT NULL,
    CONSTRAINT [PK_RegistroAcceso_TB] PRIMARY KEY CLUSTERED ([Id_Registro] ASC),
    CONSTRAINT [FK_RegistroAcceso_Usuario] FOREIGN KEY ([Id_Usuario]) REFERENCES [dbo].[Usuario_TB] ([Id_Usuario])
);

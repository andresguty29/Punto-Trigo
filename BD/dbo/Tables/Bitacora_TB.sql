CREATE TABLE [dbo].[Bitacora_TB] (
    [Id_Bitacora]    UNIQUEIDENTIFIER NOT NULL,
    [Id_Usuario]     UNIQUEIDENTIFIER NULL,
    [Nombre_Usuario] VARCHAR (100)    NOT NULL,
    [Accion]         VARCHAR (100)    NOT NULL,
    [Detalle]        VARCHAR (500)    NULL,
    [Fecha_Hora]     DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Bitacora_Fecha] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_Bitacora_TB] PRIMARY KEY CLUSTERED ([Id_Bitacora] ASC),
    CONSTRAINT [FK_Bitacora_Usuario] FOREIGN KEY ([Id_Usuario]) REFERENCES [dbo].[Usuario_TB] ([Id_Usuario])
);

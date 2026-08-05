CREATE TABLE [dbo].[Asistencia_TB] (
    [Id_Asistencia]  UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]  UNIQUEIDENTIFIER NOT NULL,
    [Fecha]          DATE             NOT NULL,
    [Tipo_Evento]    VARCHAR (20)     NOT NULL,
    [Observaciones]  VARCHAR (200)    NULL,
    [Fecha_Registro] DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Asistencia_FechaRegistro] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_Asistencia_TB] PRIMARY KEY CLUSTERED ([Id_Asistencia] ASC),
    CONSTRAINT [FK_Asistencia_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [CK_Asistencia_Tipo] CHECK ([Tipo_Evento] IN ('Falta', 'Retardo', 'DiaTrabajado')),
    CONSTRAINT [UQ_Asistencia_TrabajadorFecha] UNIQUE ([Id_Trabajador], [Fecha])
);

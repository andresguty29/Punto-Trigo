CREATE TABLE [dbo].[Prestamo_TB] (
    [Id_Prestamo]      UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]    UNIQUEIDENTIFIER NOT NULL,
    [Monto]            DECIMAL (18, 2)  NOT NULL,
    [Fecha]            DATE             NOT NULL,
    [Descripcion]      VARCHAR (200)    NULL,
    [Saldo_Pendiente]  DECIMAL (18, 2)  NOT NULL,
    [Fecha_Registro]   DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Prestamo_FechaRegistro] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_Prestamo_TB] PRIMARY KEY CLUSTERED ([Id_Prestamo] ASC),
    CONSTRAINT [FK_Prestamo_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [CK_Prestamo_Monto] CHECK ([Monto] > 0)
);

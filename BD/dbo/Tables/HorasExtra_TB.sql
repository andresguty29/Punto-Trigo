CREATE TABLE [dbo].[HorasExtra_TB] (
    [Id_HorasExtra]     UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]     UNIQUEIDENTIFIER NOT NULL,
    [Fecha]             DATE             NOT NULL,
    [Horas]             DECIMAL (5, 2)   NOT NULL,
    [Tarifa_Aplicada]   DECIMAL (18, 2)  NULL,
    [Monto_Calculado]   DECIMAL (18, 2)  NULL,
    [Fecha_Registro]    DATETIME2 (0)    NOT NULL CONSTRAINT [DF_HorasExtra_FechaRegistro] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_HorasExtra_TB] PRIMARY KEY CLUSTERED ([Id_HorasExtra] ASC),
    CONSTRAINT [FK_HorasExtra_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [CK_HorasExtra_Horas] CHECK ([Horas] > 0),
    CONSTRAINT [UQ_HorasExtra_TrabajadorFecha] UNIQUE ([Id_Trabajador], [Fecha])
);

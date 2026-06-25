CREATE TABLE [dbo].[SaldoVacaciones_TB] (
    [Id_Saldo]            UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]       UNIQUEIDENTIFIER NOT NULL,
    [Dias_Acumulados]     DECIMAL (6, 2)   DEFAULT ((0)) NOT NULL,
    [Dias_Gozados]        DECIMAL (6, 2)   DEFAULT ((0)) NOT NULL,
    [Dias_Pendientes]     AS               ([Dias_Acumulados]-[Dias_Gozados]) PERSISTED,
    [Anio]                INT              NOT NULL,
    [Fecha_Actualizacion] DATETIME         DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SaldoVacaciones_TB] PRIMARY KEY CLUSTERED ([Id_Saldo] ASC),
    CONSTRAINT [CHK_SaldoDias_Acumulados] CHECK ([Dias_Acumulados]>=(0)),
    CONSTRAINT [CHK_SaldoDias_Gozados] CHECK ([Dias_Gozados]>=(0)),
    CONSTRAINT [FK_SaldoVacaciones_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [UQ_SaldoVacaciones_TrabajadorAnio] UNIQUE NONCLUSTERED ([Id_Trabajador] ASC, [Anio] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_SaldoVacaciones_Trabajador]
    ON [dbo].[SaldoVacaciones_TB]([Id_Trabajador] ASC, [Anio] ASC);


CREATE TABLE [dbo].[VacacionAsignada_TB] (
    [Id_Vacacion]       UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]      UNIQUEIDENTIFIER NOT NULL,
    [Anio_Antiguedad]    INT              NOT NULL,
    [Dias_Asignados]     INT              NOT NULL,
    [Fecha_Asignacion]   DATETIME2 (0)    NOT NULL CONSTRAINT [DF_VacacionAsignada_Fecha] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_VacacionAsignada_TB] PRIMARY KEY CLUSTERED ([Id_Vacacion] ASC),
    CONSTRAINT [FK_VacacionAsignada_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [UQ_VacacionAsignada_TrabajadorAnio] UNIQUE ([Id_Trabajador], [Anio_Antiguedad])
);

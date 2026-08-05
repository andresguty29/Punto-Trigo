CREATE TABLE [dbo].[PlanillaPago_TB] (
    [Id_Planilla]             UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]           UNIQUEIDENTIFIER NOT NULL,
    [Periodo]                 VARCHAR (30)     NOT NULL,
    [Fecha_Inicio]            DATE             NOT NULL,
    [Fecha_Fin]               DATE             NOT NULL,
    [Salario_Base_Aplicado]   DECIMAL (18, 2)  NULL,
    [Ingreso_Horas_Extra]     DECIMAL (18, 2)  NOT NULL CONSTRAINT [DF_Planilla_IngresoHorasExtra] DEFAULT (0),
    [Deduccion_Asistencia]    DECIMAL (18, 2)  NOT NULL CONSTRAINT [DF_Planilla_DeduccionAsistencia] DEFAULT (0),
    [Deduccion_Prestamos]     DECIMAL (18, 2)  NOT NULL CONSTRAINT [DF_Planilla_DeduccionPrestamos] DEFAULT (0),
    [Deduccion_CCSS]          DECIMAL (18, 2)  NOT NULL CONSTRAINT [DF_Planilla_DeduccionCCSS] DEFAULT (0),
    [Total_Ingresos]          DECIMAL (18, 2)  NOT NULL,
    [Total_Deducciones]       DECIMAL (18, 2)  NOT NULL,
    [Monto_Neto]              DECIMAL (18, 2)  NOT NULL,
    [Fecha_Generacion]        DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Planilla_FechaGeneracion] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_PlanillaPago_TB] PRIMARY KEY CLUSTERED ([Id_Planilla] ASC),
    CONSTRAINT [FK_PlanillaPago_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [UQ_PlanillaPago_TrabajadorPeriodo] UNIQUE ([Id_Trabajador], [Periodo])
);

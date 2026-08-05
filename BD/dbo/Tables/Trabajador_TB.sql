CREATE TABLE [dbo].[Trabajador_TB] (
    [Cedula]            VARCHAR (20)  NOT NULL,
	[Nombre_Completo]   VARCHAR (MAX)  NOT NULL,
	[Id_Estado]         INT NOT NULL,
	[Id_Puesto]         uniqueidentifier NOT NULL,
    [Id_Trabajador] UNIQUEIDENTIFIER NOT NULL,
    [Fecha_Ingreso]     DATE            NULL,
    [Tipo_Pago]         VARCHAR (20)    NULL,
    [Salario_Base]      DECIMAL (18,2)  NULL,
    [Tarifa_Hora]       DECIMAL (18,2)  NULL,
    CONSTRAINT [FK_Trabajador_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [FK_Trabajador_TB_Puesto_TB] FOREIGN KEY ([Id_Puesto]) REFERENCES [dbo].[Puesto_TB] ([Id_Puesto]),
    CONSTRAINT [PK_Trabajador_TB] PRIMARY KEY ([Id_Trabajador]),
    CONSTRAINT [UQ_Trabajador_Cedula] UNIQUE ([Cedula]),
    CONSTRAINT [CK_Trabajador_TipoPago] CHECK ([Tipo_Pago] IS NULL OR [Tipo_Pago] IN ('Mensual', 'Quincenal'))
);
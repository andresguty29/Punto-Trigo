CREATE TABLE [dbo].[Incapacidad_TB] (
    [Id_Incapacidad]   UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]    UNIQUEIDENTIFIER NOT NULL,
    [Fecha_Inicio]     DATE             NOT NULL,
    [Fecha_Fin]        DATE             NOT NULL,
    [Dias_Incapacidad] INT              NOT NULL,
    [Tipo_Incapacidad] VARCHAR (100)    NOT NULL,
    [Numero_CCSS]      VARCHAR (50)     NULL,
    [Diagnostico]      VARCHAR (500)    NULL,
    [Observaciones]    VARCHAR (500)    NULL,
    [Id_Estado]        INT              DEFAULT ((1)) NOT NULL,
    [Fecha_Registro]   DATETIME         DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Incapacidad_TB] PRIMARY KEY CLUSTERED ([Id_Incapacidad] ASC),
    CONSTRAINT [CHK_Incapacidad_Dias] CHECK ([Dias_Incapacidad]>(0)),
    CONSTRAINT [CHK_Incapacidad_Fechas] CHECK ([Fecha_Fin]>=[Fecha_Inicio]),
    CONSTRAINT [FK_Incapacidad_Estado] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [FK_Incapacidad_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador])
);


GO
CREATE NONCLUSTERED INDEX [IX_Incapacidad_Trabajador]
    ON [dbo].[Incapacidad_TB]([Id_Trabajador] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Incapacidad_Fechas]
    ON [dbo].[Incapacidad_TB]([Fecha_Inicio] ASC, [Fecha_Fin] ASC);


CREATE TABLE [dbo].[Vacaciones_TB] (
    [Id_Vacaciones]    UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]    UNIQUEIDENTIFIER NOT NULL,
    [Fecha_Inicio]     DATE             NOT NULL,
    [Fecha_Fin]        DATE             NOT NULL,
    [Dias_Solicitados] INT              NOT NULL,
    [Observaciones]    VARCHAR (500)    NULL,
    [Id_Estado]        INT              DEFAULT ((1)) NOT NULL,
    [Fecha_Registro]   DATETIME         DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Vacaciones_TB] PRIMARY KEY CLUSTERED ([Id_Vacaciones] ASC),
    CONSTRAINT [CHK_Vacaciones_Dias] CHECK ([Dias_Solicitados]>(0)),
    CONSTRAINT [CHK_Vacaciones_Fechas] CHECK ([Fecha_Fin]>=[Fecha_Inicio]),
    CONSTRAINT [FK_Vacaciones_Estado] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [FK_Vacaciones_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador])
);


GO
CREATE NONCLUSTERED INDEX [IX_Vacaciones_Fechas]
    ON [dbo].[Vacaciones_TB]([Fecha_Inicio] ASC, [Fecha_Fin] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Vacaciones_Trabajador]
    ON [dbo].[Vacaciones_TB]([Id_Trabajador] ASC);


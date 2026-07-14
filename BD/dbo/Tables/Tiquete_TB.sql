CREATE TABLE [dbo].[Tiquete_TB] (
    [Id_Tiquete]     UNIQUEIDENTIFIER NOT NULL,
    [Consecutivo]    VARCHAR (20)     NOT NULL,
    [Clave]          VARCHAR (50)     NOT NULL,
    [Id_Cliente]     UNIQUEIDENTIFIER NULL,
    [Id_Trabajador]  UNIQUEIDENTIFIER NULL,
    [Fecha_Emision]  DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Tiquete_Fecha] DEFAULT (SYSDATETIME()),
    [Estado]         VARCHAR (20)     NOT NULL,
    [Monto_Total]    DECIMAL (18, 2)  NOT NULL,
    CONSTRAINT [PK_Tiquete_TB] PRIMARY KEY CLUSTERED ([Id_Tiquete] ASC),
    CONSTRAINT [FK_Tiquete_Cliente] FOREIGN KEY ([Id_Cliente]) REFERENCES [dbo].[Cliente_TB] ([Id_Cliente]),
    CONSTRAINT [FK_Tiquete_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [CK_Tiquete_Estado] CHECK ([Estado] IN ('Emitido', 'PendienteEnvio', 'Anulado')),
    CONSTRAINT [UQ_Tiquete_Consecutivo] UNIQUE ([Consecutivo])
);

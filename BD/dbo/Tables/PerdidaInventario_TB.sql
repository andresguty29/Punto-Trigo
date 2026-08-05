CREATE TABLE [dbo].[PerdidaInventario_TB] (
    [Id_Perdida]        UNIQUEIDENTIFIER NOT NULL,
    [Id_Movimiento]      UNIQUEIDENTIFIER NOT NULL,
    [Id_Inventario]      UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]           DECIMAL (10, 2)  NOT NULL,
    [Costo_Total]        DECIMAL (18, 2)  NOT NULL,
    [Fecha_Vencimiento]  DATE             NOT NULL,
    [Fecha_Procesado]    DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Perdida_FechaProcesado] DEFAULT (SYSDATETIME()),
    CONSTRAINT [PK_PerdidaInventario_TB] PRIMARY KEY CLUSTERED ([Id_Perdida] ASC),
    CONSTRAINT [FK_Perdida_Movimiento] FOREIGN KEY ([Id_Movimiento]) REFERENCES [dbo].[MovimientoInventario_TB] ([Id_Movimiento]),
    CONSTRAINT [FK_Perdida_Inventario] FOREIGN KEY ([Id_Inventario]) REFERENCES [dbo].[Inventario_TB] ([Id_Inventario]),
    CONSTRAINT [UQ_Perdida_Movimiento] UNIQUE ([Id_Movimiento])
);

CREATE TABLE [dbo].[MovimientoInventario_TB] (
    [Id_Movimiento] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Id_Inventario] UNIQUEIDENTIFIER NOT NULL,
    [Tipo]          NVARCHAR(10)     NOT NULL,
    [Cantidad]      DECIMAL(10,2)    NOT NULL,
    [Fecha]         DATETIME         NOT NULL DEFAULT GETDATE(),
    [Motivo]        NVARCHAR(200)    NULL,
    [Id_Proveedor]  UNIQUEIDENTIFIER NULL,
    [Id_Estado]     INT              NOT NULL DEFAULT 1,

    CONSTRAINT PK_MovimientoInventario PRIMARY KEY ([Id_Movimiento]),
    CONSTRAINT FK_Movimiento_Inventario FOREIGN KEY ([Id_Inventario])
        REFERENCES [dbo].[Inventario_TB]([Id_Inventario]),
    CONSTRAINT FK_Movimiento_Proveedor FOREIGN KEY ([Id_Proveedor])
        REFERENCES [dbo].[Proveedor_TB]([Id_Proveedor]),
    CONSTRAINT CK_Movimiento_Tipo CHECK ([Tipo] IN ('Entrada', 'Salida', 'Ajuste'))
);

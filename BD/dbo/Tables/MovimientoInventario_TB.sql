CREATE TABLE [dbo].[MovimientoInventario_TB] (
    [Id_Movimiento] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Id_Inventario] UNIQUEIDENTIFIER NOT NULL,
    [Tipo]          NVARCHAR (10)    NOT NULL,
    [Cantidad]      DECIMAL (10, 2)  NOT NULL,
    [Fecha]         DATETIME         DEFAULT (getdate()) NOT NULL,
    [Motivo]        NVARCHAR (200)   NULL,
    [Id_Proveedor]  UNIQUEIDENTIFIER NULL,
    [Id_Estado]     INT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_MovimientoInventario] PRIMARY KEY CLUSTERED ([Id_Movimiento] ASC),
    CONSTRAINT [CK_Movimiento_Tipo] CHECK ([Tipo]='Ajuste' OR [Tipo]='Salida' OR [Tipo]='Entrada'),
    CONSTRAINT [FK_Movimiento_Inventario] FOREIGN KEY ([Id_Inventario]) REFERENCES [dbo].[Inventario_TB] ([Id_Inventario]),
    CONSTRAINT [FK_Movimiento_Proveedor] FOREIGN KEY ([Id_Proveedor]) REFERENCES [dbo].[Proveedor_TB] ([Id_Proveedor])
);


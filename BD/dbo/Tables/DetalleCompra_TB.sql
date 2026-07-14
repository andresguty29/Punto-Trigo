CREATE TABLE [dbo].[DetalleCompra_TB] (
    [Id_DetalleCompra] UNIQUEIDENTIFIER NOT NULL,
    [Id_Compra]        UNIQUEIDENTIFIER NOT NULL,
    [Id_Inventario]    UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]         DECIMAL (10, 2)  NOT NULL,
    [Unidad_Ingresada] VARCHAR (20)     NOT NULL,
    [Costo_Unitario]   DECIMAL (18, 2)  NULL,
    CONSTRAINT [PK_DetalleCompra_TB] PRIMARY KEY CLUSTERED ([Id_DetalleCompra] ASC),
    CONSTRAINT [FK_DetalleCompra_Compra] FOREIGN KEY ([Id_Compra]) REFERENCES [dbo].[Compra_TB] ([Id_Compra]),
    CONSTRAINT [FK_DetalleCompra_Inventario] FOREIGN KEY ([Id_Inventario]) REFERENCES [dbo].[Inventario_TB] ([Id_Inventario])
);

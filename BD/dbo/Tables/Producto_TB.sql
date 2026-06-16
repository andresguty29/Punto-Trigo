CREATE TABLE [dbo].[Producto_TB] (
    [Id_Producto]     UNIQUEIDENTIFIER NOT NULL,
    [Id_Estado]       INT              NOT NULL,
    [Id_Proveedor]    UNIQUEIDENTIFIER NULL,
    [Nombre_Producto] VARCHAR (MAX)    NOT NULL,
    [Precio_Venta]    DECIMAL (18, 2)  NOT NULL,
    [Stock_Actual]    INT              NOT NULL,
    CONSTRAINT [PK_Producto_TB] PRIMARY KEY CLUSTERED ([Id_Producto] ASC),
    CONSTRAINT [FK_Producto_TB_Estados_TB]   FOREIGN KEY ([Id_Estado])    REFERENCES [dbo].[Estados_TB]   ([Id]),
    CONSTRAINT [FK_Producto_TB_Proveedor_TB] FOREIGN KEY ([Id_Proveedor]) REFERENCES [dbo].[Proveedor_TB] ([Id_Proveedor])
);


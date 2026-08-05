CREATE TABLE [dbo].[Producto_TB] (
    [Id_Producto]     UNIQUEIDENTIFIER NOT NULL,
    [Id_Estado]       INT              NOT NULL,
    [Id_Proveedor]    UNIQUEIDENTIFIER NULL,
    [Nombre_Producto] VARCHAR (MAX)    NOT NULL,
    [Precio_Venta]    DECIMAL (18, 2)  NOT NULL,
    [Stock_Actual]    INT              NOT NULL,
    [Imagen_Path]     VARCHAR (500)    NULL,
    [Codigo]          VARCHAR (30)     NULL,
    CONSTRAINT [PK_Producto_TB] PRIMARY KEY CLUSTERED ([Id_Producto] ASC),
    CONSTRAINT [FK_Producto_TB_Estados_TB]   FOREIGN KEY ([Id_Estado])    REFERENCES [dbo].[Estados_TB]   ([Id]),
    CONSTRAINT [FK_Producto_TB_Proveedor_TB] FOREIGN KEY ([Id_Proveedor]) REFERENCES [dbo].[Proveedor_TB] ([Id_Proveedor])
);
GO
-- Indice unico filtrado: permite multiples NULL pero no codigos duplicados
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Producto_Codigo]
    ON [dbo].[Producto_TB] ([Codigo])
    WHERE [Codigo] IS NOT NULL;


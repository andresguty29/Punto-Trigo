CREATE TABLE [dbo].[Producto_TB] (
    [Id_Producto]     UNIQUEIDENTIFIER NOT NULL,
    [Estado]          INT              NOT NULL,
    [Nombre_Producto] VARCHAR (MAX)    NOT NULL,
    [Precio_Venta]    DECIMAL (18)     NOT NULL,
    [Stock_Actual]    INT              NOT NULL,
    CONSTRAINT [PK_Producto_TB] PRIMARY KEY CLUSTERED ([Id_Producto] ASC),
    CONSTRAINT [FK_Producto_TB_Estados_TB] FOREIGN KEY ([Estado]) REFERENCES [dbo].[Estados_TB] ([Id])
);


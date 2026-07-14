CREATE TABLE [dbo].[DetalleTiquete_TB] (
    [Id_DetalleTiquete] UNIQUEIDENTIFIER NOT NULL,
    [Id_Tiquete]        UNIQUEIDENTIFIER NOT NULL,
    [Id_Producto]       UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]          INT              NOT NULL,
    [Precio_Unitario]   DECIMAL (18, 2)  NOT NULL,
    [Subtotal]          DECIMAL (18, 2)  NOT NULL,
    CONSTRAINT [PK_DetalleTiquete_TB] PRIMARY KEY CLUSTERED ([Id_DetalleTiquete] ASC),
    CONSTRAINT [FK_DetalleTiquete_Tiquete] FOREIGN KEY ([Id_Tiquete]) REFERENCES [dbo].[Tiquete_TB] ([Id_Tiquete]),
    CONSTRAINT [FK_DetalleTiquete_Producto] FOREIGN KEY ([Id_Producto]) REFERENCES [dbo].[Producto_TB] ([Id_Producto])
);

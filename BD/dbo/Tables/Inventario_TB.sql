CREATE TABLE [dbo].[Inventario_TB] (
    [Id_Inventario] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Nombre]        NVARCHAR(100)    NOT NULL,
    [Unidad]        NVARCHAR(20)     NOT NULL,
    [Stock_Actual]  DECIMAL(10,2)    NOT NULL DEFAULT 0,
    [Stock_Minimo]  DECIMAL(10,2)    NOT NULL DEFAULT 0,
    [Id_Proveedor]  UNIQUEIDENTIFIER NULL,
    [Id_Estado]     INT              NOT NULL DEFAULT 1,

    CONSTRAINT PK_Inventario PRIMARY KEY ([Id_Inventario]),
    CONSTRAINT FK_Inventario_Proveedor FOREIGN KEY ([Id_Proveedor])
        REFERENCES [dbo].[Proveedor_TB]([Id_Proveedor]),
    CONSTRAINT FK_Inventario_Estado FOREIGN KEY ([Id_Estado])
        REFERENCES [dbo].[Estados_TB]([Id])
);

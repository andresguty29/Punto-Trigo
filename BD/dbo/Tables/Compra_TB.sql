CREATE TABLE [dbo].[Compra_TB] (
    [Id_Compra]              UNIQUEIDENTIFIER NOT NULL,
    [Id_Proveedor]           UNIQUEIDENTIFIER NOT NULL,
    [Numero_Factura]         VARCHAR (50)     NOT NULL,
    [Fecha_Compra]           DATETIME2 (0)    NOT NULL CONSTRAINT [DF_Compra_Fecha] DEFAULT (SYSDATETIME()),
    [Categoria]              VARCHAR (50)     NOT NULL,
    [Descripcion_Adicional]  VARCHAR (200)    NULL,
    [Monto_Total]            DECIMAL (18, 2)  NOT NULL,
    [Id_Estado]              INT              NOT NULL,
    CONSTRAINT [PK_Compra_TB] PRIMARY KEY CLUSTERED ([Id_Compra] ASC),
    CONSTRAINT [FK_Compra_Proveedor] FOREIGN KEY ([Id_Proveedor]) REFERENCES [dbo].[Proveedor_TB] ([Id_Proveedor]),
    CONSTRAINT [FK_Compra_Estado] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [CK_Compra_Categoria] CHECK ([Categoria] IN ('Materia Prima', 'Limpieza', 'Mantenimiento', 'Otro')),
    CONSTRAINT [CK_Compra_Monto] CHECK ([Monto_Total] > 0),
    CONSTRAINT [UQ_Compra_ProveedorFactura] UNIQUE ([Id_Proveedor], [Numero_Factura])
);

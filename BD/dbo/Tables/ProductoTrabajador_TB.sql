CREATE TABLE [dbo].[ProductoTrabajador_TB] (
    [Id_Asignacion]     UNIQUEIDENTIFIER NOT NULL,
    [Id_Trabajador]     UNIQUEIDENTIFIER NOT NULL,
    [Id_Producto]       UNIQUEIDENTIFIER NOT NULL,
    [Cantidad_Diaria]   INT              NOT NULL,
    [Fecha_Asignacion]  DATETIME2 (0)    NOT NULL CONSTRAINT [DF_ProductoTrabajador_Fecha] DEFAULT (SYSDATETIME()),
    [Id_Estado]         INT              NOT NULL,
    [Realizado]         BIT              NOT NULL CONSTRAINT [DF_ProductoTrabajador_Realizado] DEFAULT (0),
    CONSTRAINT [PK_ProductoTrabajador_TB] PRIMARY KEY CLUSTERED ([Id_Asignacion] ASC),
    CONSTRAINT [FK_ProductoTrabajador_Trabajador] FOREIGN KEY ([Id_Trabajador]) REFERENCES [dbo].[Trabajador_TB] ([Id_Trabajador]),
    CONSTRAINT [FK_ProductoTrabajador_Producto] FOREIGN KEY ([Id_Producto]) REFERENCES [dbo].[Producto_TB] ([Id_Producto]),
    CONSTRAINT [FK_ProductoTrabajador_Estado] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [UQ_ProductoTrabajador_EmpleadoProducto] UNIQUE ([Id_Trabajador], [Id_Producto])
);

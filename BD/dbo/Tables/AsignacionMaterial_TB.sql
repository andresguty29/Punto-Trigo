CREATE TABLE [dbo].[AsignacionMaterial_TB] (
    [Id_AsignacionMaterial] UNIQUEIDENTIFIER NOT NULL,
    [Id_Asignacion]         UNIQUEIDENTIFIER NOT NULL,
    [Id_Inventario]         UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]              DECIMAL (10, 2)  NOT NULL,
    CONSTRAINT [PK_AsignacionMaterial_TB] PRIMARY KEY CLUSTERED ([Id_AsignacionMaterial] ASC),
    CONSTRAINT [FK_AsignacionMaterial_Asignacion] FOREIGN KEY ([Id_Asignacion]) REFERENCES [dbo].[ProductoTrabajador_TB] ([Id_Asignacion]),
    CONSTRAINT [FK_AsignacionMaterial_Inventario] FOREIGN KEY ([Id_Inventario]) REFERENCES [dbo].[Inventario_TB] ([Id_Inventario]),
    CONSTRAINT [UQ_AsignacionMaterial] UNIQUE ([Id_Asignacion], [Id_Inventario])
);

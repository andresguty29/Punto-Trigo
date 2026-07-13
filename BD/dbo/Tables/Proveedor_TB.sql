CREATE TABLE [dbo].[Proveedor_TB] (
    [Id_Proveedor]            UNIQUEIDENTIFIER NOT NULL,
    [Id_Estado]               INT              NOT NULL,
    [Identificacion_Proveedor] VARCHAR (20)    NULL,
    [Nombre_Proveedor]        VARCHAR (150)    NOT NULL,
    [Telefono_Proveedor]      VARCHAR (20)     NOT NULL,
    [Correo_Proveedor]        VARCHAR (200)    NOT NULL,
    CONSTRAINT [PK_Proveedor_TB] PRIMARY KEY CLUSTERED ([Id_Proveedor] ASC),
    CONSTRAINT [FK_Proveedor_TB_Estados_TB] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id])
);
GO
-- Indice unico filtrado: permite multiples NULL pero no identificaciones duplicadas
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Proveedor_Identificacion]
    ON [dbo].[Proveedor_TB] ([Identificacion_Proveedor])
    WHERE [Identificacion_Proveedor] IS NOT NULL;


CREATE TABLE [dbo].[Proveedor_TB] (
    [Id_Proveedor]       UNIQUEIDENTIFIER NOT NULL,
    [Estado]             INT              NOT NULL,
    [Nombre_Proveedor]   VARCHAR (MAX)    NOT NULL,
    [Telefono_Proveedor] VARCHAR (MAX)    NOT NULL,
    [Correo_Proveedor]   VARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_Proveedor_TB] PRIMARY KEY CLUSTERED ([Id_Proveedor] ASC),
    CONSTRAINT [FK_Proveedor_TB_Estados_TB] FOREIGN KEY ([Estado]) REFERENCES [dbo].[Estados_TB] ([Id])
);


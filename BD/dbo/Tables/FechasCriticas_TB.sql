CREATE TABLE [dbo].[FechasCriticas_TB] (
    [Id_FechaCritica] UNIQUEIDENTIFIER NOT NULL,
    [Fecha]           DATE             NOT NULL,
    [Descripcion]     VARCHAR (200)    NOT NULL,
    [Es_Recurrente]   BIT              DEFAULT ((0)) NOT NULL,
    [Id_Estado]       INT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_FechasCriticas_TB] PRIMARY KEY CLUSTERED ([Id_FechaCritica] ASC),
    CONSTRAINT [FK_FechasCriticas_Estado] FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Estados_TB] ([Id]),
    CONSTRAINT [UQ_FechasCriticas_Fecha] UNIQUE NONCLUSTERED ([Fecha] ASC)
);


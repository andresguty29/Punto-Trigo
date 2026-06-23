-- Migración: agregar columna Rol a Usuario_TB
ALTER TABLE [dbo].[Usuario_TB]
ADD [Rol] NVARCHAR(20) NOT NULL DEFAULT 'Cajas';

ALTER TABLE [dbo].[Usuario_TB]
ADD CONSTRAINT CK_Usuario_Rol CHECK ([Rol] IN ('Admin', 'Cajas', 'Panadero'));

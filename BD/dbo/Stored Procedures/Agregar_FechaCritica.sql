
-- =============================================
-- FECHAS CRÍTICAS
-- =============================================

CREATE PROCEDURE [dbo].[Agregar_FechaCritica]
    @Id_FechaCritica    UNIQUEIDENTIFIER,
    @Fecha              DATE,
    @Descripcion        VARCHAR(200),
    @Es_Recurrente      BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM [dbo].[FechasCriticas_TB] WHERE [Fecha] = @Fecha AND [Id_Estado] = 1)
    BEGIN
        RAISERROR('Ya existe una fecha crítica registrada para esa fecha.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[FechasCriticas_TB]
    ([Id_FechaCritica], [Fecha], [Descripcion], [Es_Recurrente], [Id_Estado])
    VALUES
    (@Id_FechaCritica, @Fecha, @Descripcion, @Es_Recurrente, 1);

    SELECT @Id_FechaCritica AS Id_FechaCritica;
END
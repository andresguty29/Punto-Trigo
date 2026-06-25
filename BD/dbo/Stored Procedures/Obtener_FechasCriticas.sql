
CREATE PROCEDURE [dbo].[Obtener_FechasCriticas]
    @Fecha_Inicio   DATE = NULL,
    @Fecha_Fin      DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id_FechaCritica],
        [Fecha],
        [Descripcion],
        [Es_Recurrente],
        [Id_Estado]
    FROM [dbo].[FechasCriticas_TB]
    WHERE [Id_Estado] = 1
      AND (@Fecha_Inicio IS NULL OR [Fecha] >= @Fecha_Inicio)
      AND (@Fecha_Fin   IS NULL OR [Fecha] <= @Fecha_Fin)
    ORDER BY [Fecha] ASC;
END
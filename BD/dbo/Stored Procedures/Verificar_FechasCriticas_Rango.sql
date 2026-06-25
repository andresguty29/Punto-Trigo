
CREATE PROCEDURE [dbo].[Verificar_FechasCriticas_Rango]
    @Fecha_Inicio   DATE,
    @Fecha_Fin      DATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Retorna las fechas críticas que caen dentro del rango solicitado
    -- (incluyendo recurrentes según día/mes)
    SELECT
        [Id_FechaCritica],
        [Fecha],
        [Descripcion],
        [Es_Recurrente]
    FROM [dbo].[FechasCriticas_TB]
    WHERE [Id_Estado] = 1
      AND (
        -- Fecha exacta dentro del rango
        ([Fecha] BETWEEN @Fecha_Inicio AND @Fecha_Fin)
        OR
        -- Recurrentes: mismo día y mes, cualquier año dentro del rango
        (
            [Es_Recurrente] = 1
            AND EXISTS (
                SELECT 1
                FROM (
                    SELECT DATEFROMPARTS(YEAR(@Fecha_Inicio) + n, MONTH([Fecha]), DAY([Fecha])) AS FechaRecurrente
                    FROM (VALUES (0),(1)) AS Anios(n)
                ) AS Recurrentes
                WHERE FechaRecurrente BETWEEN @Fecha_Inicio AND @Fecha_Fin
            )
        )
      );
END
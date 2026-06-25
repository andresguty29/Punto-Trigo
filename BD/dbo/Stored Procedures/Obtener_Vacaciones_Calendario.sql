
CREATE PROCEDURE [dbo].[Obtener_Vacaciones_Calendario]
    @Fecha_Inicio   DATE = NULL,
    @Fecha_Fin      DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Si no se pasan fechas, se retorna el mes actual
    IF @Fecha_Inicio IS NULL SET @Fecha_Inicio = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    IF @Fecha_Fin   IS NULL SET @Fecha_Fin   = EOMONTH(GETDATE());

    SELECT
        v.[Id_Vacaciones],
        v.[Id_Trabajador],
        t.[Nombre_Completo],
        t.[Cedula],
        p.[Nombre_Puesto],
        v.[Fecha_Inicio],
        v.[Fecha_Fin],
        v.[Dias_Solicitados],
        v.[Observaciones],
        v.[Id_Estado]
    FROM [dbo].[Vacaciones_TB] v
    INNER JOIN [dbo].[Trabajador_TB] t ON t.[Id_Trabajador] = v.[Id_Trabajador]
    INNER JOIN [dbo].[Puesto_TB] p ON p.[Id_Puesto] = t.[Id_Puesto]
    WHERE v.[Id_Estado] = 1
      AND (
            v.[Fecha_Inicio] <= @Fecha_Fin AND
            v.[Fecha_Fin]   >= @Fecha_Inicio
          )
    ORDER BY v.[Fecha_Inicio] ASC;
END

CREATE PROCEDURE [dbo].[Obtener_Vacaciones_Trabajador]
    @Id_Trabajador UNIQUEIDENTIFIER,
    @Anio INT = NULL   -- Si NULL, devuelve todos los años
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.[Id_Vacaciones],
        v.[Id_Trabajador],
        t.[Nombre_Completo],
        t.[Cedula],
        v.[Fecha_Inicio],
        v.[Fecha_Fin],
        v.[Dias_Solicitados],
        v.[Observaciones],
        v.[Id_Estado],
        v.[Fecha_Registro],
        -- Saldo del año correspondiente
        sv.[Dias_Acumulados],
        sv.[Dias_Gozados],
        sv.[Dias_Pendientes]
    FROM [dbo].[Vacaciones_TB] v
    INNER JOIN [dbo].[Trabajador_TB] t ON t.[Id_Trabajador] = v.[Id_Trabajador]
    LEFT JOIN [dbo].[SaldoVacaciones_TB] sv 
        ON sv.[Id_Trabajador] = v.[Id_Trabajador] 
        AND sv.[Anio] = YEAR(v.[Fecha_Inicio])
    WHERE v.[Id_Trabajador] = @Id_Trabajador
      AND (@Anio IS NULL OR YEAR(v.[Fecha_Inicio]) = @Anio)
    ORDER BY v.[Fecha_Inicio] DESC;
END
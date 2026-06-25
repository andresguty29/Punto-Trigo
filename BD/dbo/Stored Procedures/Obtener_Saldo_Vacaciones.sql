
CREATE PROCEDURE [dbo].[Obtener_Saldo_Vacaciones]
    @Id_Trabajador  UNIQUEIDENTIFIER,
    @Anio           INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Anio IS NULL SET @Anio = YEAR(GETDATE());

    SELECT
        sv.[Id_Saldo],
        sv.[Id_Trabajador],
        t.[Nombre_Completo],
        t.[Cedula],
        sv.[Dias_Acumulados],
        sv.[Dias_Gozados],
        sv.[Dias_Pendientes],
        sv.[Anio],
        sv.[Fecha_Actualizacion]
    FROM [dbo].[SaldoVacaciones_TB] sv
    INNER JOIN [dbo].[Trabajador_TB] t ON t.[Id_Trabajador] = sv.[Id_Trabajador]
    WHERE sv.[Id_Trabajador] = @Id_Trabajador
      AND sv.[Anio] = @Anio;
END
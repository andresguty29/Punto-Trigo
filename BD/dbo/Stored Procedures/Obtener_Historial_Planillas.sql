CREATE PROCEDURE Obtener_Historial_Planillas
	@Fecha_Inicio DATE = NULL,
	@Fecha_Fin    DATE = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		p.[Id_Planilla],
		p.[Id_Trabajador],
		t.[Nombre_Completo] AS Nombre_Trabajador,
		p.[Periodo],
		p.[Fecha_Inicio],
		p.[Fecha_Fin],
		p.[Salario_Base_Aplicado],
		p.[Ingreso_Horas_Extra],
		p.[Deduccion_Asistencia],
		p.[Deduccion_Prestamos],
		p.[Deduccion_CCSS],
		p.[Total_Ingresos],
		p.[Total_Deducciones],
		p.[Monto_Neto],
		p.[Fecha_Generacion]
	FROM [dbo].[PlanillaPago_TB] p
	INNER JOIN [dbo].[Trabajador_TB] t ON p.[Id_Trabajador] = t.[Id_Trabajador]
	WHERE (@Fecha_Inicio IS NULL OR p.[Fecha_Fin]    >= @Fecha_Inicio)
	  AND (@Fecha_Fin    IS NULL OR p.[Fecha_Inicio] <= @Fecha_Fin)
	ORDER BY p.[Fecha_Generacion] DESC
END
